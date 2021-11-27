using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class DroneToList

    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories Weight { get; set; }
        public StatusBattery StatusBatter { get; set; }
        public StatusDrone StatusDrone { get; set; }

        public int IdParcel { get; set; }//אם יש
        public Location CurrentLocation { get; set; }
    }
}
