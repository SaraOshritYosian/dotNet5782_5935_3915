using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;

namespace IBL.BO
{

        public class Drone
        {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories Weight { get; set; }
        public StatusBattery StatusBatter { get; set; }
        public StatusDrone StatusDrone { get; set; }
       
        public PackageInTransfer PackageInTransfe { get; set; }//חבילות בהובלה
        public Location CurrentLocation { get; set; }
    }

}
