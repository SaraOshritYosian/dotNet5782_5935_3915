using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL
    {

        #region Dronecharge
        public void SendingDroneToCharging(int droneId)//x
        {

            double kilometer, battery = 0;
            int stationId = 0;
            StationToList minstation;
            try
            {
                if (GetDroneToList(droneId).StatusDrone != BO.Enums.StatusDrone.available)//אם הסטטוס שונה מפנוי יש חריגה
                    throw new Exception();//(GetDroneToList(droneId).DroneStatuses, "Drone");
                else
                {

                    minstation = minDistance...(GetDroneToList(droneId).LocationDrone);
                    //חישוב מרחק בין התחנה לרחפן
                    kilometer = DistanceTo(accessIDal.GetStation(minstation.Id).Latitude, accessIDal.GetStation(minstation.Id).Longitude, GetDroneToList(droneId).LocationDrone.Latitude, GetDroneToList(droneId).LocationDrone.Longitude);
                    battery = BatteryConsumption(kilometer, GetDroneToList(droneId).Weight);// כמות הבטריה שמתבזבזת
                    if (battery < GetDroneToList(droneId).StatusBatter)// לבדוק אם הסוללה הנדרשת מספיקה לסוללה שיש לי ברחפן
                        accessIDal.SendDroneTpCharge(stationId, droneId);
                    DroneToList drone = GetDroneToList(droneId);//מכאן נשנה את המצב של הרחפן והבטריה ברשימה של ה bl
                    drone.StatusBatter -= battery;
                    drone.StatusDrone = BO.Enums.StatusDrone.InMaintenance;
                    Location l = new Location();

                    l.Latitude = accessIDal.GetStation(stationId).Latitude;
                    l.Longitude = accessIDal.GetStation(stationId).Longitude;
                    drone.LocationDrone = l;
                    BLDrones.Add(drone);
                    BLDrones.Remove(GetDroneToList(droneId));
                }
            }
            catch (IDAL.DO.Excptions)//  חריגה לא נכונה !!!!!!!!! לעשות חדשה
            {
                throw new Exception();
            }

        }

        //שיחרור רחפן מטעינה
        public void ReleaseDrone(int id, double time)//v
        {
            int index = BLDrones.FindIndex(x => x.Id == id);
            BLDrones[index].StatusBatter += time * accessIDal.ElectricityUse().ElementAt(4);//4?
            if (BLDrones[index].StatusBatter > 100)
            {
                BLDrones[index].StatusBatter = 100;
            }
            BLDrones[index].StatusDrone = BO.Enums.StatusDrone.available;
            accessIDal.ReleaseDroneFromCharging(id);
        }

        #endregion
        public BO.DroneToList GetDroneToList(int id)//x
        {
            BO.DroneToList bodroneToList = new BO.DroneToList();
            try
            {
                IDAL.DO.Drone dodrone = accessIDal.GetDrone(id);
                bodroneToList.Id = dodrone.Id;
                bodroneToList.Model = dodrone.Model;
                bodroneToList.Weight = (BO.Enums.WeightCategories)dodrone.Weight;
                bodroneToList.StatusBatter =

            }
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return bodroneToList;


        }

        public BO.Drone GetDrone(int id)//v
        {
            BO.Drone bodrone = new BO.Drone();
            try
            {
                IDAL.DO.Drone dodrone = accessIDal.GetDrone(id);
                bodrone.Id = dodrone.Id;
                bodrone.Model = dodrone.Model;
                bodrone.Weight = (BO.Enums.WeightCategories)dodrone.Weight;


            }
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return bodrone;


        }
        public IEnumerable<BO.Drone> GetAllDrone()//v
        {
            return from doDrone in accessIDal.GetAllDrone()
                   select new BO.Drone()
                   {
                       Id = doDrone.Id,
                       Model = doDrone.Model,
                       Weight = (BO.Enums.WeightCategories)doDrone.Weight
                   };
        }

        public void PrintDronesList()//הצגת רשימת הרחפנים show drone list
        {
            foreach (Drone dr in accessIDal.GetAllDrone().ToList)
            {
                Console.WriteLine(dr.ToString());
            }
        }
        public IEnumerable<BO.Drone> GetDroneToList()//x
        {
            return from doDrone in accessIDal.GetAllDrone()
                   select new BO.Drone()
                   {
                       Id = doDrone.Id,
                       Model = doDrone.Model,
                       Weight = (BO.Enums.WeightCategories)doDrone.Weight
                   };
        }

        public void AddDrone(BO.Drone d, int cod)//v
        {
            d.StatusBatter = rand.Next(20, 41);
            d.StatusDrone = BO.Enums.StatusDrone.InMaintenance;
            IDAL.DO.Station station = accessIDal.GetStation(cod);
            d.LocationDrone = new Location { Longitude = station.Longitude, Latitude = station.Latitude };
            accessIDal.AddDrone(new IDAL.DO.Drone { Id = d.Id, Model = d.Model, Weight = (IDAL.DO.WeightCategories)d.Weight });
            accessIDal.SendDroneToCharge(d.Id, cod);
            BLDrones.Add(new DroneToList { Id = d.Id, Model = d.Model, Weight = d.Weight, StatusBatter = d.StatusBatter, StatusDrone = d.StatusDrone, CurrentLocation = d.CurrentLocation, IdParcel = 0 });


        }

        public void UpdateDrone(int id, string name)//v
        {
            BO.Drone c = new BO.Drone();
            try
            {
                c = GetDrone(id);
                if (name != "")
                {
                    c.Model = name;
                }
            }
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }

        }
       
    }

}

