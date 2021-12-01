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
        public void SendDroneToCharge(int idDrone)//שליחת רחפן לטעינה
        {
            DroneToList drone = BLDrones.Find(x => x.Id == idDrone);
            if (drone == default)
                throw new CantSendToChargeException();
            if (drone.StatusDrone != BO.Enums.StatusDrone.available)
                throw new CantSendToChargeException("ERROR This is not free drone");//not good
            List<IDAL.DO.Station> dalListStation = accessIDal.GetAllStationBy(x => x.ChargeSlotsFree > 0).ToList();
            List<BO.Station> blStation = new List<BO.Station>();
            foreach (var item in dalListStation)
            {
                blStation.Add(new BO.Station { Name = item.Name, Id = item.Id, ChargeSlotsFree = item.ChargeSlots, LocationBL = new Location { Latitude = item.Latitude, Longitude = item.Longitude } });

            }

            if (!blStation.Any())
                throw new CantSendToChargeException("ERROR This is not free drone");//not good
            double far = minDistance(blStation, drone.CurrentLocation).Item2;
            if (drone.StatusBatter - far * Free < 0)
                throw new CantSendToChargeException("ERROR To the drone not have enaugh batteryto go ");
            drone.StatusBatter -= far * Free;
            drone.CurrentLocation = minDistance(blStation, drone.CurrentLocation).Item1;
            //יש עוד אני לא יודעת מההה
        }


        #endregion

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
            catch (DroneDoesNotExistException ex)
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
        public void AddDrone(BO.Drone drone, int cod)
        {
            Random rand = new Random();

            IDAL.DO.Drone newD = new IDAL.DO.Drone()
            {
                Id = drone.Id,
                Model = drone.Model,
                Weight = (WeightCategories)drone.Weight,
                sta = 20 + rand.NextDouble() * 40,//להגריל 20%-40%
                StatusDrone = BO.Enums.StatusDrone.InMaintenance,

                bodrone.CurrentLocation =//מיקום של התחנה ששם הו אהלך להטען



            };

            try
            {
                accessIDal.AddDrone(newD);

            }
            catch (DroneChargDoesNotExistException ex)
            {
                throw new DroneDoesNotExistException();
            }

        }

        void UpdateDrone(BO.Drone drone)
        {

            try
            {

            }
            catch (DroneDoesNotExistException ex)
            {
                throw new DroneDoesNotExistException();
            }

        }

        public IEnumerable<BO.Drone> DroneList()
        {
            return from item in accessIDal.ddroneList()
                   select GetDrone(item.Id);
        }

    }
}
