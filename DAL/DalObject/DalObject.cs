using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject//במיין בהוספה את מקבלת את הנתונים ומכניסה אותם לאובייקט שאותו את שולחת כפרמטר לפונקצית הוספה שבdalobject
{
    public class DalObject
    {
        public DalObject()// בנאי של דלאובצקט והיא המחלקה שקונסול יעשה לה ניו מתי שהוא ירצה להתחיל והיא שניקרא לפונקציות בדתסורס
        {
            DataSource.Initialize();
        }


        public void AddDrone(Drone dr)//מוסיף רחפן add a drone
        {
            DataSource.dronsList.Add(dr);
            DataSource.Config.amountDorneId++;

        }
        public void AddStation(Station st)//מוסיף תחנת בסיס add a station
        {
            DataSource.stationsList.Add(st);
            DataSource.Config.amountStationId++;

        }
        public void AddCustomer(Customer cs)//מוסיף לקוח add a customer
        {
            DataSource.customerList.Add(cs);
            DataSource.Config.amountCustomerId++;
        }
        public void AddDroneCharge(DroneCharge cs)//מוסיף רחפנים לעמדות טעינה add drone to the charge spot
        {
            DataSource.droneChargeList.Add(cs);
           
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
            return new Parcel();
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
        public Customer SearchCustomer(int id)//מחפש לקוח ךפי ת"ז search customer by id
        {
            foreach (Customer dr in DataSource.customerList)
            {
                if (dr.Id == id)
                    return dr;
            }
            return new Customer();
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

        public bool AssignPackageToDrone(int idParcel)//Assign a Package To Drone
        {
            Parcel pr = SearchParcle(idParcel);
            foreach (Drone dr in DataSource.dronsList)
            {
                if (dr.StatusDrone== IDAL.Status.available)
                {
                    int id = dr.Id;
                    pr.Droneld = dr.Id;//שיוך חבילה לרחפן
                    Drone dd = SearchDrone(id);
                    dd.StatusDrone = IDAL.Status.delivered;//להעביר את הרחפן למצב שהוא במשלוח
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
                    dr.ToString();
                }
            }

        }
        public static void PrindBaseStationList()
                                          // הצגת רשימת תחנות-בסיס show base-station list
        {
            foreach (Station station in DataSource.stationsList/*Drone dr in DataSource.dronsList*/)
            {
                station.ToString();
            }

        }
        //השאלה אם זה תחנות טעינה או רק תחנות רגילות.בכל אופן אני אדפיס פעולה אחרת לתחנות טעינה

        public static void PrintDronesList()//הצגת רשימת הרחפנים show drone list
        {
            foreach(Drone dr in DataSource.dronsList)
            {
                dr.ToString();
            }
        }
        public static void PrintCustomersList()//הצגת רשימת הלקוחות show customers list
        {
            foreach (Customer customer in DataSource.customerList)
            {
                customer.ToString();
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
                    parcel.ToString();
                }

            }
        }
        public static void PrintAvailableStationToChargeList()//הצגת תחנות-בסיס עם עמדות טעינה פנויות show stations with available charge slots
        {
            foreach (Station station in DataSource.stationsList)
            {
                if(station.ChargeSlots>0)
                {
                    station.ToString();
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
                    station.ToString();
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
                    drone.ToString();
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
                    customer.ToString();
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
                    parcel.ToString();
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
