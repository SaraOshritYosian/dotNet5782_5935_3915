using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL.DO;


namespace IBL
{
   public partial class BLCustomer
    {
        public BO.Customer GetCustomer(int id)
        {
           BO.Customer? per = DataSource.customerList.Find(p => p.Id == id);
            if (per is null)
                return (Customer)per;
            else
                throw new CsustomerDoesNotExistException($"bad Customer id: {id}");//שרה הקישקוש של השם זה השם של הזריקה
        }
        public BO.Customer DelCustomer(int id)
        {
            BO.Customer? per = DataSource.customerList.Find(p => p.Id == id);
            if (per is null)
                return (Customer)per;
            else
                throw new CsustomerDoesNotExistException($"bad Customer id: {id}");//שרה הקישקוש של השם זה השם של הזריקה
        }

        public void UpdateCustomer(int id, string name, string phone )
        {
            BO.Customer c = new BO.Customer();
            try
            {
                c = GetCustomer(id);
                if (name != "")
                {
                    c.Name = name;
                }
                if (phone != "")
                {
                    c.phone = phone;
                }
                BLCustomer.


            }
       public void AddCustomer(int id)
        {
            BO.DroneBL boCustomer = new BO.DroneBL;
            try
            {
                DO.
            }

        }

    }
}
