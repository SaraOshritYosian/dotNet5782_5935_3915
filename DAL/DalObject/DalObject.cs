using IDAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using IDAL.DO;
using DAL.DalObject;

namespace DalObject//במיין בהוספה את מקבלת את הנתונים ומכניסה אותם לאובייקט שאותו את שולחת כפרמטר לפונקצית הוספה שבdalobject
{

    public partial class DalObject : IDal
    {
        public DalObject()// בנאי של דלאובצקט והיא המחלקה שקונסול יעשה לה ניו מתי שהוא ירצה להתחיל והיא שניקרא לפונקציות בדתסורס
        {
            DataSource.Initialize();
        }


        //
        #region Drone
        //CRUD Drone
        public int GetDroneDoSndByParcelId(int idp)//מחזיר את ת"ז של הרחפ ןשמבצע את המשלוח
        {
            for (int i = 0; i < DataSource.parcelList.Count(); i++)
            {
                if (DataSource.parcelList[i].Id == idp)
                {
                    return DataSource.parcelList[i].Droneld;
                }
            }
            throw new ParcelDoesNotExistException($"bad Parcle id: {idp}");
        }
        public IEnumerable<IDAL.DO.Drone> ListDroneInStation(int id)
        {
            return (IEnumerable<Drone>)(from Drone in DataSource.dronsList
                   select GetStation(id));
        }


        public IDAL.DO.Drone GetDrone(int id)
        {
            bool fal = DataSource.dronsList.Any(p => p.Id == id);
           
            if (fal==false)
                throw new DroneDoesNotExistException($"bad drone id: {id}");
            else
                return DataSource.dronsList.Find(p => p.Id == id);
        }

        public IEnumerable<IDAL.DO.Drone> GetAllDrone()
        {
            return from Drone in DataSource.dronsList
                   select Drone.Clone();
        }

        public IEnumerable<IDAL.DO.Drone> GetAllDroneBy(Predicate<IDAL.DO.Drone> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void AddDrone(IDAL.DO.Drone drone)
        {
            if (DataSource.dronsList.Any(p => p.Id == drone.Id))
            {
                throw new IDAL.DO.DroneAlreadyExistException($"bad drone id: {drone.Id}");
            }
            DataSource.dronsList.Add(drone);
           }

        public void DeleteDrone(int id)
        {
            bool fal = DataSource.dronsList.Any(p => p.Id == id);
            //IDAL.DO.Drone? per = DataSource.dronsList.Find(p => p.Id == id);

            if (fal==false)
                throw new IDAL.DO.DroneDoesNotExistException($"bad drone id: {id}");
            else
                DataSource.dronsList.Remove(DataSource.dronsList.Find(p => p.Id == id));
            

        }

        public void UpdetDrone(IDAL.DO.Drone drone)
        {

            bool fal = DataSource.dronsList.Any(p => p.Id == drone.Id);
            if (fal == false)
                throw new IDAL.DO.DroneDoesNotExistException($"bad drone id: {drone.Id}");
            else
            {
                DataSource.dronsList.Remove(DataSource.dronsList.Find(p => p.Id == drone.Id));//מחיקה
                DataSource.dronsList.Add(drone);//הוספה מעודכן
            }
           

        }

        public void UpdetDrone(int id, Action<IDAL.DO.Drone> action)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDAL.DO.Drone> ddroneList()
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
        public IEnumerable<IDAL.DO.Station> sStationList()
        {
            return DataSource.stationsList;
        }
        
        public IDAL.DO.Station GetStation(int id)
        {
            bool fal= DataSource.stationsList.Any(p => p.Id == id);
            
            if (fal == true)//נימצא
            {
                Console.WriteLine("dd");
                Console.WriteLine(DataSource.stationsList.Find(p => p.Id == id).ToString());
                return DataSource.stationsList.Find(p => p.Id == id);
            }
               
            else
                throw new StationDoesNotExistException($"bad Station id: {id}");

        }

        public IEnumerable<IDAL.DO.Station> GetAllStation()
        {
            return from Station in DataSource.stationsList
                   select Station.Clone();
        }

        public IEnumerable<IDAL.DO.Station> GetAllStationBy(Predicate<IDAL.DO.Station> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void AddStation(IDAL.DO.Station station)
        {
            if(DataSource.stationsList.Any(p => p.Id == station.Id)){//נימצא
                throw new IDAL.DO.StationAlreadyExistsException($"bad station id: {station.Id}");
            }
            DataSource.stationsList.Add(station);
        }

        public void DeleteStation(int id)
        {
            bool fal = DataSource.stationsList.Any(p => p.Id == id);
            if (fal == true)//נימצא
              DataSource.stationsList.Remove( DataSource.stationsList.Find(p => p.Id == id));
            else
                throw new IDAL.DO.StationDoesNotExistException($"bad station id: {id}");

        }

        public void UpdetStation(IDAL.DO.Station station)
        {
            bool fal = DataSource.stationsList.Any(p => p.Id == station.Id);
           // IDAL.DO.Station per = DataSource.stationsList.Find(p => p.Id == station.Id);
            if (fal==true)//נימצא
            {
                DataSource.stationsList.Remove(DataSource.stationsList.Find(p => p.Id == station.Id));//מחיקה
                DataSource.stationsList.Add(station);//הוספה מעודכן
            }

            else
                throw new StationDoesNotExistException($"bad drone id: {station.Id}");

        }

        public void UpdetStation(int id, Action<IDAL.DO.Station> action)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Parcel
        public IEnumerable<IDAL.DO.Parcel> pparcelList()//return list
        {
            return DataSource.parcelList;
        }
        public IDAL.DO.Parcel GetParcel(int id)
        {
            bool fal= DataSource.parcelList.Any(p => p.Id == id);
            if (fal==false)
                throw new ParcelDoesNotExistException($"bad Parcle id: {id}");
            else
                 return DataSource.parcelList.Find(p => p.Id == id);
        }
        public IDAL.DO.Parcel GetParcelByDrone(int id)
        {
            bool fal = DataSource.parcelList.Any(p => p.Droneld == id);
            if (fal == false)
                throw new DroneDoesNotExistException($"bad drone id: {id}");
            else
                return DataSource.parcelList.Find(p => p.Droneld == id);
        }


        public IEnumerable<IDAL.DO.Parcel> GetAllParcel()
        {
            return from Parcel in DataSource.parcelList
                   select Parcel.Clone();
        }

        public IEnumerable<IDAL.DO.Parcel> GetAllParcelBy(Predicate<IDAL.DO.Parcel> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void AddParcel(IDAL.DO.Parcel parcel)
        {
            if (DataSource.parcelList.Any(p => p.Id == parcel.Id))
            {
                throw new IDAL.DO.ParcelAlreadyExistsException($"bad parcel id: {parcel.Id}");
            }
            DataSource.parcelList.Add(parcel);
        }
       

        public void DeleteParcel(int id)
        {
            bool fal = DataSource.parcelList.Any(p => p.Id == id);

            if (fal == true)//Bימצא
                DataSource.parcelList.Remove(DataSource.parcelList.Find(p => p.Id == id));
            else
                throw new IDAL.DO.ParcelDoesNotExistException($"bad Parcel id: {id}");


        }

        public void UpdetParcel(IDAL.DO.Parcel parcel)
        {
            bool fal = DataSource.parcelList.Any(p => p.Id == parcel.Id);
            if (fal == true)//Bימצא
            {
                DataSource.parcelList.Remove(DataSource.parcelList.Find(p => p.Id == parcel.Id));//מחיקה
                DataSource.parcelList.Add(parcel);//הוספה מעודכן
            }

            else
                throw new IDAL.DO.ParcelDoesNotExistException($"bad parcel id: {parcel.Id}");

        }

        public void UpdetParcel(int id, Action<IDAL.DO.Parcel> action)
        {
            throw new NotImplementedException();
        }
      
        #endregion

        #region DroneCharg
        public IEnumerable<IDAL.DO.Drone> droneChargeList()
        {
            return (IEnumerable<Drone>)DataSource.droneChargeList;
        }

        public int coutCharge(int id)//בדיקה כמה עמדות טעינה תפוסים ישש לתחנה מסויימת
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

        public IDAL.DO.DroneCharge GetDroneChargByDrone(int id)
        {
            bool fal = DataSource.droneChargeList.Any(p => p.Droneld == id);
            if (fal == true)//Bימצא
                return DataSource.droneChargeList.Find(p => p.Droneld == id);
            else
                throw new IDAL.DO.DroneChargDoesNotExistException($"bad Drone id: {id}");
        }
        public IDAL.DO.DroneCharge GetDroneChargByStation(int id)
        {
            bool fal = DataSource.droneChargeList.Any(p => p.Stationld == id);
            if (fal == true)//Bימצא
                return DataSource.droneChargeList.Find(p => p.Stationld == id);
            else
                throw new IDAL.DO.StationDoesNotExistException($"bad station id: {id}");
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
     
        public IEnumerable<IDAL.DO.DroneCharge> GetAllDroneCharge()
        {
            return from DroneCharge in DataSource.droneChargeList
                   select DroneCharge.Clone();
        }

        public IEnumerable<IDAL.DO.DroneCharge> GetAllDroneChargeBy(Predicate<IDAL.DO.DroneCharge> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void UpdetDroneCharge(IDAL.DO.DroneCharge droneCharge)
        {
            bool fal = DataSource.droneChargeList.Any(p => p.Stationld == droneCharge.Stationld);
          
            if (fal == true)//Bימצא
            {
                DataSource.droneChargeList.Remove(DataSource.droneChargeList.Find(p => p.Stationld == droneCharge.Stationld));//מחיקה
                
                DataSource.droneChargeList.Add(droneCharge);//הוספה מעודכן
            }

            else
                throw new IDAL.DO.DroneChargDoesNotExistException($"bad station id: {droneCharge.Stationld}");

        }
       
        public void UpdetDroneCharge(int id, Action<IDAL.DO.DroneCharge> action)
        {
            throw new NotImplementedException();
        }

        public void SendDroneToCharge(int station, int drone)//Drone a skimmer for charging
        {
          for (int j = 0; j < DataSource.stationsList.Count; j++)
          {
                if (DataSource.stationsList[j].Id == station)
                {
                            Station s = DataSource.stationsList[j];
                            s.ChargeSlots--;

                            DataSource.stationsList[j]=s;
                            DataSource.droneChargeList.Add(new DroneCharge { Droneld = drone, Stationld = station });
                            return;
                }
          }
        }
        public void ReleaseDroneFromCharging(int drone)//Drone a skimmer from charging
        {
            for (int j = 0; j < DataSource.droneChargeList.Count; j++)
            {
                if (DataSource.droneChargeList[j].Droneld == drone)
                {
                    for (int i = 0; i < DataSource.stationsList.Count; i++)
                    {
                        if (DataSource.stationsList[i].Id == DataSource.droneChargeList[j].Stationld)
                        {
                            Station ss = DataSource.stationsList[i];
                            ss.ChargeSlots++;//לעלות את כמות ההטאינה
                            DataSource.stationsList[i] = ss; 
                            DataSource.droneChargeList.Remove(DataSource.droneChargeList[j]);
                           
                            return;
                        }
                    }
                }
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
        public IEnumerable<IDAL.DO.Customer> ccustomerList()
        {
            return DataSource.customerList;
        }
      
            public IDAL.DO.Customer GetCustomer(int id)
             {
            bool fal = DataSource.customerList.Any(p => p.Id == id);
            if (fal==false)//לא נימצא
                throw new CsustomerDoesNotExistException($"bad Customer id: {id}");
            else
                return DataSource.customerList.Find(p => p.Id == id);

             }

        public IEnumerable<IDAL.DO.Customer> GetAllCustomer()
        {
            return from Customer in DataSource.customerList
                   select Customer.Clone();
        }

        public IEnumerable<IDAL.DO.Customer> GetAllCustomerBy(Predicate<IDAL.DO.Customer> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void AddCustomer(IDAL.DO.Customer customer)
        {
            if (DataSource.parcelList.Any(p => p.Id == customer.Id))
            {
                throw new IDAL.DO.CustomerAlreadyExistsException($"bad customer id: {customer.Id}");
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

        public void UpdetCustomer(IDAL.DO.Customer customer)
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

        public void UpdetCustomer(int id, Action<IDAL.DO.Customer> action)
        {
            throw new NotImplementedException();
        }
        #endregion

        public void AssignPackageToDrone(int idParcel,int idDrone)//Assign a Package To Drone
        {
            //לבדוק אם קיים
            for (int i = 0; i < DataSource.parcelList.Count; i++)//שיוך החבילה לרחפן אני מחפשתאת החבילה ואת הרחפן שרני רוצה ומשנה באתאם את הנתונים
            {
                if (DataSource.parcelList[i].Id == idParcel)
                {
                    for (int j = 0; j < DataSource.dronsList.Count; j++)
                    {
                        if (DataSource.dronsList[j].Id == idDrone)//אם מצאתי רחפן 
                        {
                            Parcel p = DataSource.parcelList[i];
                            p.Droneld = DataSource.dronsList[j].Id;//הכנסה ת"ז של הרחפן 
                            p.Scheduled = DateTime.Now;//עדכון זמן שיוך חבילה
                            DataSource.parcelList[i] = p;
                        }
                        
                           
                            return;
                        
                    }

                }
            }
            throw new Exception("error id");

        }
        public void PackageCollectionByDrone(int idParcel)//Package collection by Drone
        {
            for (int i = 0; i < DataSource.parcelList.Count; i++)
            {
                if (DataSource.parcelList[i].Id == idParcel)
                {
                    Parcel p = DataSource.parcelList[i];
                    p.PichedUp = DateTime.Now;
                    DataSource.parcelList[i]=p;//עדכון זמן הגעת הרחפן לשולח חבילה
                    return;
                }
            }
        }

        public void DeliveryOfPackageToTheCustomer(int idParcel)//Delivery of a package to the customer
        {
            //לבדוק תקינות
            for (int i = 0; i < DataSource.parcelList.Count; i++)//לחפש את החבילה
            {
                if (DataSource.parcelList[i].Id == idParcel)
                {
                    for (int j = 0; j < DataSource.dronsList.Count; j++)
                    {
                        if (DataSource.dronsList[j].Id == DataSource.parcelList[i].Droneld)//אם מצאתי רחפן לפי ת"ז של רחפן בחבילה
                        {
                            // DataSource.dronsList[j].StatusDrone = IDAL.Status.available;//להעביר את הרחפן למצב שהוא זמין
                            Parcel p = DataSource.parcelList[i];
                            p.Delivered = DateTime.Now;//עדכון זמן שיוך חבילה
                             
                            DataSource.parcelList[i] = p;
                            return;
                        }
                        
                    }
                   
                }
            }
           
        }

       
     
        // .אפשרויות הצגת הרשימות
        public void PrindDroneChargeList()//הדפסת רשימת הרחפנים בטעינה print the list of drones in charging
        {
            foreach (Drone dr in DataSource.dronsList)
            {
                // if (dr.StatusDrone == IDAL.Status.InMaintenance)
                {
                    Console.WriteLine(dr.ToString());
                }
            }

        }
        public void PrindBaseStationList()
        // הצגת רשימת תחנות-בסיס show base-station list
        {
            foreach (Station station in DataSource.stationsList/*Drone dr in DataSource.dronsList*/)
            {
                Console.WriteLine(station.ToString());
            }

        }
       

        public void PrintDronesList()//הצגת רשימת הרחפנים show drone list
        {
            foreach (Drone dr in DataSource.dronsList)
            {
                Console.WriteLine(dr.ToString());
            }
        }
        public void PrintCustomersList()//הצגת רשימת הלקוחות show customers list
        {
            foreach (Customer customer in DataSource.customerList)
            {
                Console.WriteLine(customer.ToString());
            }
        }
        public void PrintParcelsList()//הצגת רשימת החבילות show parcels list
        {
            foreach (Parcel parcel in DataSource.parcelList)
            {
                parcel.ToString();
            }
        }
        public void PrintUnconnectedParceslList()// הצגת רשימת חבילות שעוד לא שויכו לרחפן  show unconnected parcesl list
        {
            foreach (Parcel parcel in DataSource.parcelList)
            {
                if (parcel.Id == 0)
                {
                    Console.WriteLine(parcel.ToString());
                }

            }
        }
        public void PrintAvailableStationToChargeList()//הצגת תחנות-בסיס עם עמדות טעינה פנויות show stations with available charge slots
        {
            foreach (Station station in DataSource.stationsList)
            {
                if (station.ChargeSlots > 0)
                {
                    Console.WriteLine(station.ToString());
                }
            }
        }

        //אפשרויות תצוגה כולן ע"פ מספר מזהה מתאים
        public void PrintBaseStation(int stationId)//תצוגת תחנת-בסיס show station details by id
        {
            foreach (Station station in DataSource.stationsList)
            {
                if (station.Id == stationId)
                {
                    Console.WriteLine(station.ToString());
                    break;
                }
            }
        }
        public void PrintDrone(int droneId)// תצוגת רחפן  show drone details by id
        {
            foreach (Drone drone in DataSource.dronsList)
            {
                if (drone.Id == droneId)
                {
                    Console.WriteLine(drone.ToString());
                    break;
                }
            }
        }


        public void PrintCustomer(int customerId)//תצוגת לקוח shoe customer details by id
        {
            foreach (Customer customer in DataSource.customerList)
            {
                if (customer.Id == customerId)
                {
                    Console.WriteLine(customer.ToString());
                    break;
                }
            }
        }
        public void PrintParcel(int parcelId)//תצוגת חבילה show parcel details by id
        {
            foreach (Parcel parcel in DataSource.parcelList)
            {
                if (parcel.Id == parcelId)
                {
                    Console.WriteLine(parcel.ToString());
                    break;
                }
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


