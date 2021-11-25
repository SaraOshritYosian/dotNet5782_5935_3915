
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IBL
{
    namespace BO
    {
        public class StationBL
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public Location LocationBL { get; set; }
            public int ChargeSlotsFree { get; set; }
            public List<DroneInCharge> DroneInChargeList { get; set; }
        }
    }
}