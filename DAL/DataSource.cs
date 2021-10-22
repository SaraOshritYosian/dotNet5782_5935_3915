using System;
using System.Collections.Generic;
using System.Text;

namespace DalObject
{
     public class DataSource//זה כל המערכים או שנעשה רשימות לא ידוע
    {
        internal class Config
        {
            internal static int amountDorneId = 0;//כמות הרחפנים
            //מונה לחבילות והמונה ות"ז של חבילות יהיו זהים
        }
        //הרשימות
        static Random rand = new Random();//add current time


        private static void creatDrone(int num)//פונקציה שיוצרת רחפנים
        {
            Config.amountDorneId++; //.... באחד amountDorneId שיוצרים רחפן זה מגדיל את
        }
    }
}
