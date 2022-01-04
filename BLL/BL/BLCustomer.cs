using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;


namespace BL
{
    sealed partial class BL :IBL
    {
        private ParcelInCustomer ParcelInCustomeWhoSend(int idp)//return parcel to  customer//מי ששלח-מקור
        {
           
            ParcelInCustomer p = new ParcelInCustomer()//לקוח בחבילה זה מי שמקבל
            {
                Id = idp,
                Weight = (WeightCategories)accessIDal.GetParcel(idp).Weight,
                Priority = (Priority)accessIDal.GetParcel(idp).Priority,
                StatusParcel=StatuseParcelKnow(idp),
                Senderld = new CustomerInParcel() { Id = accessIDal.GetParcel(idp).Targetld, Name = accessIDal.GetCustomer(accessIDal.GetParcel(idp).Targetld).Name }
                
            };
            return p;
        }
        private ParcelInCustomer ParcelInCustomeWhoGet(int idp)//return parcel from  customer//מי שמקבל
        {

            ParcelInCustomer p = new ParcelInCustomer()//לקוח בחבילה זה מי ששלח
            {
                Id = idp,
                Weight = (WeightCategories)accessIDal.GetParcel(idp).Weight,
                Priority = (Priority)accessIDal.GetParcel(idp).Priority,
                StatusParcel = StatuseParcelKnow(idp),
                Senderld = new CustomerInParcel() { Id = accessIDal.GetParcel(idp).Senderld, Name = accessIDal.GetCustomer(accessIDal.GetParcel(idp).Senderld).Name }

            };
            return p;
        }
        //


        private IEnumerable<ParcelInCustomer> ListParcelToCustomer(int idc)//return list of the parcel to customer//ת"ז של השולח 
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
        private IEnumerable<ParcelInCustomer> ListParcelFromCustomers(int idc)//return list of the parcel to customer//מקבל ת"ז של מי שצריך לקבל 
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
        public Customer GetCustomer(int id)//v 
        {
            Customer c = new Customer();
            try
            {
                Customer customer = accessIDal.GetCustomer(id);
                c.Id = customer.Id;
                c.Name = customer.Name;
                c.Pone = customer.Pone;
                Location newC = new Location() { Latitude = customer.Lattitude, Longitude = customer.Longitude };
                c.LocationOfCustomer=newC;
                c.ListOfPackagesFromTheCustomer = (List<ParcelInCustomer>)ListParcelFromCustomers(id);// רשימה של מישלוחים שמקבל
                c.ListOfPackagesToTheCustomer = (List<ParcelInCustomer>)ListParcelToCustomer(id);//רשימה של משלוחים ששולח

            }
            catch (Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return c;
        }
        //update customer by id or name or phone ot more
        public void UpdateCustomer(int id, string name, string phone)//v
        {
            Customer c=accessIDal.GetCustomer(id);
            Customer cc= new Customer();
            cc.Id = c.Id;
            cc.Lattitude = c.Lattitude;
            cc.Longitude = c.Longitude;
            try
            {
                c =accessIDal.GetCustomer(id);
                if (name != "")
            {
                cc.Name = name;
            }
            if (phone != "")
            {
                cc.Pone = phone;
            }
                accessIDal.UpdetCustomer(cc);


            }
            catch (Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }

        }
        //add customer
        public void AddCustomer(Customer customer)//v
        {
            Customer customer1 = new Customer() { Id = customer.Id, Name = customer.Name, Pone = customer.Pone, Longitude = customer.LocationOfCustomer.Longitude, Lattitude = customer.LocationOfCustomer.Latitude };
            try
            {
                accessIDal.AddCustomer(customer1);
            }

            catch (Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
        }


        public BO.CustomerToList CostumerToListToPrint(int idc)//
        {
            BO.CustomerToList c = new BO.CustomerToList();
            try
            {
               Customer customer = accessIDal.GetCustomer(idc);
                c.Id = customer.Id;
                c.Name = customer.Name;
                c.Pone = customer.Pone;
                c.NumberOfPackagesSentAndDeliveredCustomer = NumberOfPackagesSentAndDelivered(idc);
                c.NumberOfPackagesSentAndNotDeliveredCustomer = NumberOfPackagesSentButNotDelivered(idc);
                c.NumberOfPackagesGetCustomer = NumberOfPackagesHeReceived(idc);
                c.SeveralPackagesOnTheWayToTheCustomerCustomer = SeveralPackagesOnTheWayToTheCustomer(idc);
            }
            catch (Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return c;
        }//v

        private int NumberOfPackagesSentAndDelivered(int idc)//מחזיר מספר חבילות ששלח וסופקו מקבל ת"ז של הלקוח ששלח
        {
            int mone = 0;
            BO.Customer c = GetCustomer(idc);
            IEnumerable<BO.ParcelInCustomer> a = c.ListOfPackagesToTheCustomer;//רשימה של משלוחים ששלח
            for(int i = 0; i < a.Count(); i++)
            {
                if (a.ElementAt(i).StatusParcel == StatusParcel.provided)//אם מצב החבילה נוצר אז להוסיף את הכצות של המונה
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
                if (a.ElementAt(i).StatusParcel != StatusParcel.provided)//אם מצב החבילה נוצר אז להוסיף את הכצות של המונה
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
                if (a.ElementAt(i).StatusParcel == StatusParcel.provided)//אם מצב החבילה נוצר אז להוסיף את הכצות של המונה
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
                if (a.ElementAt(i).StatusParcel != StatusParcel.provided)//אם מצב החבילה נוצר אז להוסיף את הכצות של המונה
                    mone++;
            }
            return mone;
        }
    }
    
}
