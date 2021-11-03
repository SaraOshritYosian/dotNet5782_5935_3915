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


        public void AddDrone(Drone dr)//מוסיף רחפן
        {
            DataSource.dronsList.Add(dr);
            DataSource.Config.amountDorneId++;

        }
        public void AddStation(Station st)//מוסיף תחנת בסיס
        {
            DataSource.stationsList.Add(st);
            DataSource.Config.amountStationId++;

        }
        public void AddCustomer(Customer cs)//מוסיף לקוח
        { 
            DataSource.Config.amountCustomerId++;
            DataSource.customerList.Add(cs);
        }
        public void AddDroneCharge(DroneCharge cs)//מוסיף רחפנים לעמדות טעינה
        {
            DataSource.droneChargeList.Add(cs);
           
        }
        public int AddParcel(Parcel pr)//מוסיף הזמנה
        {
            pr.Id = DataSource.Config.amountParcelId;
            DataSource.parcelList.Add(pr);
            DataSource.Config.amountParcelId++;
            return DataSource.Config.amountParcelId - 1;
        }
        public Parcel SearchParcle(int id)//מחפש חבילה ךפי ת"ז
        {
            foreach (Parcel dr in DataSource.parcelList)
            {
                if (dr.Id == id)
                    return dr;
            }
            return new Parcel();
        }
        public Drone SearchDrone(int id)//מחפש רחפן ךפי ת"ז
        {
            foreach (Drone dr in DataSource.dronsList)
            {
                if (dr.Id == id)
                    return dr;
            }
            return new Drone();
        }
        public Customer SearchCustomer(int id)//מחפש לקוח ךפי ת"ז
        {
            foreach (Customer dr in DataSource.customerList)
            {
                if (dr.Id == id)
                    return dr;
            }
            return new Customer();
        }
        public Station SearchStation(int id)//מחפש ,תחנה ךפי ת"ז
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
        public static void PrindDroneChargeList()//הדפסת רשימת הרחפנים בטעינה
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
                                          // הצגת רשימת תחנות-בסיס
        {
            foreach (Station station in DataSource.stationsList/*Drone dr in DataSource.dronsList*/)
            {
                Console.WriteLine(station.ToString());
            }

        }
        //השאלה אם זה תחנות טעינה או רק תחנות רגילות.בכל אופן אני אדפיס פעולה אחרת לתחנות טעינה

        public static void PrintDronesList()//הצגת רשימת הרחפנים
        {
            foreach(Drone dr in DataSource.dronsList)
            {
                Console.WriteLine(dr.ToString());
            }
        }
        public static void PrintCustomersList()//הצגת רשימת הלקוחות
        {
            foreach (Customer customer in DataSource.customerList)
            {
              Console.WriteLine(  customer.ToString());
            }
        }
        public static void PrintParcelsList()//הצגת רשימת החבילות 
        {
            foreach (Parcel parcel in DataSource.parcelList)
            {
                parcel.ToString();
            }
        }
        public static void PrintUnconnectedParceslList()// הצגת רשימת חבילות שעוד לא שויכו לרחפן 
        {
            foreach (Parcel parcel in DataSource.parcelList)
            {
                if(parcel.Id==0)
                {
                    Console.WriteLine(parcel.ToString());
                }

            }
        }
        public static void PrintAvailableStationToChargeList()//הצגת תחנות-בסיס עם עמדות טעינה פנויות
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
        public static void PrintBaseStation(int stationId)//תצוגת תחנת-בסיס
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
        public static void PrintDrone(int droneId)// תצוגת רחפן 
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


        public static void PrintCustomer(int customerId)//תצוגת לקוח
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
        public static void PrintParcel(int parcelId)//תצוגת חבילה
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
        public void PrintFreeBaseStationList(int idDrone)//הדפסת רשימת התחנות  שיש בהם עמדות טעינה פנויות
        {
            foreach (Station dr in DataSource.stationsList) {
                if (dr.ChargeSlots > 0)
                    Console.WriteLine(dr.Id);

            }

        }

    }

}
