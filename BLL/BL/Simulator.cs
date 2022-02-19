using System;
using BO;
using System.Threading;
using System.Linq;

namespace BL
{
    internal class Simulator
    {
        private const int EMPTY = 0;
        int SLEEP = 1000;
        int SPEED = 5000;
        
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

                            try
                            {
                                lock (bl)
                                {
                                    bl.SendingDroneToCharging(drone.Id); Thread.Sleep((int)SLEEP);
                                }
                            }
                            catch (Exception)
                            {
                                droneIsWaitnigForCharging = true;
                            }
                        else
                            try
                            {
                                lock (bl)
                                {
                                    bl.AssignPackageToDrone(drone.Id);//שיוך חבילה לרחפן
                                }
                            }
                            catch (Exception)
                            {
                                if (drone.StatusBatter < 100)
                                   
                                    droneIsWaitnigForCharging = true;
                            }
                       
                        break;


                    case StatusDrone.delivered://אם בשליחות אז אפשר לאסוף או למסור 
                        
                        if (!drone.PackageInTransfe.PackageMode)//אם לא נאסף
                        {
                            if (drone.PackageInTransfe.far > (SLEEP/1000) * SPEED)
                            {
                                Location newLocation = calculateCurrnetLocation(drone.LocationDrone, drone.PackageInTransfe.DeliveryDestination, (SLEEP/1000) * SPEED);
                                double newBattery = drone.StatusBatter -( SLEEP/1000) * SPEED/10 * bl.accessIDal.RequestPowerConsuption()[0];//
                                drone.StatusBatter = newBattery;
                                drone.LocationDrone = newLocation;
                                bl.UpdateDrone(drone);
                            }
                            else
                            {
                                try
                                {
                                    lock (bl)
                                    {
                                        bl.PickUpPackage(drone.Id);
                                    }
                                }
                                catch (Exception)
                                {
                                    Thread.Sleep(SLEEP/1000);
                                }
                               
                            }

                        }
                        else//הרחפן בדרך לחבילה
                        {
                            if (drone.PackageInTransfe.far > (SLEEP/1000) * SPEED)
                            {
                                lock (bl)
                                {
                                    Location newLocation = calculateCurrnetLocation(drone.LocationDrone, drone.PackageInTransfe.DeliveryDestination, (SLEEP/1000) * SPEED);
                                    double newBattery = drone.StatusBatter - (SLEEP / 1000) * SPEED * (bl.accessIDal.RequestPowerConsuption()[(int)drone.PackageInTransfe.Weight + 1]/10);
                                    drone.StatusBatter = newBattery;
                                    drone.LocationDrone = newLocation;
                                    bl.UpdateDrone(drone);
                                }
                            }
                            else
                            {
                                try
                                {
                                    lock (bl)
                                    {
                                        bl.PackageDeliveryByDrone(drone.Id);
                                    }
                                }
                                catch (Exception)
                                {
                                    Thread.Sleep(SLEEP);
                                }


                            }//אם לא סופק החבילה
                        }
                       
                        break;


                    case StatusDrone.InMaintenance://אם בטעינה אז אפשר לשחרר
                        if (drone.StatusBatter < 100)
                        {
                            double newBattery = drone.StatusBatter + SLEEP/1000 * bl.accessIDal.RequestPowerConsuption()[4];
                            newBattery = newBattery < 100 ? newBattery : 100;
                            drone.StatusBatter = newBattery;
                            lock (bl)
                            {
                                bl.UpdateDrone(drone);
                            }
                        }
                        else//סוללה מלאה מלאה אז..
                        {
                            try
                            {
                                lock (bl)
                                {
                                    bl.ReleaseDrone(drone.Id, default);
                                    droneIsWaitnigForCharging = false;
                                }
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
                Thread.Sleep(SLEEP);
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

