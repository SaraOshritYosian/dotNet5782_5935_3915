using System;
using BO;
using System.Threading;
using System.Linq;

namespace BL
{
    class Simulator
    {
        int speed;
        int SLEEP = 500;
        int SPEED = 20;
        //BlApi.IBL bl;
        // DalApi.IDal dal;
        public Simulator(BL bl, int DroneID, Action WPFUpdate, Func<bool> StopCheck)
        {
            
            Drone drone = new Drone();
            drone.StatusBatter = 0;
            WPFUpdate();
            while (!StopCheck())
            {
                drone = bl.GetDrone(DroneID);
                switch (drone.StatusDrone)
                {
                    case StatusDrone.available://הרחפן זמין אפשר לשייך לחביחה אם יש או שליחה לטעינה
                        try
                        {
                            bl.AssignPackageToDrone(drone.Id);//שיוך חבילה לרחפן
                            WPFUpdate();
                            Thread.Sleep(SLEEP);
                        }
                        catch
                        {
                            try
                            {
                                if (drone.StatusBatter < 100)
                                {

                                    try { bl.SendingDroneToCharging(drone.Id); }//שליחת רחפן לטעינה
                                    catch { bl.UpdateDrone(drone); }
                                    WPFUpdate();
                                    Thread.Sleep(SLEEP);
                                }
                                else
                                {
                                    while (drone.StatusDrone == StatusDrone.available)
                                    {
                                        try { bl.AssignPackageToDrone(drone.Id); }//Tז שיוך חבילה לרחפן
                                        catch { Thread.Sleep(5000); }
                                    }
                                }
                            }
                            catch { Thread.Sleep(SLEEP); }
                        }
                        break;


                    case StatusDrone.delivered://אם בשליחות אז אפשר לאסוף או למסור 
                        BO.Parcel parcel = bl.GetParcel(drone.PackageInTransfe.Id);
                        if (parcel.PichedUp == default)//אם לא נאסף
                        {
                            try
                            {
                                bl.PickUpPackage(drone.Id);
                                WPFUpdate();
                                Thread.Sleep(SLEEP);
                            }
                            catch
                            { 

                                Thread.Sleep(5000);     
                            }

                        }
                        else
                        {
                            if (parcel.Delivered == default)//אם לא סופק החבילה
                            {
                                bl.PackageDeliveryByDrone(drone.Id);
                                WPFUpdate();
                                Thread.Sleep(SLEEP);
                                //update(drone, parcel);
                            }
                        }
                        break;


                    case StatusDrone.InMaintenance://אם בטעינה אז אפשר לשחרר
                        if (drone.StatusBatter == 100)//הסוללה מלאה אז לשחרר מטעינה
                        {
                            bl.ReleaseDrone(drone.Id, default);//שחרור רחפן
                            WPFUpdate();
                            Thread.Sleep(SLEEP);
                        }
                        else//סוללה לא מלאה אז..
                        {
                            double N = drone.StatusBatter + 1 * bl.LoadingPrecents;
                            drone.StatusBatter = N <= 100 ? N : 100;
                            bl.UpdateDrone(drone);
                            WPFUpdate();
                            Thread.Sleep(SLEEP);
                        }
                        break;
                    default:
                        break;
                }
            }
          

        }
    }
}

