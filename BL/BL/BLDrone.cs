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
        public void AddDrone(Drone drone)
        {

            IDAL.DO.Drone newD = new IDAL.DO.Drone()
            {
                Id = drone.Id;
            Model = drone.Model;
            Weight = drone.Weight;
        };

        try{
           accessIDal.AddDrone(newD);
            }
    catch(IDAL.DO.DroneDoesNotExistException)
        {
         throw new DroneDoesNotExistException(); }

  }
}
