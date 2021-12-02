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

        #region DroneGarge
        public void SendingDroneforCharging(int droneId)
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

                    minstation = min...(GetDroneToList(droneId).LocationDrone);
                    //חישוב מרחק בין התחנה לרחפן
                    kilometer = DistanceTo(accessIDal.GetStation(minstation.Id).Latitude, accessIDal.GetStation(minstation.Id).Longitude, GetDroneToList(droneId).LocationDrone.Latitude, GetDroneToList(droneId).LocationDrone.Longitude);
                    battery = BatteryConsumption(kilometer, GetDroneToList(droneId).Weight);//שמירת כמות הבטריה שמתבזבזת
                    if (battery < GetDroneToList(droneId).StatusBatter)//צריך לבדוק אם הסוללה הנדרשת מספיקה לסוללה שיש לי ברחפן
                        accessIDal.SendDroneTpCharge(stationId, droneId);
                    DroneToList drone = GetDroneToList(droneId);//מכאן נשנה את המצב של הרחפן והבטריה ברשימה של ה bl
                    drone.ButerryStatus -= battery;
                    drone.DroneStatuses = DroneStatuses.maintenance;
                    Location l = new Location();
                    l.Lattitude = dl.GetStation(stationId).Lattitude;
                    l.Longitude = dl.GetStation(stationId).Longitude;
                    drone.ThisLocation = l;
                    dronesBl.Add(drone);
                    dronesBl.Remove(GetDroneToList(droneId));
                }
            }
            catch (IDAL.DO.MissingIdException ex)//  חריגה לא נכונה !!!!!!!!! לעשות חדשה
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }

        }




        public void SendDroneToCharge(int idDrone)//שליחת רחפן לטעינה
        {
            for (int i = 0; i < BLDrones.Count; i++)
            {
                if (BLDrones[i].Id == idDrone)
                {
                    DroneToList drone = BLDrones[i];//.Find(x => x.Id == idDrone);
                    if (drone == default)
                        throw new CantSendToChargeException();
                    if (drone.StatusDrone != BO.Enums.StatusDrone.available)
                        throw new CantSendToChargeException("ERROR This is not free drone");//not good
                    List<IDAL.DO.Station> dalListStation = accessIDal.GetAllStationBy(x => x.ChargeSlots > 0).ToList();
                    List<BO.Station> blStation = new List<BO.Station>();
                    foreach (var item in dalListStation)
                    {
                        blStation.Add(new BO.Station { Name = item.Name, Id = item.Id, ChargeSlotsFree = item.ChargeSlots, LocationStation = new Location { Latitude = item.Latitude, Longitude = item.Longitude } });

                    }

                    if (!blStation.Any())
                        throw new CantSendToChargeException("ERROR This is not free drone");//not good
                    double far = minDistance(blStation, drone.LocationDrone).Item2;
                    if (drone.StatusBatter - far * Free < 0)
                        throw new CantSendToChargeException("ERROR To the drone not have enaugh batteryto go ");
                    drone.StatusBatter -= far * Free;
                    drone.LocationDrone = minDistance(blStation, drone.LocationDrone).Item1;
                    
                }
            }
        }


        #endregion
        public BO.Drone GetDroneToList(int id)
        {
            BO.Drone bodroneToList = new BO.Drone();
            try
            {
                IDAL.DO.Drone dodrone = accessIDal.GetDrone(id);
                bodroneToList.Id = dodrone.Id;
                bodroneToList.Model = dodrone.Model;
                bodroneToList.Weight = (BO.Enums.WeightCategories)dodrone.Weight;


            }
            catch (DroneDoesNotExistException)
            {
                throw new DroneDoesNotExistException();
            }
            return bodroneToList;


        }

        public BO.Drone GetDrone(int id)
        {
            BO.Drone bodrone = new BO.Drone();
            try
            {
                IDAL.DO.Drone dodrone = accessIDal.GetDrone(id);
                bodrone.Id = dodrone.Id;
                bodrone.Model = dodrone.Model;
                bodrone.Weight = (BO.Enums.WeightCategories)dodrone.Weight;


            }
            catch (DroneDoesNotExistException )
            {
                throw new DroneDoesNotExistException();
            }
            return bodrone;


        }
        public IEnumerable<BO.Drone> GetAllDrone()
        {
            return from doDrone in accessIDal.GetAllDrone()
                   select new BO.Drone()
                   {
                       Id = doDrone.Id,
                       Model = doDrone.Model,
                       Weight = (BO.Enums.WeightCategories)doDrone.Weight
                   };
        }
        public IEnumerable<BO.Drone> DroneList()
        {
            return from item in accessIDal.ddroneList()
                   select GetDrone(item.Id);
        }

        public IEnumerable<BO.Drone> GetDroneToList()
        {
            return from doDrone in accessIDal.GetAllDrone()
                   select new BO.Drone()
                   {
                       Id = doDrone.Id,
                       Model = doDrone.Model,
                       Weight = (BO.Enums.WeightCategories)doDrone.Weight
                   };
        }
      
        public void AddDrone(BO.Drone d, int cod)
        {
            d.StatusBatter = rand.Next(20, 41);
            d.StatusDrone = BO.Enums.StatusDrone.InMaintenance;
            IDAL.DO.Station station = accessIDal.GetStation(cod);
            d.LocationDrone = new Location { Longitude = station.Longitude, Latitude = station.Latitude };
            accessIDal.AddDrone(new IDAL.DO.Drone { Id=d.Id,Model=d.Model,Weight=(IDAL.DO.WeightCategories)d.Weight});
            accessIDal.SendDroneToCharge(d.Id, cod);
            BLDrones.Add(new DroneToList { Id = d.Id, Model = d.Model, Weight = d.Weight, StatusBatter = d.StatusBatter, StatusDrone = d.StatusDrone, CurrentLocation = d.CurrentLocation, IdParcel = 0 });


        }

        void UpdateDrone(BO.Drone drone,)//לא גמור
        {

            try
            {

            }
            catch (DroneDoesNotExistException )
            {
                throw new DroneDoesNotExistException();
            }

        }
        //שיחרור רחפן מטעינה
        public void ReleaseDrone(int id,double time)//
        {
            int index = BLDrones.FindIndex(x => x.Id == id);
            BLDrones[index].StatusBatter += time * accessIDal.ElectricityUse().ElementAt(4);//4?
            if(BLDrones[index].StatusBatter>100)
            {
                BLDrones[index].StatusBatter = 100;
            }
            BLDrones[index].StatusDrone = BO.Enums.StatusDrone.available;
            accessIDal.ReleaseDroneFromCharging(id);
           }   

     

        //שליחת רחפן לטעינה
        public void SendingDroneToCharging(int droneId)
        {

            double kilometer, battery = 0;
            int stationId = 0;
            StationToList minstation;
            try
            {
                if (GetDroneToList(droneId).DroneStatuses != DroneStatuses.available)//אם הסטטוס שונה מפנוי יש חריגה
                    throw new UnsuitableDroneMode(GetDroneToList(droneId).DroneStatuses, "Drone");
                else
                {

                    minstation = MinFarToStation(GetDroneToList(droneId).ThisLocation);
                    //חישוב מרחק בין התחנה לרחפן
                    kilometer = DistanceTo(dl.GetStation(minstation.idnumber).Lattitude, dl.GetStation(minstation.idnumber).Longitude, GetDroneToList(droneId).ThisLocation.Lattitude, GetDroneToList(droneId).ThisLocation.Longitude);
                    battery = BatteryConsumption(kilometer, GetDroneToList(droneId).Weightcategories);//שמירת כמות הבטריה שמתבזבזת
                    if (battery < GetDroneToList(droneId).ButerryStatus)//צריך לבדוק אם הסוללה הנדרשת מספיקה לסוללה שיש לי ברחפן
                        dl.SendDroneTpCharge(stationId, droneId);
                    DroneToList drone = GetDroneToList(droneId);//מכאן נשנה את המצב של הרחפן והבטריה ברשימה של ה bl
                    drone.ButerryStatus -= battery;
                    drone.DroneStatuses = DroneStatuses.maintenance;
                    Location l = new Location();
                    l.Lattitude = dl.GetStation(stationId).Lattitude;
                    l.Longitude = dl.GetStation(stationId).Longitude;
                    drone.ThisLocation = l;
                    dronesBl.Add(drone);
                    dronesBl.Remove(GetDroneToList(droneId));
                }
            }
            catch (IDAL.DO.MissingIdException ex)//  חריגה לא נכונה !!!!!!!!! לעשות חדשה
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }



    }
}
