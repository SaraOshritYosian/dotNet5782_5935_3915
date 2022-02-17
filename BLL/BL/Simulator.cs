using System;
using BO;
using System.Threading;
using System.Linq;

namespace BL
{
    class Simulator
    {
        private const int EMPTY = 0;
        int SLEEP = 1000;
        int SPEED = 50;
        
        public Simulator(BL bl, int DroneID, Action WPFUpdate, Func<bool> StopCheck)
        {
            
            Drone drone = new Drone();
            drone.StatusBatter = 0;
            bool droneIsWaitnigForCharging = false;
            WPFUpdate();
            while (!StopCheck())
            {
                drone = bl.GetDrone(DroneID);
                switch (drone.StatusDrone)
                {
                    case StatusDrone.available://הרחפן זמין אפשר לשייך לחביחה אם יש או שליחה לטעינה
                        if (droneIsWaitnigForCharging)
                            
                            try { bl.SendingDroneToCharging(drone.Id); Thread.Sleep((int)SLEEP); }
                            catch (Exception)
                            {
                                droneIsWaitnigForCharging = true;
                            }
                        else
                            try
                            {
                                bl.AssignPackageToDrone(drone.Id);//שיוך חבילה לרחפן
                            }
                            catch (Exception)
                            {
                                if(drone.StatusBatter==100)
                                    Thread.Sleep((int)SLEEP);
                                else
                                  droneIsWaitnigForCharging = true;
                            }
                       // Thread.Sleep((int)SLEEP);
                        break;


                    case StatusDrone.delivered://אם בשליחות אז אפשר לאסוף או למסור 
                        BO.Parcel parcel = bl.GetParcel(drone.PackageInTransfe.Id);
                        if (!drone.PackageInTransfe.PackageMode)//אם לא נאסף
                        {
                            //if (drone.PackageInTransfe.far > SPEED * SLEEP)
                            //{
                            //    Location newLocation = calculateCurrnetLocation(drone.LocationDrone, drone.PackageInTransfe.DeliveryDestination, SLEEP * SPEED);
                            //    double newBattery = drone.StatusBatter - SLEEP * SPEED * bl.Free;//
                            //    drone.StatusBatter = newBattery;
                            //    drone.LocationDrone = newLocation;
                            //    bl.UpdateDrone(drone);
                            //}
                            //else
                            //{
                                try
                                {
                                    bl.PickUpPackage(drone.Id);
                                }
                                catch (Exception)
                                {
                                    Thread.Sleep(SLEEP);
                                }
                               
                            //}

                        }
                        else//הרחפן בדרך לחבילה
                        {
                            //if (drone.PackageInTransfe.far > SLEEP * SPEED)
                            //{
                            //    Location newLocation = calculateCurrnetLocation(drone.LocationDrone, drone.PackageInTransfe.DeliveryDestination, SLEEP * SPEED);
                            //    double newBattery = drone.StatusBatter - SLEEP * SPEED * bl.accessIDal.RequestPowerConsuption()[(int)drone.PackageInTransfe.Weight + 1];
                            //    drone.StatusBatter = newBattery;
                            //    drone.LocationDrone = newLocation;
                            //    bl.UpdateDrone(drone);
                            //}
                            //else
                            //{
                                try
                                {
                                    bl.PackageDeliveryByDrone(drone.Id);
                                }
                                catch (Exception)
                                {
                                    Thread.Sleep(SLEEP);
                                }

                                
                           // }//אם לא סופק החבילה
                        }
                       
                        break;


                    case StatusDrone.InMaintenance://אם בטעינה אז אפשר לשחרר
                        if (drone.StatusBatter < 100)
                        {
                            double newBattery = drone.StatusBatter + SPEED * bl.LoadingPrecents;
                            newBattery = newBattery < 100 ? newBattery : 100;
                            drone.StatusBatter = newBattery;
                            bl.UpdateDrone(drone);
                        }
                        else//סוללה מלאה מלאה אז..
                        {
                            try
                            {
                                bl.ReleaseDrone(drone.Id, default);
                                droneIsWaitnigForCharging = false;
                            }
                            catch (Exception)
                            {
                                Thread.Sleep(SLEEP);
                            }
                            
                        }
                        break;
                    default:
                        break;
                }
                WPFUpdate();
                Thread.Sleep((int)SLEEP);
            }
          

        }

        public BO.Location calculateCurrnetLocation(BO.Location startLoc, BO.Location endLoc, double step)
        {
            double ratio = step / calculateDist(startLoc, endLoc);
            double latDelta = endLoc.Latitude - startLoc.Latitude;
            double lngDelta = endLoc.Longitude - startLoc.Longitude;
            BO.Location newLoc = new();
            newLoc.Latitude = startLoc.Latitude + ratio * latDelta;
            newLoc.Longitude = startLoc.Longitude + ratio * lngDelta;
            return newLoc;

        }
        public double calculateDist(BO.Location loc1, BO.Location loc2)
        {
            const double Radios = 6371000;//meters
            //deg to radians
            double lat1 = loc1.Latitude * Math.PI / 180;
            double lat2 = loc2.Latitude * Math.PI / 180;
            double lng1 = loc1.Longitude * Math.PI / 180;
            double lng2 = loc2.Longitude * Math.PI / 180;

            //Haversine formula
            double a = Math.Pow(Math.Sin((lat2 - lat1) / 2), 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Pow(Math.Sin((lng2 - lng1) / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return Radios * c;
        }
    }
}

