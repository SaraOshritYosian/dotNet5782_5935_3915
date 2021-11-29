using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL.DO;
//using IBL.BO;

namespace IBL
{
    public partial class BL
    {
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
            catch (IDAL.DO.DroneDoesNotExistException ex)
            {
                throw new DroneDoesNotExistException(ex);
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
                       Weight = (BO.Enums.WeightCategories)doDrone.Weigh
                   }


        }
        public void AddDrone(BO.Drone drone)
        {
            Random rand = new Random();

            IDAL.DO.Drone newD = new IDAL.DO.Drone()
            {
                Id = drone.Id,
                Model = drone.Model,
                Weight = (WeightCategories)drone.Weight,
                StatusBatter = rand.NextDouble() * 40 + 20,//להגריל 20%-40%
                StatusDrone = BO.Enums.StatusDrone.InMaintenance,
                bodrone.CurrentLocation =
            };

            try
            {
                accessIDal.addDrone(newD);

            }
            catch (IDAL.DO.DroneDoesNotExistException ex)
            {
                throw new DroneDoesNotExistException(ex);
            }

        }
        void UpdateDrone(BO.Drone drone)
        {

            try
            {

            }
            catch (IDAL.DO.DroneDoesNotExistException ex)
            {
                throw new DroneDoesNotExistException(ex);
            }

        }
    }
}
