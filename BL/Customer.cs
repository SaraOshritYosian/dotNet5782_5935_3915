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
            public int Name { get; set; }
            public string Pone { get; set; }
            public Location LocationBL { get; set; }
            public List<DroneInCharge> ListOfPackagesFromTheCustomer { get; set; }//רשימת חבילות אצל הלקוח -מהלקוח
            public List<DroneInCharge> ListOfPackagesToTheCustomer { get; set; }//רשימת חבילות אצל הלקוח- מהלקוח

        }
    }
}