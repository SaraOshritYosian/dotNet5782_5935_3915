using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;
namespace IBL.BO
{
    public class ParcelInCustomer
    {
        public int Id { get; set; }
        public WeightCategories Weight { get; set; }
        public Priority Priority { get; set; }
        public StatusParcel StatusParcel { get; set; }
        public CustomerInParcel Senderld { get; set; }//לקוח בחבילה
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
