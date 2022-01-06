using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;


namespace BL
{
    sealed partial class BL : IBL
    {
        private BO.ParcelInCustomer ParcelInCustomeWhoSend(int idp)//return parcel to  customer//מי ששלח-מקור
        {

            BO.ParcelInCustomer p = new BO.ParcelInCustomer()//לקוח בחבילה זה מי שמקבל
            {
                Id = idp,
                Weight = (BO.WeightCategories)accessIDal.GetParcel(idp).Weight,
                Priority = (BO.Priority)accessIDal.GetParcel(idp).Priority,
                StatusParcel = StatuseParcelKnow(idp),
                Senderld = new BO.CustomerInParcel() { Id = accessIDal.GetParcel(idp).Targetld, Name = accessIDal.GetCustomer(accessIDal.GetParcel(idp).Targetld).Name }

            };
            return p;
        }
        private BO.ParcelInCustomer ParcelInCustomeWhoGet(int idp)//return parcel from  customer//מי שמקבל
        {

            BO.ParcelInCustomer p = new BO.ParcelInCustomer()//לקוח בחבילה זה מי ששלח
            {
                Id = idp,
                Weight = (BO.WeightCategories)accessIDal.GetParcel(idp).Weight,
                Priority = (BO.Priority)accessIDal.GetParcel(idp).Priority,
                StatusParcel = StatuseParcelKnow(idp),
                Senderld = new BO.CustomerInParcel() { Id = accessIDal.GetParcel(idp).Senderld, Name = accessIDal.GetCustomer(accessIDal.GetParcel(idp).Senderld).Name }

            };
            return p;
        }
        //


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
                DO.Customer customer = accessIDal.GetCustomer(id);
                c.Id = customer.Id;
                c.Name = customer.Name;
                c.Pone = customer.Pone;
                BO.Location newC = new BO.Location() { Latitude = customer.Lattitude, Longitude = customer.Longitude };
                c.LocationOfCustomer = newC;
                c.ListOfPackagesFromTheCustomer = (List<BO.ParcelInCustomer>)ListParcelFromCustomers(id);// רשימה של מישלוחים שמקבל
                c.ListOfPackagesToTheCustomer = (List<BO.ParcelInCustomer>)ListParcelToCustomer(id);//רשימה של משלוחים ששולח

            }
            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
            return c;
        }
        //update customer by id or name or phone ot more
        public void UpdateCustomer(int id, string name, string phone)//v
        {
            DO.Customer c = accessIDal.GetCustomer(id);
            DO.Customer cc = new DO.Customer();
            cc.Id = c.Id;
            cc.Lattitude = c.Lattitude;
            cc.Longitude = c.Longitude;
            try
            {
                c = accessIDal.GetCustomer(id);
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
            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }

        }
        //add customer
        public void AddCustomer(BO.Customer customer)//v
        {
            DO.Customer customer1 = new DO.Customer() { Id = customer.Id, Name = customer.Name, Pone = customer.Pone, Longitude = customer.LocationOfCustomer.Longitude, Lattitude = customer.LocationOfCustomer.Latitude };
            try
            {
                accessIDal.AddCustomer(customer1);
            }

            catch (BO.Excptions ex)
            {
                throw new BO.Excptions(ex.Message);
            }
        }


        public BO.CustomerToList CostumerToListToPrint(int idc)//
        {
            BO.CustomerToList c = new BO.CustomerToList();
            try
            {
                DO.Customer customer = accessIDal.GetCustomer(idc);
                c.Id = customer.Id;
                c.Name = customer.Name;
                c.Pone = customer.Pone;
                c.NumberOfPackagesSentAndDeliveredCustomer = NumberOfPackagesSentAndDelivered(idc);
                c.NumberOfPackagesSentAndNotDeliveredCustomer = NumberOfPackagesSentButNotDelivered(idc);
                c.NumberOfPackagesGetCustomer = NumberOfPackagesHeReceived(idc);
                c.SeveralPackagesOnTheWayToTheCustomerCustomer = SeveralPackagesOnTheWayToTheCustomer(idc);
            }
            catch (BO.Excptions ex)
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
            for (int i = 0; i < a.Count(); i++)
            {
                if (a.ElementAt(i).StatusParcel == BO.StatusParcel.provided)//אם מצב החבילה נוצר אז להוסיף את הכצות של המונה
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
                if (a.ElementAt(i).StatusParcel != BO.StatusParcel.provided)//אם מצב החבילה נוצר אז להוסיף את הכצות של המונה
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
                if (a.ElementAt(i).StatusParcel == BO.StatusParcel.provided)//אם מצב החבילה נוצר אז להוסיף את הכצות של המונה
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
                if (a.ElementAt(i).StatusParcel != BO.StatusParcel.provided)//אם מצב החבילה נוצר אז להוסיף את הכצות של המונה
                    mone++;
            }
            return mone;
        }
        public IEnumerable<BO.CustomerToList> GetCustomers()
        {
            List<BO.CustomerToList> list = new List<BO.CustomerToList>();
            IEnumerable<DO.Customer> a = accessIDal.GetAllCustomer();

        for (int i = 0; i < a.Count(); i++)
            {
                list.Add(CostumerToListToPrint(a.ElementAt(i).Id));
            }
            return list;
        }
    }
    
}


