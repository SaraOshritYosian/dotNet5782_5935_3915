﻿using System;
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

            public static double Free=0.5;
            public static double LightWeight=3;
            public static double MediumWeight=4;
            public static double HeavyWeight=15;
            public static double LoadingPrecents=20;//  הוספתי 20,קצב טעינת רחפן באחוזים לשעה
            internal static int amountDorneId = 0;//כמות הרחפנים
            internal static int amountStationId = 0;//כמות התחנות
            internal static int amountCustomerId = 0;//כמות הלקוחות
            internal static int amountParcelId = 0;//כמות חבילות
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
            dronsList.Add(new Drone() { Id = 0, Model = "2MO3",  Weight = WeightCategories.Light });
            dronsList.Add(new Drone() { Id = 1, Model = "V78K",Weight = WeightCategories.Heavy });
            dronsList.Add(new Drone() { Id = 2, Model = "5TD7", Weight = WeightCategories.Medium });
            dronsList.Add(new Drone() { Id = 3, Model = "FS001", Weight = WeightCategories.Medium });
            stationsList.Add(new Station() { Name = 123, Id = 1, ChargeSlots = 6, Longitude = 0.3340, Latitude = 9.33340 });
            stationsList.Add(new Station() { Name = 1234, Id = 2, ChargeSlots = 30, Longitude = 111.23, Latitude = 43.99 });
            customerList.Add(new Customer() { Id = 32424593, Name = "chana", Lattitude = 33.44, Longitude = 544.554, Pone = "0504378982" });
          //  parcelList.Add(new Parcel() { Id = Config.amountParcelId++, Senderld = 0, Targetld = 0, Weight = 0, Priority = 0, Droneld = 0, Requested = DateTime.Now, Scheduled = DateTime.Now, PichedUp = DateTime.Now, Delivered = DateTime.Now });
            Config.amountStationId++;
            Config.amountStationId++;
            Config.amountCustomerId++;
            Config.amountDorneId+=4;

        }

    }
}
