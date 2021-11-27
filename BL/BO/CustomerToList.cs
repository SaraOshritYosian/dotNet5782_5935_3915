using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class CustomerToList
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string Pone { get; set; }
        public int NumberOfPackagesSentAndDelivered { get; set; }//NumberOfPackagesSentAndDelivered
        public int NumberOfPackagesSentAndNotDelivered { get; set; }//NumberOfPackagesSentAndNotDelivered
        public int NumberOfPackagesGet { get; set; }//NumberOfPackagesGet
        public int SeveralPackagesOnTheWayToTheCustomer { get; set; }//SeveralPackagesOnTheWayToTheCustomer
       
    }
}
