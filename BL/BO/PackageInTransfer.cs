
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;
namespace IBL.BO
{
   public class PackageInTransfer
    {
        public int Id { get; set; }
        public bool PackageMode { get; set; }
        public Priority Priority { get; set; }
        public WeightCategories Weight { get; set; }
        public CustomerInParcel Senderld { get; set; }//לקוח שולח
        public CustomerInParcel Targetld { get; set; }//לקוח מקבל
        public Location Collection { get; set; }
        public Location DeliveryDestination { get; set; }
        public double far { get; set; }

    }
}
