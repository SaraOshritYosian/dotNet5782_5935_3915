using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BL
{
    sealed partial class BL : IBL
    {

        #region Dronecharge
        public  double GetFarCalculatesTheSmallestDistance(int idDr)//check the min far station to drone
        {
            BO.DroneToList drone = DroneToLisToPrint(idDr);
            IEnumerable<DO.Station> station = accessIDal.GetAllStation();
            double  min;
            double chack;
            min = DistanceToFromStationToDroneLocation(drone.LocationDrone.Latitude, drone.LocationDrone.Longitude, station.ElementAt(0).Latitude, station.ElementAt(0).Longitude);
           
            for (int i = 1; i < station.Count(); i++)
            {
                if (station.ElementAt(i).ChargeSlots > 0)
                {
                    chack = DistanceToFromStationToDroneLocation(drone.LocationDrone.Latitude, drone.LocationDrone.Longitude, station.ElementAt(i).Latitude, station.ElementAt(i).Longitude);
                    if (min > chack)
                        min = chack;
                }
            }

            return min;
        }
        public DO.Station GetStationCalculatesTheSmallestDistance(int idDr)//check the min far station to drone
        {
            BO.DroneToList drone = DroneToLisToPrint(idDr);
            IEnumerable<DO.Station> station = accessIDal.GetAllStation();
            double chack, min;
            DO.Station s;
            min = DistanceToFromStationToDroneLocation(drone.LocationDrone.Latitude, drone.LocationDrone.Longitude, station.ElementAt(0).Latitude, station.ElementAt(0).Longitude);
            s = station.ElementAt(0);
            for (int i = 1; i < station.Count(); i++)
            {
                if (station.ElementAt(i).ChargeSlots > 0)//אם יש מקום טעינה פנוי
                {
                    chack = DistanceToFromStationToDroneLocation(drone.LocationDrone.Latitude, drone.LocationDrone.Longitude, station.ElementAt(i).Latitude, station.ElementAt(i).Longitude);
                    if (min > chack)
                    {
                        min = chack;
                        s = station.ElementAt(i);//שומרת את התחנה אם המרחק קטן יותר
                    }
                }
                   
            }

            return s;
        }
        public DO.Station GetStationCalculatesTheSmallestDistance(BO.Location location)//מקבל מיקום ושולח את התחנה שקרובה למיקום
        {
            IEnumerable<DO.Station> station = accessIDal.GetAllStation();
            double chack, min;
            DO.Station s;
            min = DistanceToFromStationToDroneLocation(location.Latitude, location.Longitude, station.ElementAt(0).Latitude, station.ElementAt(0).Longitude);
            s = station.ElementAt(0);
            for (int i = 1; i < station.Count(); i++)
            {
                if (station.ElementAt(i).ChargeSlots > 0)//אם יש מקום טעינה פנוי
                {
                    chack = DistanceToFromStationToDroneLocation(location.Latitude, location.Longitude, station.ElementAt(i).Latitude, station.ElementAt(i).Longitude);
                    if (min > chack)
                    {
                        min = chack;
                        s = station.ElementAt(i);//שומרת את התחנה אם המרחק קטן יותר
                    }
                }

            }

            return s;
        }






        public double returnMinDistancFromLicationToStation(BO.Location lo)//check the min far station to drone
        {
            IEnumerable<DO.Station> station = accessIDal.GetAllStation();
            double chack, min;
           // IDAL.DO.Station s = station.ElementAt(0);
            min = DistanceToFromStationToDroneLocation(lo.Latitude, lo.Longitude, station.ElementAt(0).Latitude, station.ElementAt(0).Longitude);
           // s = station.ElementAt(0);
            for (int i = 1; i < station.Count(); i++)
            {
                if (station.ElementAt(i).ChargeSlots > 0)//אם יש מקום טעינה פנוי
                {
                    chack = DistanceToFromStationToDroneLocation(lo.Latitude, lo.Longitude, station.ElementAt(i).Latitude, station.ElementAt(i).Longitude);
                    if (min > chack)
                    {
                        min = chack;
                      
                    }
                }

            }

            return min;
        }
        public double DistanceToFromStationToDroneLocation(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
        {
            
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case 'K': //Kilometers -> default
                    return dist * 1.609344;
                case 'N': //Nautical Miles 
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }

            return dist;
        }
        public void SendingDroneToCharging(int droneId)
        {
            DO.Station s;
            double battery = 0;
            double kilometerStationToDrone;
            try
            {
                if (DroneToLisToPrint(droneId).StatusDrone !=BO.StatusDrone.available)//אם הסטטוס שונה מפנוי יש חריגה
                    throw new Exception();
                if(accessIDal.ReturnStationHaveFreeCharde().Count()==0)
                    throw new Exception();
                else
                {
                    
                     kilometerStationToDrone = GetFarCalculatesTheSmallestDistance(droneId);//מקבל מרחק לתחנה קרובה
                    //חישוב מרחק בין התחנה לרחפן
                    s = GetStationCalculatesTheSmallestDistance(droneId);//מקבל את התחנה הקרובה
                    battery = BatteryConsumption(kilometerStationToDrone,(DO.WeightCategories)DroneToLisToPrint(droneId).Weight);// The amount of battery wasted
                    if (battery < DroneToLisToPrint(droneId).StatusBatter)// Check if the required battery is enough for the battery I have in the glider
                    {
                        accessIDal.SendDroneToCharge(s.Id, droneId);
                        BO.DroneToList drone = BlDrone.Find(p => p.Id == droneId);//תחילה מוצא אותו וזה עצם מועתק
                        drone.StatusBatter -= battery;//עדכון בטריה
                        drone.StatusDrone = BO.StatusDrone.InMaintenance;//עדכון מצב 
                        BO.Location l = new BO.Location();
                        l.Latitude = accessIDal.GetStation(s.Id).Latitude;//המיקום של התחנה שאיול נישלח הרחפן
                        l.Longitude = accessIDal.GetStation(s.Id).Longitude;
                        drone.LocationDrone = l;//עדכון מיקום רחפן
                        BlDrone.Remove(BlDrone.Find(p => p.Id == droneId));//מוציא
                        BlDrone.Add(new BO.DroneToList { Id = drone.Id, Model = drone.Model, Weight = drone.Weight, StatusBatter = drone.StatusBatter, StatusDrone = drone.StatusDrone, LocationDrone = drone.LocationDrone, IdParcel = drone.IdParcel });

                    }
                       else
                        throw new Exception();


                }
            }
            catch (BO.Excptions)
            {
                throw new BO.AlreadyExistException();
            }
        }

        //שיחרור רחפן מטעינה
        public void ReleaseDrone(int id,TimeSpan time)
        {
            
            double time1;
            time1= time.TotalMinutes;//זמן בדקות שהרחפן היה בטעינה
            Console.WriteLine(time);
            Console.WriteLine(time1);
            if (DroneToLisToPrint(id).StatusDrone != BO.StatusDrone.InMaintenance)//אם הסטטוס שונה בתחזוקה יש חריגה
                throw new Exception();
            accessIDal.ReleaseDroneFromCharging(id);//שחרור רחפן מטעינה בשכבת הנתונים
            BO.DroneToList drone = BlDrone.Find(p => p.Id == id);//מעדכנת את הרחפן
            drone.StatusDrone = BO.StatusDrone.available;
            drone.StatusBatter += (time1 / 60) * LoadingPrecents;//מצב סוללה
            if (drone.StatusBatter > 100)
                drone.StatusBatter = 100;
          // Console.WriteLine( (time1 / 60) * LoadingPrecents);
            BlDrone.Remove(BlDrone.Find(p => p.Id == id));
            BlDrone.Add(new BO.DroneToList { Id = drone.Id, Model = drone.Model, Weight = drone.Weight, StatusBatter = drone.StatusBatter, StatusDrone = drone.StatusDrone, LocationDrone = drone.LocationDrone, IdParcel = drone.IdParcel });
          

        }

        #endregion

        public BO.Drone GetDrone(int id)//v 
        {
            BO.Drone bodrone=new BO.Drone();
            try
            {
                DO.Drone dodrone = accessIDal.GetDrone(id);//from dalObject
                BO.DroneToList dotolist=BlDrone.Find(p => p.Id == id);//from BL
                bodrone = new BO.Drone()
                {
                    Id = dodrone.Id,
                     Model = dodrone.Model,
                   // Model = "ffff",
                    Weight = (BO.WeightCategories)dodrone.Weight,
                    StatusDrone = dotolist.StatusDrone,
                    StatusBatter = dotolist.StatusBatter,
                    LocationDrone = dotolist.LocationDrone,

                    //PackageInTransfe = GetParcelInTransfer(id)
                };
                if (bodrone.StatusDrone == BO.StatusDrone.delivered)
                    bodrone.PackageInTransfe = GetParcelInTransfer(id);

            }
            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return bodrone;


        }
       
        public void AddDrone(BO.Drone d, int cod)//v
        {
            d.StatusBatter = rand.Next(20, 41);
            d.StatusDrone = BO.StatusDrone.InMaintenance;
            DO.Station station = accessIDal.GetStation(cod);
            d.LocationDrone = new BO.Location { Longitude = station.Longitude, Latitude = station.Latitude };
           
            BlDrone.Add(new BO.DroneToList { Id = d.Id, Model = d.Model, Weight = d.Weight, StatusBatter = d.StatusBatter, StatusDrone = d.StatusDrone, LocationDrone = d.LocationDrone, IdParcel = 0 });
            try
            {

                accessIDal.AddDrone(new DO.Drone { Id = d.Id, Model = d.Model, Weight = (DO.WeightCategories)d.Weight });
                accessIDal.SendDroneToCharge(cod, d.Id);
            }
            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }

        }

        public void UpdateDrone(int id, string name)//v
        {
            DO.Drone c;
            DO.Drone cc;
            try
            {
                c = accessIDal.GetDrone(id);
                if (name != "")
                {
                    cc = new DO.Drone() { Id = c.Id, Model = name, Weight = c.Weight };
                }
                else
                    cc = new DO.Drone() { Id = c.Id, Model = c.Model, Weight = c.Weight };
            }
            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            BO.DroneToList dds = DroneToLisToPrint(id);
            BO.DroneToList dd = new BO.DroneToList() { Id = dds.Id, LocationDrone = dds.LocationDrone, StatusDrone = dds.StatusDrone, IdParcel = dds.IdParcel, Model = name, StatusBatter = dds.StatusBatter, Weight = dds.Weight };
            BlDrone.Remove(BlDrone.Find(p => p.Id == id));
            BlDrone.Add(dd);
            accessIDal.UpdetDrone(cc);
        }


        public BO.DroneToList DroneToLisToPrint(int id)//v
        {
            BO.DroneToList bodroneToList = new BO.DroneToList();
            try
            {
                BO.DroneToList bo = BlDrone.Find(p => p.Id == id);
                DO.Drone dodrone = accessIDal.GetDrone(id);
                bodroneToList.Id = dodrone.Id;
                bodroneToList.Model = dodrone.Model;
                bodroneToList.Weight = (BO.WeightCategories)dodrone.Weight;
                bodroneToList.StatusBatter = bo.StatusBatter;
                bodroneToList.StatusDrone = bo.StatusDrone;
                bodroneToList.LocationDrone = bo.LocationDrone;
                if (bo.StatusDrone == BO.StatusDrone.delivered)//אם הוא במצב עסוק אז יש לו חבילה בעברה ומכניסה את מספר החבילה
                    bodroneToList.IdParcel = GetDrone(id).PackageInTransfe.Id;


            }
            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return bodroneToList;


        }
       
      
        public int BatteryConsumption(double kilometrs, DO.WeightCategories weightcategories)// A function that gets a mileage and calculates how much battery it takes to get there
        {
            if (weightcategories == 0)
                return (int)(kilometrs *LightWeight);
            if (weightcategories == DO.WeightCategories.Medium)
                return (int)(kilometrs * MediumWeight);
            return (int)(kilometrs * HeavyWeight);

        }
        public double BatteryConsumption(double kilometrs)// Overrides the previous function in case the glider is free and no weight category enters
        {
            return kilometrs * Free;
        }
        public IEnumerable< BO.DroneToList> GetDrons()
        {
            return from Drone in BlDrone
                   select Drone;
        }
        public IEnumerable<BO.DroneToList> GetDronsByWeight(BO.WeightCategories weightcategories)
        {

            List < BO.DroneToList > dd= new List<BO.DroneToList>();
            
            for (int i = 0; i < BlDrone.Count(); i++)
            {
                if (BlDrone.ElementAt(i).Weight == weightcategories)
                    dd.Add(BlDrone.ElementAt(i));
            }
            return dd;
        }
        public IEnumerable<BO.DroneToList> GetDronesByPerdicate(Predicate<BO.DroneToList> predicate)
        {
            return from drone in this.GetDrons()
                   where predicate(drone)
                   select drone;
        }
    }

}

