using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    class IBL
    {
        BO.DroneBL GetDroneBL(int id);
        IEnumerable<BO.DroneIBL> GetAllDrone();
        IEnumerable<BO.DroneIBL> GetAllDroneForList(Predicate<BO.DroneIBL> predicate);
    }
}
