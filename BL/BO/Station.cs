using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
    {
        public class Station
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public Location LocationStation { get; set; }
            public int ChargeSlotsFree { get; set; }
            public IEnumerable<DroneInCharge> DroneInChargeList { get; set; }
        }
}
