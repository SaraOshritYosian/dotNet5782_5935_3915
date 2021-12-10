using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
   public class CustomerToList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pone { get; set; }
        public int NumberOfPackagesSentAndDeliveredCustomer { get; set; }//NumberOfPackagesSentAndDelivered
        public int NumberOfPackagesSentAndNotDeliveredCustomer { get; set; }//NumberOfPackagesSentAndNotDelivered
        public int NumberOfPackagesGetCustomer { get; set; }//NumberOfPackagesGet מספר חבילות קבל
        public int SeveralPackagesOnTheWayToTheCustomerCustomer { get; set; }//SeveralPackagesOnTheWayToTheCustomer מספר חבילות בדרך ללקוח
        public override string ToString()
        {
            return this.ToStringProperty();
        }
        //public override string ToString()
        //{

        //    return base.ToString() + string.Format("the id is:{0,6},\t name is:{1,-3})\n", Id, Name);
        //}

    }
}
