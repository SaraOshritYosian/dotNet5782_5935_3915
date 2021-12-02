using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;



namespace IBL

{
    namespace BO
    {
        public class Customer

        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Pone { get; set; }
            public Location LocationOfCustomer { get; set; }
            public List<DroneInCharge> ListOfPackagesFromTheCustomer { get; set; }//רשימת חבילות אצל הלקוח -מהלקוח
            public List<DroneInCharge> ListOfPackagesToTheCustomer { get; set; }//רשימת חבילות אצל הלקוח- ללקוח
            public override string ToString()
            { 
                 
                   return base.ToString() + string.Format("location:{0,6},\n", LocationOfCustomer) + "the list of the parcels from this customer:\n" + string.Join("\t", ListOfPackagesFromTheCustomer) + "the list of parcels to this customer:\n" + string.Join("*).", ListOfPackagesToTheCustomer);
            }

        }
    }
}