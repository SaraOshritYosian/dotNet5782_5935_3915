using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
   public class StationToList
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int ChargeSlotsFree { get; set; }
        public int ChargeSlotsNotFree { get; set; }
    }
}
