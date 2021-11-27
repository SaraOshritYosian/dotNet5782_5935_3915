using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DataSource//זה כל המערכים או שנעשה רשימות לא ידוע
    {

        //הרשימות
        internal static List<Drone> dronsList = new List<Drone>();//רשימה של רחפנים
        internal static List<Station> stationsList = new List<Station>();//רשימה של תחנות
        internal static List<Customer> customerList = new List<Customer>();//רשימה של לקוחות
        internal static List<Parcel> parcelList = new List<Parcel>();//רשימה של חבילות
        internal static List<DroneCharge> droneChargeList = new List<DroneCharge>();//רשימה של עמדות טעינה
        internal static Random rand = new Random(DateTime.Now.Millisecond);//add current time


        internal class Config
        {

           // public static int
            //internal static int amountDorneId = 0;//כמות הרחפנים
            //internal static int amountStationId = 0;//כמות התחנות
            //internal static int amountCustomerId = 0;//כמות הלקוחות
            //internal static int amountParcelId = 0;//כמות חבילות
            //מונה לחבילות והמונה ות"ז של חבילות יהיו זהים
        }
        private static void creatDrone(int num)//פונקציה שיוצרת רחפנים create a drone
        {
            Config.amountDorneId++; //.... באחד amountDorneId שיוצרים רחפן זה מגדיל את
        }

        private static void creatStation(int num)//פונקציה שיוצרת תחנה create a station
        {
            Config.amountStationId++; //.... באחד amountStationId שיוצרים רחפן זה מגדיל את
        }

        private static void creatCustomer(int num)//פונקציה שיוצרת לקוח create a customer
        {
            Config.amountCustomerId++; //.... באחד amountCustomerId שיוצרים רחפן זה מגדיל את
        }

        private static void creatParcel(int num)//פונקציה שיוצרת חבילה create a parcel
        {
            Config.amountParcelId++; //.... באחד amountParcelId שיוצרים רחפן זה מגדיל את
        }

        private static void creatDroneCharge(int num)//פונקציה שיוצרת עמדות טעינה create a charge slot
        {
            Config.amountDorneId++; //.... באחד amountDorneId שיוצרים רחפן זה מגדיל את
        }
        internal static void Initialize()//מאתחל את הרשימות וקורא לכל היצירות 
        {
            //dronsList.Add(new Drone() { Id = 0, Model = " ", Battery = 0, StatusDrone = 0, Weight = 0 });
            dronsList.Add(new Drone() { Id = 0, Model = " ",Weight = 0 });
            stationsList.Add(new Station() { Name = 0, Id = 0, ChargeSlots = 0, Longitude = 0.0, Latitude = 0 });
            customerList.Add(new Customer() { Id = 0, Name = " ", Lattitude = 0, Longitude = 0, Pone = " " });
            parcelList.Add(new Parcel() { Id = Config.amountParcelId++, Senderld = 0, Targetld = 0, Weight = 0, Priority = 0, Droneld = 0, Requested = DateTime.Now, Scheduled = DateTime.Now, PichedUp = DateTime.Now, Delivered = DateTime.Now });
            Config.amountStationId++;
            Config.amountCustomerId++;
            Config.amountDorneId++;

        }

    }
}
