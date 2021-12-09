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
        private BO.ParcelInCustomer ParcelInCustomeWhoSend(int idp)//return parcel to  customer//מי ששלח-מקור
        {
           
            BO.ParcelInCustomer p = new BO.ParcelInCustomer()//לקוח בחבילה זה מי שמקבל
            {
                Id = idp,
                Weight = (Enums.WeightCategories)accessIDal.GetParcel(idp).Weight,
                Priority = (Enums.Priority)accessIDal.GetParcel(idp).Priority,
                StatusParcel=StatuseParcelKnow(idp),
                Senderld = new CustomerInParcel() { Id = accessIDal.GetParcel(idp).Targetld, Name = accessIDal.GetCustomer(accessIDal.GetParcel(idp).Targetld).Name }
                
            };
            return p;
        }
        private BO.ParcelInCustomer ParcelInCustomeWhoGet(int idp)//return parcel from  customer//מי שמקבל
        {

            BO.ParcelInCustomer p = new BO.ParcelInCustomer()//לקוח בחבילה זה מי ששלח
            {
                Id = idp,
                Weight = (Enums.WeightCategories)accessIDal.GetParcel(idp).Weight,
                Priority = (Enums.Priority)accessIDal.GetParcel(idp).Priority,
                StatusParcel = StatuseParcelKnow(idp),
                Senderld = new CustomerInParcel() { Id = accessIDal.GetParcel(idp).Senderld, Name = accessIDal.GetCustomer(accessIDal.GetParcel(idp).Senderld).Name }

            };
            return p;
        }



        private IEnumerable<BO.ParcelInCustomer> ListParcelToCustomer(int idc)//return list of the parcel to customer//ת"ז של השולח 
        {
            List<int> ListIdParcelTo = new List<int>();//vv
            ListIdParcelTo = (List<int>)accessIDal.ListSendetParcel(idc);
            List<BO.ParcelInCustomer> a = new List<BO.ParcelInCustomer>();
            for (int i = 0; i < ListIdParcelTo.Count(); i++)
            {
                a.Add(ParcelInCustomeWhoSend(ListIdParcelTo[i]));
            }
            return a;

        }
        private IEnumerable<BO.ParcelInCustomer> ListParcelFromCustomers(int idc)//return list of the parcel to customer//מקבל ת"ז של מי שצריך לקבל 
        {
            List<int> ListIdParcelTo = new List<int>();//vv
            ListIdParcelTo = (List<int>)accessIDal.ListTargetParcel(idc);// מקבל רשימה של ת"ז של משלוחים מי שיקבל ע"פ ת"ז של לקוח מסויים
            List<BO.ParcelInCustomer> a = new List<BO.ParcelInCustomer>();
            for (int i = 0; i < ListIdParcelTo.Count(); i++)
            {
                a.Add(ParcelInCustomeWhoGet(ListIdParcelTo[i]));//שולח את המספר של המשלוח
            }
            return a;

        }
        //return a customer
        public BO.Customer GetCustomer(int id)//v 
        {
            BO.Customer c = new BO.Customer();
            try
            {
                IDAL.DO.Customer customer = accessIDal.GetCustomer(id);
                c.Id = customer.Id;
                c.Name = customer.Name;
                c.Pone = customer.Pone;
                c.LocationOfCustomer.Latitude = customer.Lattitude;
                c.LocationOfCustomer.Longitude = customer.Longitude;
                c.ListOfPackagesFromTheCustomer = (List<ParcelInCustomer>)ListParcelFromCustomers(id);// רשימה של מישלוחים שמקבל
                c.ListOfPackagesToTheCustomer = (List<ParcelInCustomer>)ListParcelToCustomer(id);//רשימה של משלוחים ששולח

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
            IDAL.DO.Customer c; ;
            try
            {
                c =accessIDal.GetCustomer(id);
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

            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
        }


        public BO.CustomerToList CostumerToListToPrint(int idc)
        {
            BO.CustomerToList c = new BO.CustomerToList();
            try
            {
                IDAL.DO.Customer customer = accessIDal.GetCustomer(idc);
                c.Id = customer.Id;
                c.Name = customer.Name;
                c.Pone = customer.Pone;
                c.NumberOfPackagesSentAndDeliveredCustomer = NumberOfPackagesSentAndDelivered(idc);
                c.NumberOfPackagesSentAndNotDeliveredCustomer = NumberOfPackagesSentButNotDelivered(idc);
                c.NumberOfPackagesGetCustomer = NumberOfPackagesHeReceived(idc);
                c.SeveralPackagesOnTheWayToTheCustomerCustomer = SeveralPackagesOnTheWayToTheCustomer(idc);
            }
            catch (IDAL.DO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return c;
        }//v

        public void PrintCustomersList()//print all dtone
        {
            IEnumerable<IDAL.DO.Customer> a = accessIDal.GetAllCustomer();
            for (int i = 0; i < a.Count(); i++)
            {
                Console.WriteLine(CostumerToListToPrint(a.ElementAt(i).Id));
            }
        }
        public void PrintCustomerById(int ids)//print drone get id of drone
        {
            Console.WriteLine(GetCustomer(ids).ToString());
        }
        private int NumberOfPackagesSentAndDelivered(int idc)//מחזיר מספר חבילות ששלח וסופקו מקבל ת"ז של הלקוח ששלח
        {
            int mone = 0;
            BO.Customer c = GetCustomer(idc);
            IEnumerable<BO.ParcelInCustomer> a = c.ListOfPackagesToTheCustomer;//רשימה של משלוחים ששלח
            for(int i = 0; i < a.Count(); i++)
            {
                if (a.ElementAt(i).StatusParcel == Enums.StatusParcel.provided)//אם מצב החבילה נוצר אז להוסיף את הכצות של המונה
                    mone++;
            }
            return mone;
        }
        private int NumberOfPackagesSentButNotDelivered(int idc)//מחזיר מספר חבילות ששלח ולא וסופקו מקבל ת"ז של הלקוח ששלח
        {
            int mone = 0;
            BO.Customer c = GetCustomer(idc);
            IEnumerable<BO.ParcelInCustomer> a = c.ListOfPackagesToTheCustomer;//רשימה של משלוחים ששלח
            for (int i = 0; i < a.Count(); i++)
            {
                if (a.ElementAt(i).StatusParcel != Enums.StatusParcel.provided)//אם מצב החבילה נוצר אז להוסיף את הכצות של המונה
                    mone++;
            }
            return mone;
        }
        private int NumberOfPackagesHeReceived(int idc)//מחזיר מספר חבילות שקיבל הפונקציה מקבלת את הת"ז של הלקוח שהוא צריך לקבל את ההזמנות
        {
            int mone = 0;
            BO.Customer c = GetCustomer(idc);
            IEnumerable<BO.ParcelInCustomer> a = c.ListOfPackagesFromTheCustomer;//רשימה של משלוחים שצריך לקבל
            
            for (int i = 0; i < a.Count(); i++)
            {
                if (a.ElementAt(i).StatusParcel == Enums.StatusParcel.provided)//אם מצב החבילה נוצר אז להוסיף את הכצות של המונה
                    mone++;
            }
            return mone;
        }
        private int SeveralPackagesOnTheWayToTheCustomer(int idc)//מחזיר מספר חבילות שבדרך אילו הפו' מקבלת הת"ז של הלקוח הנ"ל
        {
            int mone = 0;
            BO.Customer c = GetCustomer(idc);
            IEnumerable<BO.ParcelInCustomer> a = c.ListOfPackagesFromTheCustomer;//
            
            for (int i = 0; i < a.Count(); i++)
            {
                if (a.ElementAt(i).StatusParcel != Enums.StatusParcel.provided)//אם מצב החבילה נוצר אז להוסיף את הכצות של המונה
                    mone++;
            }
            return mone;
        }
    }
    
}
