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
            DataSource.Config.Initialize();
        }


         public  void  AddDrone( Drone  dr)//מוסיף רחפן
        {
            DataSource.dronsList.Add( dr);

        }
        public  void AddStation(Station st)//מוסיף תחנת בסיס
        {
            DataSource.stationsList.Add(st);
        }
        public void AddCustomer(Customer cs)//מוסיף לקוח
        {
            DataSource.customerList.Add(cs);
        }
        public void AddParcel(Parcel pr)//מוסיף הזמנה
        {
            DataSource.parcelList.Add(pr);
        }
        public static Drone SearchDrone(int id)//מחפש רחפן ךפי ת"ז
        {
            foreach(Drone dr in DataSource.dronsList.)
            {
                if (dr.Id ==id)
                    return dr;
                else
                    return new Drone();

            }
        }
    }
}
