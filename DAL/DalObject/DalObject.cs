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

        public IEnumerable<IDAL.DO.Drone> ddroneList()
        {
            return DataSource.dronsList;
        }

        public IDAL.DO.Drone GetDrone(int id)
        {
            IDAL.DO.Drone? per = DataSource.dronsList.FirstOrDefault(p => p.Id == id);
            if (per is null)
                throw new DroneDoesNotExistException($"bad drone id: {id}");
            else
                return (Drone)per;
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
            IDAL.DO.Drone? per = DataSource.dronsList.FirstOrDefault(p => p.Id == drone.Id);

            if (per is null)
                throw new IDAL.DO.DroneAlreadyExistException($"bad drone id: {drone.Id}");
            DataSource.dronsList.Add(drone.Clone());//צריך ליצור קלון
        }

        public void DeleteDrone(int id)
        {
            IDAL.DO.Drone? per = DataSource.dronsList.Find(p => p.Id == id);

            if (per is null)
                DataSource.dronsList.Remove((Drone)per);
            else
                throw new IDAL.DO.DroneDoesNotExistException($"bad drone id: {id}");

        }

        public void UpdetDrone(IDAL.DO.Drone drone)
        {
            IDAL.DO.Drone? per = DataSource.dronsList.FirstOrDefault(p => p.Id == drone.Id);
            if (per is null)
            {
                DataSource.dronsList.Remove((Drone)per);//מחיקה
                DataSource.dronsList.Add(drone.Clone());//הוספה מעודכן
            }

            else
                throw new IDAL.DO.DroneDoesNotExistException($"bad drone id: {drone.Id}");

        }

        public void UpdetDrone(int id, Action<IDAL.DO.Drone> action)
        {
            throw new NotImplementedException();
        }








        //public void AddDrone(Drone dr)//מוסיף רחפן add a drone
        //{
        //    //
        //    DataSource.dronsList.Add(dr);
        //    DataSource.Config.amountDorneId++;

        //}


        public Drone SearchDrone(int id)//מחפש רחפן ךפי ת"ז search drone by id
        {
            foreach (Drone dr in DataSource.dronsList)
            {
                if (dr.Id == id)
                    return dr;
            }
            return new Drone();
        }


        //public void UpdetDrone()
        //{
        //    try
        //    {


        //        IDAL.DO.Customer customer = accessIdal.GetCustomer(id);
        //        if (name != "")
        //            customer.Name = neme;
        //        if (name != "")
        //            customer.Name = neme;
        //        if (name != "")
        //            customer.Name = neme;
        //    }
        //}
        #endregion

        #region Station
        //CRU
        public IEnumerable<IDAL.DO.Station> sStationList()
        {
            return DataSource.stationsList;
        }
        public IDAL.DO.Station GetStation(int id)
        {
            IDAL.DO.Station? per = DataSource.stationsList.Find(p => p.Id == id);
            if (per is null)
                return (Station)per;
            else
                throw new StationDoesNotExistException($"bad Station id: {id}");//שרה הקישקוש של השם זה השם של הזריקה
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
            IDAL.DO.Station? per = DataSource.stationsList.FirstOrDefault(p => p.Id == station.Id);
            if (per is null)
                throw new IDAL.DO.StationAlreadyExistsException($"bad drone id: {station.Id}");
            DataSource.stationsList.Add(station.Clone());
        }

        public void DeleteStation(int id)
        {
            IDAL.DO.Station? per = DataSource.stationsList.Find(p => p.Id == id);

            if (per is null)
                DataSource.stationsList.Remove((Station)per);
            else
                throw new IDAL.DO.StationDoesNotExistException($"bad drone id: {id}");

        }

        public void UpdetStation(IDAL.DO.Station station)
        {
            IDAL.DO.Station? per = DataSource.stationsList.Find(p => p.Id == station.Id);
            if (per is null)
            {
                DataSource.stationsList.Remove((Station)per);//מחיקה
                DataSource.stationsList.Add(station.Clone());//הוספה מעודכן
            }

            else
                throw new StationDoesNotExistException($"bad drone id: {station.Id}");

        }

        public void UpdetStation(int id, Action<IDAL.DO.Station> action)
        {
            throw new NotImplementedException();
        }


        //public IEnumerable<Station> showStation()
        //{
        //    return DataSource.stationsList.toList();
        //}


        //public void AddStation(Station st)//מוסיף תחנת בסיס add a station
        //{
        //    DataSource.stationsList.Add(st);
        //    DataSource.Config.amountStationId++;

        //}


        public Station SearchStation(int id)//מחפש ,תחנה ךפי ת"ז search station by id
        {
            foreach (Station dr in DataSource.stationsList)
            {
                if (dr.Id == id)
                    return dr;
            }
            throw new StationDoesNotExistException();
        }
        #endregion

        #region Parcel
        public IEnumerable<IDAL.DO.Parcel> pparcelList()
        {
            return DataSource.parcelList;
        }
        public IDAL.DO.Parcel GetParcel(int id)
        {
            IDAL.DO.Parcel? per = DataSource.parcelList.Find(p => p.Id == id);
            if (per is null)
                return (Parcel)per;
            else
                throw new ParcelDoesNotExistException($"bad Parcle id: {id}");//שרה הקישקוש של השם זה השם של הזריקה
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
            IDAL.DO.Parcel? per = DataSource.parcelList.FirstOrDefault(p => p.Id == parcel.Id);
            if (per is null)
                throw new IDAL.DO.ParcelAlreadyExistsException($"bad drone id: {parcel.Id}");
            DataSource.parcelList.Add(parcel.Clone());//צריך ליצור קלון
        }

        public void DeleteParcel(int id)
        {
            IDAL.DO.Parcel? per = DataSource.parcelList.Find(p => p.Id == id);

            if (per != null)
                DataSource.parcelList.Remove((Parcel)per);
            else
                throw new IDAL.DO.ParcelDoesNotExistException($"bad Parcel id: {id}");


        }

        public void UpdetParcel(IDAL.DO.Parcel parcel)
        {
            IDAL.DO.Parcel? per = DataSource.parcelList.Find(p => p.Id == parcel.Id);
            if (per != null)
            {
                DataSource.parcelList.Remove((Parcel)per);//מחיקה
                DataSource.parcelList.Add(parcel.Clone());//הוספה מעודכן
            }

            else
                throw new IDAL.DO.ParcelDoesNotExistException($"bad drone id: {parcel.Id}");

        }

        public void UpdetParcel(int id, Action<IDAL.DO.Parcel> action)
        {
            throw new NotImplementedException();
        }







        //public int AddParcel(Parcel pr)//מוסיף הזמנה add a parcel
        //{
        //    pr.Id = DataSource.Config.amountParcelId;
        //    DataSource.parcelList.Add(pr);
        //    DataSource.Config.amountParcelId++;
        //    return DataSource.Config.amountParcelId - 1;
        //}

        public Parcel SearchParcel(int id)//מחפש חבילה ךפי ת"ז search parcel by id
        {
            foreach (Parcel dr in DataSource.parcelList)
            {
                if (dr.Id == id)
                    return dr;
            }
            throw new ParcelDoesNotExistException();

        }
        #endregion

        #region DroneCharg
        public IDAL.DO.DroneCharge GetDroneChargByDrone(int id)
        {
            IDAL.DO.DroneCharge? per = DataSource.droneChargeList.Find(p => p.Droneld == id);
            if (per != null)
                return (DroneCharge)per;
            else
                throw new IDAL.DO.DroneChargDoesNotExistException($"bad Drone id: {id}");
        }
        public IDAL.DO.DroneCharge GetDroneChargByStation(int id)
        {
            IDAL.DO.DroneCharge? per = DataSource.droneChargeList.Find(p => p.Stationld == id);
            if (per != null)
                return (DroneCharge)per;
            else
                throw new IDAL.DO.StationDoesNotExistException($"bad station id: {id}");
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

        //public void AddDroneCharge(IDAL.DO.DroneCharge droneCharge)
        //{
        //    if (DataSource.droneChargeList.FirstOrDefault(p => p.Id == drone.Id) != null)
        //        throw new IDAL.DO.DroneChargDoesNotExistException($"bad drone id: {drone.Id}");
        //    DataSource.dronsList.Add(drone.clone());//צריך ליצור קלון
        //}

        //public void deleteCDrone(int id)
        //{
        //    IDAL.DO.Drone? per = DataSource.dronsList.Find(p => p.Id == id);

        //    if (per != null)
        //        DataSource.dronsList.Remove(per);
        //    else
        //        throw new IDAL.DO.DroneChargDoesNotExistException($"bad drone id: {id}");


        //}

        public void UpdetDroneCharge(IDAL.DO.DroneCharge droneCharge)
        {
            IDAL.DO.DroneCharge? per = DataSource.droneChargeList.Find(p => p.Stationld == droneCharge.Stationld);
            if (per != null)
            {
                DataSource.droneChargeList.Remove((DroneCharge)per);//מחיקה
                
                DataSource.droneChargeList.Add(droneCharge.Clone());//הוספה מעודכן
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
            for (int i = 0; i < DataSource.dronsList.Count; i++)
            {
                if (DataSource.dronsList[i].Id == drone)
                {
                    for (int j = 0; j < DataSource.stationsList.Count; j++)
                    {
                        if (DataSource.stationsList[j].Id == station)
                        {
                            DataSource.stationsList[j].ChargeSlots--;
                            DroneCharge cc = new DroneCharge(drone, station);
                            DataSource.droneChargeList.Add(cc);
                        }
                    }
                    throw new DoesNotExistException();
                }
            }
            throw new DoesNotExistException();

        }

        public void AddDroneCharge(DroneCharge cs)//מוסיף רחפנים לעמדות טעינה add drone to the charge spot
        {
            SendDroneToCharge(cs.Stationld,cs.Droneld);

        }
        #endregion

        #region Customer
        public IEnumerable<IDAL.DO.Customer> ccustomerList()
        {
            return DataSource.customerList;
        }
        public void DeleteDroneCharge(int idDrone)//Release Drone from charging
        {
            //צריך למחוק את הרחפן מרשימת טעינה
            for (int i = 0; i < DataSource.droneChargeList.Count; i++)
            {

                if (DataSource.droneChargeList[i].Droneld == idDrone)
                {
                    for (int j = 0; j < DataSource.stationsList.Count; j++)
                    {
                        if (DataSource.droneChargeList[i].Stationld == DataSource.stationsList[j].Id)
                            DataSource.stationsList[j].ChargeSlots++;//צריך לעלות את כמות המקומות הטעינה
                    }
                    throw new DoesNotExistException();
                }
            }
            throw new DoesNotExistException();
           

        }

           

            public IDAL.DO.Customer GetCustomer(int id)
        {
            IDAL.DO.Customer? per = DataSource.customerList.Find(p => p.Id == id);
            if (per is null)
                return (Customer)per;
            else
                throw new CsustomerDoesNotExistException($"bad Customer id: {id}");//שרה הקישקוש של השם זה השם של הזריקה
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
            IDAL.DO.Customer? per = DataSource.customerList.Find(p => p.Id == customer.Id);
            if (per is null)
                throw new IDAL.DO.CustomerAlreadyExistsException($"bad customer id: {customer.Id}");
            DataSource.customerList.Add(customer.Clone());//צריך ליצור קלון
        }

        public void DeleteCustomer(int id)
        {
            IDAL.DO.Customer? per = DataSource.customerList.Find(p => p.Id == id);

            if (per is null)
                DataSource.customerList.Remove((Customer)per);
            else
                throw new CsustomerDoesNotExistException($"bad customer id: {id}");

        }

        public void UpdetCustomer(IDAL.DO.Customer customer)
        {
            IDAL.DO.Customer? per = DataSource.customerList.Find(p => p.Id == customer.Id);
            if (per is null)
            {
                DataSource.customerList.Remove((Customer)per);//מחיקה
                DataSource.customerList.Add(customer.Clone());//הוספה מעודכן
            }

            else
                throw new CsustomerDoesNotExistException($"bad drone id: {customer.Id}");

        }

        public void UpdetCustomer(int id, Action<IDAL.DO.Customer> action)
        {
            throw new NotImplementedException();
        }










        //public void AddCustomer(Customer cs)//מוסיף לקוח
        //{
        //    DataSource.Config.amountCustomerId++;
        //    DataSource.customerList.Add(cs);
        //}

        public Customer SearchCustomer(int id)//מחפש לקוח ךפי ת"ז search customer by id
        {
            foreach (Customer dr in DataSource.customerList)
            {
                if (dr.Id == id)
                    return dr;
            }
            return new Customer();
        }
        #endregion









        public bool AssignPackageToDrone(int idParcel)//Assign a Package To Drone
        {
            //לבדוק אם קיים
            for (int i = 0; i < DataSource.parcelList.Count; i++)//שיוך החבילה לרחפן אני מחפשתאת החבילה ואת הרחפן שרני רוצה ומשנה באתאם את הנתונים
            {
                if (DataSource.parcelList[i].Id == idParcel)
                {
                    for (int j = 0; j < DataSource.Config.amountStationId; j++)
                    {
                        // if (DataSource.dronsList[j].StatusDrone == IDAL.Status.available)//אם מצאתי רחפן זמין
                        {
                            DataSource.parcelList[i].Droneld = DataSource.dronsList[j].Id;//הכנסה ת"ז של הרחפן 
                                                                                          //  DataSource.dronsList[j].Status = IDAL.Status.delivered;//שינוי סטטוס שהוא לא זמין
                            DataSource.parcelList[i].Scheduled = DateTime.Now;//עדכון זמן שיוך חבילה
                            return true;
                        }
                    }

                }
            }
            return false;

        }
        public void PackageCollectionByDrone(int idParcel)//Package collection by Drone
        {
            for (int i = 0; i < DataSource.parcelList.Count; i++)
            {
                if (DataSource.parcelList[i].Id == idParcel)
                {
                    DataSource.parcelList[i].PichedUp = DateTime.Now;//עדכון זמן הגעת הרחפן לשולח חבילה
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
                    for (int j = 0; j < DataSource.parcelList.Count; j++)
                    {
                        if (DataSource.dronsList[j].Id == DataSource.parcelList[i].Droneld)//אם מצאתי רחפן לפי ת"ז של רחפן בחבילה
                        {
                            // DataSource.dronsList[j].StatusDrone = IDAL.Status.available;//להעביר את הרחפן למצב שהוא זמין
                            DataSource.parcelList[i].Delivered = DateTime.Now;//עדכון זמן שיוך הגעת חבילה למקבל
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
        //השאלה אם זה תחנות טעינה או רק תחנות רגילות.בכל אופן אני אדפיס פעולה אחרת לתחנות טעינה

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

        //public void PrintFreeBaseStationList()//הדפסת רשימת התחנות  שיש בהם עמדות טעינה פנויות show 
        //{
        //    foreach (Station dr in DataSource.stationsList) {
        //        if (dr.ChargeSlots > 0)
        //            Console.WriteLine(dr.Id);

        //    }
        //יש לנו כבר כזה לא?

        //}


        public IEnumerable<double> ElectricityUse()
        {
            double[] arr = new double[5];
            arr[0] = DataSource.Config.Free;
            arr[1] = DataSource.Config.HeavyWeight;
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


