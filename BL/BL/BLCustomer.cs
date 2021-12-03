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
        //return a customer
        public BO.Customer GetCustomer(int id)//
        {
            BO.Customer c = new BO.Customer();
            try
            {
                IDAL.DO.Customer customer = accessIDal.GetCustomer(id);
                c.Id = customer.Id;
                c.Name = customer.Name;
                c.Pone = customer.Pone;
                c.LocationOfCustomer.Latitude = customer.Lattitude;
                c.LocationOfCustomer.Latitude = customer.Lattitude;
                c.ListOfPackagesFromTheCustomer = (IEnumerable<Parcel>)accessIDal.ListSendParcel(id);
                c.ListOfPackagesToTheCustomer = (IEnumerable<Parcel>)accessIDal.ListTargetParcel(id);

            }
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions( ex.Message);
            }
            return c;
        }
        //update customer by id or name or phone ot more
        public void UpdateCustomer(int id, string name, string phone)//v
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
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }

        }
        //add customer
        public void AddCustomer(Customer customer)//v
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
        public IEnumerable<BO.Customer> CustomerList()//x
        {
            return from item in accessIDal.ccustomerList()
                   select GetCustomer(item.Id);
        }

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
