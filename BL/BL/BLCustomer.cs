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
        private BO.ParcelInCustomer ParcelInCustomeWhoSend(int idp)//return parcel to  customer//מקור
        {
           
            BO.ParcelInCustomer p = new BO.ParcelInCustomer()
            {
                Id = idp,
                Weight = (Enums.WeightCategories)accessIDal.GetParcel(idp).Weight,
                Priority = (Enums.Priority)accessIDal.GetParcel(idp).Priority,
                Senderld = new CustomerInParcel() { Id = accessIDal.GetParcel(idp).Senderld, Name = accessIDal.GetCustomer(accessIDal.GetParcel(idp).Senderld).Name }

            };
            return p;
        }
        private BO.ParcelInCustomer ParcelInCustomeWhoGet(int idp)//return parcel from  customer//יעד
        {

            BO.ParcelInCustomer p = new BO.ParcelInCustomer()
            {
                Id = idp,
                Weight = (Enums.WeightCategories)accessIDal.GetParcel(idp).Weight,
                Priority = (Enums.Priority)accessIDal.GetParcel(idp).Priority,
                Senderld = new CustomerInParcel() { Id = accessIDal.GetParcel(idp).Targetld, Name = accessIDal.GetCustomer(accessIDal.GetParcel(idp).Senderld).Name }

            };
            return p;
        }



        private IEnumerable<BO.DroneInCharge> ListParcelToCustomer(int idp)//return list of the parcel to customer
        {
            List<int> ListIdParcelTo = new List<int>();//vv
            ListIdParcelTo = (List<int>)accessIDal.ListSendetParcel(idp);
            List<BO.ParcelToLIst> a = new List<BO.ParcelToLIst>();
            for (int i = 0; i < ListIdParcelTo.Count(); i++)
            {
                ParcelToLIst parcelToLIst = new ParcelToLIst() { Id = ListIdParcelTo[i], Senderld = GetParcel(ListIdParcelTo[i]).Senderld, Targetld = GetParcel(ListIdParcelTo[i]).Targetld,Priority = GetParcel(ListIdParcelTo[i]).Priority,Weight= GetParcel(ListIdParcelTo[i]).Weight,situatinOfParcel= GetParcel(ListIdParcelTo[i]).sis };
                a.Add(parcelToLIst);
            }
            return a;

        }
        private IEnumerable<BO.DroneInCharge> ListParcelfromCustomer(int idS)//return list of the parcel from customer
        {
            List<int> ListDroneId = new List<int>();//vv
            ListDroneId = (List<int>)accessIDal.GetDroneChargByStationListInt(idS);
            List<BO.DroneInCharge> a = new List<BO.DroneInCharge>();
            for (int i = 0; i < ListDroneId.Count(); i++)
            {
                DroneInCharge droneInCharge = new DroneInCharge() { Id = ListDroneId[i], StatusBatter = GetDrone(ListDroneId[i]).StatusBatter };
                a.Add(droneInCharge);
            }
            return a;

        }
        //return a customer
        public BO.Customer GetCustomer(int id)//v x
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
                c.ListOfPackagesFromTheCustomer = (List<DroneInCharge>)(IEnumerable<Parcel>)accessIDal.ListSendParcel(id);
                c.ListOfPackagesToTheCustomer = (IEnumerable<Parcel>)accessIDal.ListTargetParcel(id);

            }
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
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


        public IEnumerable<BO.CustomerToList> GetALLCostumerToList()
        {
            CustomerToList customer = new CustomerToList();
            foreach (IDAL.DO.Customer item in accessIDal.GetAllCustomer())//מיוי הנתונים ב BL מתוך DAL
            {
                customer.Id = item.Id;
                customer.Name = item.Name;
                customer.Pone = item.Pone;
                customer.NumberOfPackagesSentAndDelivered = 0;//Number Of Packages Sent And Delivered
                customer.NumberOfPackagesSentAndNotDelivered = 0;//Number Of Packages SentAnd Not Delivered
                customer.NumberOfPackagesGet = 0;//Number O fPackage sGet
                customer.SeveralPackagesOnTheWayToTheCustomer = 0;//Several Packages On The WayTo The Customer
                foreach (IDAL.DO.Parcel itemParcel in accessIDal.GetAllCustomer())
                {
                    if (itemParcel.Senderld == item.Id)//אם זאת חבילה שנשלחה עי הלקוח 
                    {
                        if (itemParcel.Delivered != default(DateTime))//החבילה כבר סופקה
                            customer.NumberOfPackagesSentAndDelivered++;
                        else
                             if (itemParcel.PichedUp != default(DateTime))//החבילה באמצע משלוח
                            customer.NumberOfPackagesSentAndNotDelivered++;
                    }
                    if (itemParcel.Targetld == item.Id)//חבילה שהתקבלה על ידי הלקוח
                    {
                        if (itemParcel.Delivered != default(DateTime))//החבילה כבר סופקה לו
                            customer.NumberOfPackagesGet++;
                        else
                             if (itemParcel.PichedUp != default(DateTime))// אליו החבילה באמצע משלוח
                            customer.SeveralPackagesOnTheWayToTheCustomer++;
                    }
                }

                customerBl.Add(customer);
            }
            return customerBl;
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
