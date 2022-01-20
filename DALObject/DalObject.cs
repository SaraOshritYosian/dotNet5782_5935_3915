
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using DalApi;
using DO;
namespace Dal//במיין בהוספה את מקבלת את הנתונים ומכניסה אותם לאובייקט שאותו את שולחת כפרמטר לפונקצית הוספה שבdalobject
{

     sealed partial class DalObject : IDal
    {
        static readonly IDal instance = new DalObject();
        public static IDal Instance { get => instance; }

       
        static DalObject()// בנאי של דלאובצקט והיא המחלקה שקונסול יעשה לה ניו מתי שהוא ירצה להתחיל והיא שניקרא לפונקציות בדתסורס
        {
            DataSource.Initialize(); 
        }
        //
        #region Drone
        //CRUD Drone
        public int GetDroneDoSndByParcelId(int idp)//מחזיר את ת"ז של הרחפ ןשמבצע את המשלוח
        {
            bool fal = DataSource.parcelList.Any(p => p.Id == idp);
            if (fal == false)
                throw new ParcelDoesNotExistException($"bad Parcle id: {idp}");
            else
                return DataSource.parcelList.Find(p => p.Id == idp).Droneld;
          
        }
        public IEnumerable<DO.Drone> ListDroneInStation(int id)
        {
            return (IEnumerable<Drone>)(from Drone in DataSource.dronsList
                   select GetStation(id));
        }


        public Drone GetDrone(int id)
        {
            bool fal = DataSource.dronsList.Any(p => p.Id == id);
           Drone per= DataSource.dronsList.Find(p => p.Id == id);
            if (fal==false)
                throw new DroneDoesNotExistException($"bad drone id: {id}");
            else
                return per;
        }

        public IEnumerable<Drone> GetAllDrone()
        {
            return from Drone in DataSource.dronsList
                   select Drone;
        }

        public IEnumerable<Drone> GetAllDroneBy(Predicate<DO.Drone> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void AddDrone(Drone drone)
        {
            if (DataSource.dronsList.Any(p => p.Id == drone.Id))
            {
                throw new DroneAlreadyExistException($"bad drone id: {drone.Id}");
            }
            DataSource.dronsList.Add(drone);
           }

        public void DeleteDrone(int id)
        {
            bool fal = DataSource.dronsList.Any(p => p.Id == id);
            Drone per = DataSource.dronsList.Find(p => p.Id == id);
            //IDAL.DO.Drone? per = DataSource.dronsList.Find(p => p.Id == id);

            if (fal==false)
                throw new DroneDoesNotExistException($"bad drone id: {id}");
            else
                DataSource.dronsList.Remove(per);
            

        }

        public void UpdetDrone(Drone drone)
        {
            Drone per = DataSource.dronsList.Find(p => p.Id == drone.Id);
            bool fal = DataSource.dronsList.Any(p => p.Id == drone.Id);
            if (fal == false)
                throw new DroneDoesNotExistException($"bad drone id: {drone.Id}");
            else
            {
                DataSource.dronsList.Remove(per);//מחיקה
                DataSource.dronsList.Add(drone);//הוספה מעודכן
            }
           

        }

        public void UpdetDrone(int id, Action<Drone> action)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> DdroneList()
        {
            return DataSource.dronsList;
        }
        public IEnumerable<int> GetListDrone()//מחזיר רשימה של רחפנים רק הת"ז שלהם
        {
            List<int> a = new List<int>();
            foreach (Drone item in DataSource.dronsList)
            {
                a.Add(item.Id);
            }
            return a;
        }
        #endregion

        #region Station
        //CRU
        public IEnumerable<Station> ReturnStationHaveFreeCharde()//מחזיר רשימה של תחנות שיש להם כמות טעינה גדולה מ0
        {
            List<Station> ss = new List<Station>();
            List<Station> pp = DataSource.stationsList;
            for (int i = 0; i < pp.Count(); i++)
            {
                if (pp[i].ChargeSlots > 0)
                    ss.Add(pp[i]);
            }
            return ss;
        }
        public IEnumerable<Station> SStationList()
        {
            return DataSource.stationsList;
        }
        
        public Station GetStation(int id)
        {
            bool fal= DataSource.stationsList.Any(p => p.Id == id);
            Station per= DataSource.stationsList.Find(p => p.Id == id);
            if (fal == true)//נימצא
            {
                return per;
            }
               
            else
                throw new StationDoesNotExistException($"bad Station id: {id}");

        }
        
        public IEnumerable<Station> GetAllStation()
        {
           
            return from Station in DataSource.stationsList
                   select Station;
        }

        public IEnumerable<Station> GetAllStationBy(Predicate<Station> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void AddStation(Station station)
        {
            if(DataSource.stationsList.Any(p => p.Id == station.Id)){//נימצא
                throw new StationAlreadyExistsException($"bad station id: {station.Id}");
            }
            DataSource.stationsList.Add(station);
        }

        public void DeleteStation(int id)
        {
            Station per= DataSource.stationsList.Find(p => p.Id == id);
            bool fal = DataSource.stationsList.Any(p => p.Id == id);
            if (fal == true)//נימצא
              DataSource.stationsList.Remove(per);
            else
                throw new StationDoesNotExistException($"bad station id: {id}");

        }

        public void UpdetStation(Station station)
        {
            Station per = DataSource.stationsList.Find(p => p.Id == station.Id);
            bool fal = DataSource.stationsList.Any(p => p.Id == station.Id);
           // IDAL.DO.Station per = DataSource.stationsList.Find(p => p.Id == station.Id);
            if (fal==true)//נימצא
            {
                DataSource.stationsList.Remove(per);//מחיקה
                DataSource.stationsList.Add(station);//הוספה מעודכן
            }

            else
                throw new StationDoesNotExistException($"bad drone id: {station.Id}");

        }

        public void UpdetStation(int id, Action<Station> action)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Parcel
        
        public IEnumerable<Parcel> PparcelList()//return list
        {
            return DataSource.parcelList;
        }
        public Parcel GetParcel(int id)
        {
            Parcel per = DataSource.parcelList.Find(p => p.Id == id);
            bool fal= DataSource.parcelList.Any(p => p.Id == id);
            if (fal==false)//לא קיים
                throw new ParcelDoesNotExistException($"bad Parcle id: {id}");
            else
                 return per;
        }
        public Parcel GetParcelByDrone(int id)
        {
           Parcel per = DataSource.parcelList.Find(p => p.Droneld == id);
            bool fal = DataSource.parcelList.Any(p => p.Droneld == id);
            if (fal == false)
                throw new DroneDoesNotExistException($"bad drone id: {id}");
            else
                return per;
        }


        public IEnumerable<Parcel> GetAllParcel()
        {
            return from Parcel in DataSource.parcelList
                   select Parcel;
        }

        public IEnumerable<Parcel> GetAllParcelBy(Predicate<Parcel> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public int AddParcel(Parcel parcel)
        {
            //if (DataSource.parcelList.Any(p => p.Id == parcel.Id))
            //{
            //    throw new ParcelAlreadyExistsException($"bad parcel id: {parcel.Id}");
            //}
            Parcel p = new Parcel();
            p.Senderld = parcel.Senderld;
            p.Targetld = parcel.Targetld;
            p.Weight = parcel.Weight;
            p.Priority = parcel.Priority;
            p.Droneld = parcel.Droneld;
            p.Requested = parcel.Requested;
            p.Scheduled = parcel.Scheduled;
            p.PichedUp = parcel.PichedUp;
            p.Delivered = parcel.Delivered;
            p.Id = DataSource.Config.ParcelId++;
            DataSource.parcelList.Add(p);
            return DataSource.Config.ParcelId;
           // DataSource.Config.ParcelId++;
        }
       

        public void DeleteParcel(int id)
        {
            Parcel per = DataSource.parcelList.Find(p => p.Droneld == id);
            bool fal = DataSource.parcelList.Any(p => p.Id == id);

            if (fal == true)//Bימצא
                DataSource.parcelList.Remove(per);
            else
                throw new ParcelDoesNotExistException($"bad Parcel id: {id}");


        }

        public void UpdetParcel(Parcel parcel)
        {
            Parcel per = DataSource.parcelList.Find(p => p.Droneld == parcel.Id);
            bool fal = DataSource.parcelList.Any(p => p.Id == parcel.Id);
            if (fal == true)//Bימצא
            {
                DataSource.parcelList.Remove(per);//מחיקה
                DataSource.parcelList.Add(parcel);//הוספה מעודכן
            }

            else
                throw new ParcelDoesNotExistException($"bad parcel id: {parcel.Id}");

        }

        public void UpdetParcel(int id, Action<Parcel> action)
        {
            throw new NotImplementedException();
        }
      
        #endregion

        #region DroneCharg
      

        public int CoutCharge(int id)//בדיקה כמה עמדות טעינה תפוסים ישש לתחנה מסויימת
        {
            int mone = 0;
            for(int i = 0; i < DataSource.droneChargeList.Count; i++)
            {
                if (DataSource.droneChargeList[i].Stationld == id)
                {
                    mone++;
                }
               
            }
            return mone;
        }

        public DroneCharge GetDroneChargByDrone(int id)
        {
            bool fal = DataSource.droneChargeList.Any(p => p.Droneld == id);
            if (fal == true)//Bימצא
                return DataSource.droneChargeList.Find(p => p.Droneld == id);
            else
                throw new DroneChargDoesNotExistException($"bad Drone id: {id}");
        }
        public DroneCharge GetDroneChargByStation(int id)
        {
            bool fal = DataSource.droneChargeList.Any(p => p.Stationld == id);
            if (fal == true)//Bימצא
                return DataSource.droneChargeList.Find(p => p.Stationld == id);
            else
                throw new StationDoesNotExistException($"bad station id: {id}");
        }

        public IEnumerable<int> GetDroneChargByStationListInt(int ids)//מחזיר רשימה של ת"ז של רחפנים שנימצאים בתחנה מסויימת
        {
            List<int> a = new List<int>();
           
            for (int i = 0; i < DataSource.droneChargeList.Count(); i++)
            {
                if (DataSource.droneChargeList[i].Stationld == ids)
                {
                    a.Add(DataSource.droneChargeList[i].Droneld);
                }
            }
            return a;
        }
        public int MoneDroneChargByStationListInt(int ids)//מחזיר כמוצ של ת"ז של רחפנים שנימצאים בתחנה מסויימת
        {
            int mone = 0;

            for (int i = 0; i < DataSource.droneChargeList.Count(); i++)
            {
                if (DataSource.droneChargeList[i].Stationld == ids)//כמה רחפנים יש בטעינה סתחנה מסויימת
                {
                    mone++;
                }
            }
            return mone;
        }

        public IEnumerable<DroneCharge> GetAllDroneCharge()
        {
            return from DroneCharge in DataSource.droneChargeList
                   select DroneCharge;
        }

        public IEnumerable<DroneCharge> GetAllDroneChargeBy(Predicate<DroneCharge> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void UpdetDroneCharge(DroneCharge droneCharge)
        {
            bool fal = DataSource.droneChargeList.Any(p => p.Stationld == droneCharge.Stationld);
          
            if (fal == true)//Bימצא
            {
                DataSource.droneChargeList.Remove(DataSource.droneChargeList.Find(p => p.Stationld == droneCharge.Stationld));//מחיקה
                
                DataSource.droneChargeList.Add(droneCharge);//הוספה מעודכן
            }

            else
                throw new DroneChargDoesNotExistException($"bad station id: {droneCharge.Stationld}");

        }
       
        public void UpdetDroneCharge(int id, Action<DroneCharge> action)
        {
            throw new NotImplementedException();
        }

        public void SendDroneToCharge(int station, int drone)//Drone a skimmer for charging
        {
            bool fal1 = DataSource.stationsList.Any(p => p.Id == station);
            bool fal2 = DataSource.dronsList.Any(p => p.Id == drone);
            Drone drone1 = DataSource.dronsList.Find(p => p.Id == drone);
            Station station1= DataSource.stationsList.Find(p => p.Id == station);
                        
            if (fal1 == true& fal2==true)//שתיה  נימצאו
            {
                station1.ChargeSlots--;
                UpdetStation(station1);//update
                DataSource.droneChargeList.Add(new DroneCharge { Droneld = drone, Stationld = station });//add to list
            } 
        }
        public void ReleaseDroneFromCharging(int drone)//Drone a skimmer from charging
        {
            bool fal2 = DataSource.droneChargeList.Any(p => p.Droneld == drone);
            DroneCharge drone1 = DataSource.droneChargeList.Find(p => p.Droneld == drone);
            if (fal2 == true)
            {
                Station station1 = DataSource.stationsList.Find(p => p.Id == drone1.Stationld);
                station1.ChargeSlots++;
                UpdetStation(station1);
                DataSource.droneChargeList.Remove(drone1);
            }
        }

        public void AddDroneCharge(DroneCharge cs)//מוסיף רחפנים לעמדות טעינה add drone to the charge spot
        {
            SendDroneToCharge(cs.Stationld,cs.Droneld);

        }

        #endregion

        #region Customer

        public IEnumerable<int> ListTargetParcel(int idta)//return a list of parcek by target
        {
            List<int> a = new List<int>();
            foreach (Parcel item in DataSource.parcelList)
            {
                if (item.Targetld == idta)
                {
                    a.Add(item.Id);
                }
            }
            return a;
        }
        public IEnumerable<int> ListSendetParcel(int idse)//return a list of parcek by sender
        {
            List<int> a = new List<int>();
            foreach (Parcel item in DataSource.parcelList)
            {
                if (item.Senderld == idse)
                {
                    a.Add(item.Id);
                }
            }
            return a;
        }
        public IEnumerable<Customer> CcustomerList()
        {
            return DataSource.customerList;
        }
      
            public Customer GetCustomer(int id)
             {
            bool fal = DataSource.customerList.Any(p => p.Id == id);
            if (fal==false)//לא נימצא
                throw new CsustomerDoesNotExistException($"bad Customer id: {id}");
            else
                return DataSource.customerList.Find(p => p.Id == id);

             }

        public IEnumerable<Customer> GetAllCustomer()
        {
            return from Customer in DataSource.customerList
                   select Customer;
        }

        public IEnumerable<Customer> GetAllCustomerBy(Predicate<Customer> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void AddCustomer(Customer customer)
        {
            if (DataSource.parcelList.Any(p => p.Id == customer.Id))
            {
                throw new CustomerAlreadyExistsException($"bad customer id: {customer.Id}");
            }
            DataSource.customerList.Add(customer);
        }

        public void DeleteCustomer(int id)
        {
            bool fal = DataSource.customerList.Any(p => p.Id == id);
            if (fal == false)
                throw new CsustomerDoesNotExistException($"bad customer id: {id}");
            else
                DataSource.customerList.Remove( DataSource.customerList.Find(p => p.Id == id));



        }

        public void UpdetCustomer(Customer customer)
        {
            bool fal = DataSource.customerList.Any(p => p.Id == customer.Id);
            if (fal == false)
                throw new CsustomerDoesNotExistException($"bad customer id: {customer.Id}");

            else
            {
                DataSource.customerList.Remove(DataSource.customerList.Find(p => p.Id == customer.Id));//מחיקה
                DataSource.customerList.Add(customer);//הוספה מעודכן
            }
        }

        public void UpdetCustomer(int id, Action<Customer> action)
        {
            throw new NotImplementedException();
        }
        #endregion

        public void AssignPackageToDrone(int idParcel, int idDrone)//Assign a Package To Drone
        {
            Parcel per1 = DataSource.parcelList.Find(p => p.Id == idParcel);
            bool fal1 = DataSource.parcelList.Any(p => p.Id == idParcel);
            bool fal2 = DataSource.dronsList.Any(p => p.Id == idDrone);
            Drone per2 = DataSource.dronsList.Find(p => p.Id == idDrone);
            //לבדוק אם קיים
            if (fal2 == true & fal1 == true)
            {
                per1.Droneld = per2.Id;//הכנסה ת"ז של הרחפן
                per1.Scheduled = DateTime.Now;//עדכון זמן שיוך חבילה
                UpdetParcel(per1);
            }
            else
                throw new Exception("error id");

        }
            

        public void PackageCollectionByDrone(int idParcel)//Package collection by Drone
        {
            Parcel per1 = DataSource.parcelList.Find(p => p.Id == idParcel);
            bool fal1 = DataSource.parcelList.Any(p => p.Id == idParcel);
            if (fal1 == true)
            {
                per1.PichedUp= DateTime.Now;
                UpdetParcel(per1);
            }
        }

        public void DeliveryOfPackageToTheCustomer(int idParcel)//Delivery of a package to the customer
        {
            Parcel per1 = DataSource.parcelList.Find(p => p.Id == idParcel);
            bool fal1 = DataSource.parcelList.Any(p => p.Id == idParcel);
            if (fal1 == true)
            {
                per1.Delivered = DateTime.Now;
                //per1.Droneld = 0;
                UpdetParcel(per1);
            }
        }

      
        public IEnumerable<double> ElectricityUse()
        {
            double[] arr = new double[5];
            arr[0] = DataSource.Config.Free;//פנוי
            arr[1] = DataSource.Config.LightWeight;
            arr[2] = DataSource.Config.MediumWeight;
            arr[3] = DataSource.Config.HeavyWeight;
            arr[4] = DataSource.Config.LoadingPrecents;
            return arr;
        }
        public double[] RequestPowerConsuption()
        {
            double[] t = { DataSource.Config.Free, DataSource.Config.LightWeight, DataSource.Config.MediumWeight, DataSource.Config.HeavyWeight, DataSource.Config.LoadingPrecents };
            return t;
        }


    }
}


