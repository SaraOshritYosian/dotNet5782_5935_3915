
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;
namespace BO
{
   public class PackageInTransfer
    {
        public int Id { get; set; }
        public bool PackageMode { get; set; }
        public Priority PriorityParcel { get; set; }
        public WeightCategories Weight { get; set; }
        public CustomerInParcel CustomerInParcelSender { get; set; }//לקוח שולח
        public CustomerInParcel CustomerInParcelTarget { get; set; }//לקוח מקבל
        public Location Collection { get; set; }
        public Location DeliveryDestination { get; set; }
        public double far { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
