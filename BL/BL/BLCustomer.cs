using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;



namespace IBL
{
    public partial class BL
    {
        
        public void UpdateCustomer(int id, string name, string phone)
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
                    c.Pone = phone;
                }



            }
            catch ()
            {

            }
        }
        public void AddCustomer(Customer customer)
        {
            IDAL.DO.Customer customer1 = new IDAL.DO.Customer() { Id = customer.Id, Name = customer.Name, Pone = customer.Pone, Longitude = customer.LocationOfCustomer.Longitude, Lattitude = customer.LocationOfCustomer.Latitude };
            try
            {
                accessIDal.AddCustomer(customer1);
            }

            catch (IDAL.DO.Excptions)
            {
                throw new BO.AlreadyExistException();
            }
        }
        //public IEnumerable<BO.Customer> CustomerList()
        //{
        //    return from item in accessIDal.ccustomerList()
        //           select GetCustomer(item.Id);
        //}

        public void PrintCustomer(int customerId)//תצוגת לקוח shoe customer details by id
        {
            foreach (Customer customer in BL)
            {
                if (customer.Id == customerId)
                {
                    Console.WriteLine(customer.ToString());
                    break;
                }
            }
        }

    }  
}
