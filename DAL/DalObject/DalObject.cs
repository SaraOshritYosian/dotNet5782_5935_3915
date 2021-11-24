﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace DalObject//במיין בהוספה את מקבלת את הנתונים ומכניסה אותם לאובייקט שאותו את שולחת כפרמטר לפונקצית הוספה שבdalobject
{
    public partial class DalObject: IDAL
    {
        public DalObject()// בנאי של דלאובצקט והיא המחלקה שקונסול יעשה לה ניו מתי שהוא ירצה להתחיל והיא שניקרא לפונקציות בדתסורס
        {
            DataSource.Initialize();
        }



        #region Drone
        //CRUD Drone
        public DO.Drone GetDrone(int id)
        {
            DO.Drone per = DataSource.dronsList.Find(p => p.Id == id);
            if (per != null)
                return per;
            else
                return throw DO.BadDronIdException(id, $"bad drone id: {id}");//שרה זה של השם זה השם של הזריקה
        }

        public IEnumerable<DO.Drone> GetAllDrone()
        {
            return from Drone in DataSource.dronsList
                   select Drone.clone();
        }

        public IEnumerable<DO.Drone> GetAllDroneBy(Predicate<DO.Drone> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void addDrone(DO.Drone drone) 
        {
            if(DataSource.dronsList.FirstOrDefault(p=> p.Id == drone.Id)!=null)
                throw new DO.BadDronIdException(drone.Id, $"bad drone id: {drone.Id}");
            DataSource.dronsList.Add(drone.clone());//צריך ליצור קלון
        }

        public void deleteDrone(int id) 
        {
            DO.Drone per = DataSource.dronsList.Find(p => p.Id == id);

            if (per != null)
                DataSource.dronsList.Remove(per);
            else
                throw new DO.BadDronIdException(id, $"bad drone id: {id}")

        }

        public void UpdetDrone(DO.Drone drone) 
        {
            DO.Drone per = DataSource.dronsList.Find(p => p.Id ==drone.Id );
            if (per != null) {
                DataSource.dronsList.Remove(per);//מחיקה
                DataSource.dronsList.Add(drone.clone());//הוספה מעודכן
            }

            else
                throw new DO.BadDronIdException(drone.Id, $"bad drone id: {drone.Id}")

        }

        public void UpdetDrone(int id, Action<DO.Drone> action)
        {
            throw new NotImplementedException();
        }








        public void AddDrone(Drone dr)//מוסיף רחפן add a drone
        {
            //
            DataSource.dronsList.Add(dr);
            DataSource.Config.amountDorneId++;

        }


        public Drone SearchDrone(int id)//מחפש רחפן ךפי ת"ז search drone by id
        {
            foreach (Drone dr in DataSource.dronsList)
            {
                if (dr.Id == id)
                    return dr;
            }
            return new Drone();
        }


        public void UpdetDrone()
        {
            try
            {


                IDAL.DO.Customer customer = accessIdal.GetCustomer(id);
                if (name != "")
                    customer.Name = neme;
                if (name != "")
                    customer.Name = neme;
                if (name != "")
                    customer.Name = neme;
            }
        }
        #endregion

        #region Station
        //CRU
        public DO.Station GetStation(int id)
        {
            DO.Station per = DataSource.stationsList.Find(p => p.Id == id);
            if (per != null)
                return per;
            else
                return throw DO.BadStationIdException(id, $"bad Station id: {id}");//שרה הקישקוש של השם זה השם של הזריקה
        }

        public IEnumerable<DO.Station> GetAllStation()
        {
            return from Station in DataSource.stationsList
                   select Station.clone();
        }

        public IEnumerable<DO.Station> GetAllStationBy(Predicate<DO.Station> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void addStation(DO.Station station)
        {
            if (DataSource.stationsList.FirstOrDefault(p => p.Id == station.Id) != null)
                throw new DO.BadStationIdException(station.Id, $"bad Station id: {station.Id}");
            DataSource.stationsList.Add(station.clone());//צריך ליצור קלון
        }

        public void deleteStation(int id)
        {
            DO.Station per = DataSource.stationsList.Find(p => p.Id == id);

            if (per != null)
                DataSource.stationsList.Remove(per);
            else
                throw new DO.BadStationIdException(id, $"bad Station id: {id}");

        }

        public void UpdetStation(DO.Station station)
        {
            DO.Station per = DataSource.stationsList.Find(p => p.Id == station.Id);
            if (per != null)
            {
                DataSource.stationsList.Remove(per);//מחיקה
                DataSource.stationsList.Add(station.clone());//הוספה מעודכן
            }

            else
                throw new DO.BadDronIdException(drone.Id, $"bad drone id: {drone.Id}")

        }

        public void UpdetStation(int id, Action<DO.Station> action)
        {
            throw new NotImplementedException();
        }









        public void AddStation(Station st)//מוסיף תחנת בסיס add a station
        {
            DataSource.stationsList.Add(st);
            DataSource.Config.amountStationId++;

        }


        public Station SearchStation(int id)//מחפש ,תחנה ךפי ת"ז search station by id
        {
            foreach (Station dr in DataSource.stationsList)
            {
                if (dr.Id == id)
                    return dr;
            }
            return new Station();
        }
        #endregion

        #region Parcle
        public DO.Parcle GetParcle(int id)
        {
            DO.Parcle per = DataSource.parcelList.Find(p => p.Id == id);
            if (per != null)
                return per;
            else
                return throw DO.hjghjfljgl(id, $"bad Parcle id: {id}");//שרה הקישקוש של השם זה השם של הזריקה
        }

        public IEnumerable<DO.Parcle> GetAllParcle()
        {
            return from Parcle in DataSource.parcelList
                   select Parcle.clone();
        }

        public IEnumerable<DO.Parcle> GetAllParcleBy(Predicate<DO.Parcle> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void addParcle(DO.Parcle parcle)
        {
            if (DataSource.parcelList.FirstOrDefault(p => p.Id == parcle.Id) != null)
                throw new DO.BadParcleIdException(parcle.Id, $"bad drone id: {parcle.Id}");
            DataSource.parcelList.Add(parcle.clone());//צריך ליצור קלון
        }

        public void deleteParcle(int id)
        {
            DO.Parcle per = DataSource.parcelList.Find(p => p.Id == id);

            if (per != null)
                DataSource.parcelList.Remove(per);
            else
                throw new DO.BadParcleIdException(id, $"bad Parcle id: {id}")

        }

        public void UpdetParcle(DO.Parcle parcle)
        {
            DO.Parcle per = DataSource.parcelList.Find(p => p.Id == parcle.Id);
            if (per != null)
            {
                DataSource.parcelList.Remove(per);//מחיקה
                DataSource.parcelList.Add(parcle.clone());//הוספה מעודכן
            }

            else
                throw new DO.BadParcleIdException(parcle.Id, $"bad drone id: {parcle.Id}")

        }

        public void UpdetParcle(int id, Action<DO.Parcle> action)
        {
            throw new NotImplementedException();
        }



















        public int AddParcel(Parcel pr)//מוסיף הזמנה add a parcel
        {
            pr.Id = DataSource.Config.amountParcelId;
            DataSource.parcelList.Add(pr);
            DataSource.Config.amountParcelId++;
            return DataSource.Config.amountParcelId - 1;
        }

        public Parcel SearchParcle(int id)//מחפש חבילה ךפי ת"ז search parcel by id
        {
            foreach (Parcel dr in DataSource.parcelList)
            {
                if (dr.Id == id)
                    return dr;
            }
            return new Parcel();//exeption
        }
        #endregion

        #region DroneCharg
        public DO.Parcle GetDroneCharg(int id)
        {
            DO.DroneCharg per = DataSource.droneChargeList.Find(p => p.Id == id);
            if (per != null)
                return per;
            else
                return throw DO.hjghjfljgl(id, $"bad DroneCharg id: {id}");//שרה הקישקוש של השם זה השם של הזריקה
        }

        public IEnumerable<DO.Drone> GetAllDrone()
        {
            return from Drone in DataSource.dronsList
                   select Drone.clone();
        }

        public IEnumerable<DO.Drone> GetAllDroneBy(Predicate<DO.Drone> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void addDrone(DO.Drone drone)
        {
            if (DataSource.dronsList.FirstOrDefault(p => p.Id == drone.Id) != null)
                throw new DO.BadDronIdException(drone.Id, $"bad drone id: {drone.Id}");
            DataSource.dronsList.Add(drone.clone());//צריך ליצור קלון
        }

        public void deleteCDrone(int id)
        {
            DO.Drone per = DataSource.dronsList.Find(p => p.Id == id);

            if (per != null)
                DataSource.dronsList.Remove(per);
            else
                throw new DO.BadDronIdException(id, $"bad drone id: {id}")

        }

        public void UpdetDrone(DO.Drone drone)
        {
            DO.Drone per = DataSource.dronsList.Find(p => p.Id == drone.Id);
            if (per != null)
            {
                DataSource.dronsList.Remove(per);//מחיקה
                DataSource.dronsList.Add(drone.clone());//הוספה מעודכן
            }

            else
                throw new DO.BadDronIdException(drone.Id, $"bad drone id: {drone.Id}")

        }

        public void UpdetDrone(int id, Action<DO.Drone> action)
        {
            throw new NotImplementedException();
        }












        public void AddDroneCharge(DroneCharge cs)//מוסיף רחפנים לעמדות טעינה add drone to the charge spot
        {
            DataSource.droneChargeList.Add(cs);

        }
        #endregion

        #region Customer
        public DO.Customer GetDroneCustomer(int id)
        {
            DO.Customer per = DataSource.customerList.Find(p => p.Id == id);
            if (per != null)
                return per;
            else
                return throw DO.hjghjfljgl(id, $"bad Customer id: {id}");//שרה הקישקוש של השם זה השם של הזריקה
        }

        public IEnumerable<DO.Customer> GetAllCustomer()
        {
            return from Customer in DataSource.customerList
                   select Customer.clone();
        }

        public IEnumerable<DO.Customer> GetAllCustomerBy(Predicate<DO.Customer> predicate)
        {
            throw new NotImplementedException();//זריקה
        }

        public void addCustomer(DO.Customer customer)
        {
            if (DataSource.customerList.FirstOrDefault(p => p.Id == customer.Id) != null)
                throw new DO.BadCustomerIdException(customer.Id, $"bad customer id: {customer.Id}");
            DataSource.customerList.Add(customer.clone());//צריך ליצור קלון
        }

        public void deleteCustomer(int id)
        {
            DO.Customer per = DataSource.customerList.Find(p => p.Id == id);

            if (per != null)
                DataSource.customerList.Remove(per);
            else
                throw new DO.BadCustomerIdException(id, $"bad customer id: {id}")

        }

        public void UpdetCustomer(DO.Customer customer)
        {
            DO.Customer per = DataSource.customerList.Find(p => p.Id == customer.Id);
            if (per != null)
            {
                DataSource.customerList.Remove(per);//מחיקה
                DataSource.customerList.Add(customer.clone());//הוספה מעודכן
            }

            else
                throw new DO.BadDronCustomerException(customer.Id, $"bad drone id: {customer.Id}");

        }

        public void UpdetCustomer(int id, Action<DO.Customer> action)
        {
            throw new NotImplementedException();
        }










        public void AddCustomer(Customer cs)//מוסיף לקוח
        {
            DataSource.Config.amountCustomerId++;
            DataSource.customerList.Add(cs);
        }

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
            Parcel pr = SearchParcle(idParcel);
            foreach (Drone dr in DataSource.dronsList)//for 
            {
                if (dr.StatusDrone== IDAL.Status.available)
                {
                    int id = dr.Id;
                    pr.Droneld = dr.Id;//שיוך חבילה לרחפן
                    Drone dd = SearchDrone(id);
                    dd.StatusDrone = IDAL.Status.delivered;//להעביר את הרחפן למצב שהוא במשלוח
                    DataSource.dronsList[i] = dr;
                    pr.Scheduled = DateTime.Now;//עדכון זמן שיוך חבילה
                    return true;
                }
            }
            return false;

        }
        public void PackageCollectionByDrone(int idParcel)//Package collection by Drone
        {
            Parcel pr = SearchParcle(idParcel);
            Drone dr = SearchDrone(pr.Droneld);
            pr.PichedUp = DateTime.Now;//עדכון זמן הגעת הרחפן לשולח חבילה
        }

        public void DeliveryOfPackageToTheCustomer(int idParcel)//Delivery of a package to the customer
        {
            Parcel pr = SearchParcle(idParcel);
            Drone dr = SearchDrone(pr.Droneld);
            dr.StatusDrone = IDAL.Status.available;//להעביר את הרחפן למצב שהוא זמין
            pr.Delivered = DateTime.Now;//עדכון זמן שיוך הגעת חבילה למקבל

        }
        public void DroneDkimmerForCharging(DroneCharge cs)//Drone a skimmer for charging
        {
            Drone dr = SearchDrone(cs.Droneld);
            Station st = SearchStation(cs.Stationld);
            st.ChargeSlots--;//להוריד את כמות המקומות השעינה הפנויות
            dr.StatusDrone = IDAL.Status.available;//להעביר את הרחפן למצב שהוא בטעינה
            AddDroneCharge(cs);//להוסיף אותו לרשימה של הרחפנים בטעינה
        }
        public void ReleaseDroneFroCharging(int idDrone)//Release Drone from charging
        {
            int stationCod;
            Station dd;
            //צריך למחוק את הרחפן מרשימת טעינה
            foreach (DroneCharge ds in DataSource.droneChargeList) {
                if (ds.Droneld == idDrone)
                {
                     stationCod = ds.Stationld;
                    dd = SearchStation(stationCod);
                    dd.ChargeSlots++;//צריך לעלות את כמות המקומות הטעינה
                    break;
                }
                
            }
            Drone dr = SearchDrone(idDrone);
           dr.StatusDrone = IDAL.Status.available;//להעביר את הרחפן למצב שהוא זמין
            
        }
        // .אפשרויות הצגת הרשימות
        public static void PrindDroneChargeList()//הדפסת רשימת הרחפנים בטעינה print the list of drones in charging
        {
            foreach (Drone dr in DataSource.dronsList) 
            {
                if(dr.StatusDrone==IDAL.Status.InMaintenance)
                {
                    Console.WriteLine(dr.ToString());
                }
            }

        }
        public static void PrindBaseStationList()
                                          // הצגת רשימת תחנות-בסיס show base-station list
        {
            foreach (Station station in DataSource.stationsList/*Drone dr in DataSource.dronsList*/)
            {
                Console.WriteLine(station.ToString());
            }

        }
        //השאלה אם זה תחנות טעינה או רק תחנות רגילות.בכל אופן אני אדפיס פעולה אחרת לתחנות טעינה

        public static void PrintDronesList()//הצגת רשימת הרחפנים show drone list
        {
            foreach(Drone dr in DataSource.dronsList)
            {
                Console.WriteLine(dr.ToString());
            }
        }
        public static void PrintCustomersList()//הצגת רשימת הלקוחות show customers list
        {
            foreach (Customer customer in DataSource.customerList)
            {
              Console.WriteLine(  customer.ToString());
            }
        }
        public static void PrintParcelsList()//הצגת רשימת החבילות show parcels list
        {
            foreach (Parcel parcel in DataSource.parcelList)
            {
                parcel.ToString();
            }
        }
        public static void PrintUnconnectedParceslList()// הצגת רשימת חבילות שעוד לא שויכו לרחפן  show unconnected parcesl list
        {
            foreach (Parcel parcel in DataSource.parcelList)
            {
                if(parcel.Id==0)
                {
                    Console.WriteLine(parcel.ToString());
                }

            }
        }
        public static void PrintAvailableStationToChargeList()//הצגת תחנות-בסיס עם עמדות טעינה פנויות show stations with available charge slots
        {
            foreach (Station station in DataSource.stationsList)
            {
                if(station.ChargeSlots>0)
                {
                    Console.WriteLine(station.ToString());
                }
            }
        }

        //אפשרויות תצוגה כולן ע"פ מספר מזהה מתאים
        public static void PrintBaseStation(int stationId)//תצוגת תחנת-בסיס show station details by id
        {
            foreach (Station station in DataSource.stationsList)
            {
                if(station.Id==stationId)
                {
                    Console.WriteLine(station.ToString());
                    break;
                }
            }
        }
        public static void PrintDrone(int droneId)// תצוגת רחפן  show drone details by id
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


        public static void PrintCustomer(int customerId)//תצוגת לקוח shoe customer details by id
        {
            foreach(Customer customer in DataSource.customerList)
            {
                if(customer.Id==customerId)
                {
                    Console.WriteLine(customer.ToString());
                    break;
                }
            }
        }
        public static void PrintParcel(int parcelId)//תצוגת חבילה show parcel details by id
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





    }

}
