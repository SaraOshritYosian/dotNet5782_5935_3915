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
        internal class Config
        {
            internal static int amountDorneId = 0;//כמות הרחפנים
            internal static int amountStationId = 0;//כמות התחנות
            internal static int amountCustomerId = 0;//כמות הלקוחות
            internal static int amountParcelId = 0;//כמות חבילות
            //מונה לחבילות והמונה ות"ז של חבילות יהיו זהים
        }
        //הרשימות
        internal static List<Drone> drons=new  List<Drone>;//רשימה של רחפנים
         internal static List<Station> stations=new  List<Station>;//רשימה של תחנות
         internal static List<Customer> customer=new  List<Customer>;//רשימה של לקוחות
         internal static List<Parcel> parcel=new  List<Parcel>;//רשימה של חבילות
        static Random rand = new Random();//add current time

        private static void creatDrone(int num)//פונקציה שיוצרת רחפנים
        {
            Config.amountDorneId++; //.... באחד amountDorneId שיוצרים רחפן זה מגדיל את
        }
        static Initialize()//קורא לכל היצירות
        {

        }
    }
}
