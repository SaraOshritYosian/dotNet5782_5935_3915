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
            public List<ParcelInCustomer> ListOfPackagesFromTheCustomer { get; set; }//רשימת חבילות אצל הלקוח -מהלקוח
            public List<ParcelInCustomer> ListOfPackagesToTheCustomer { get; set; }//רשימת חבילות אצל הלקוח- ללקוח
                                                                                   //public override string ToString()
                                                                                   //  {
                                                                                   //       return this.ToStringProperty();
                                                                                   //   }
            public override string ToString() => this.ToStringProperty();
        }
    }
}