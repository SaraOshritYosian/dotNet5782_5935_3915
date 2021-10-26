using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace DalObject//במיין בהוספה את מקבלת את הנתונים ומכניסה אותם לאובייקט שאותו את שולחת כפרמטר לפונקצית הוספה שבdalobject
{
    public class DalObject
    {
        public DalObject()// בנאי של דלאובצקט והיא המחלקה שקונסול יעשה לה ניו מתי שהוא ירצה להתחיל והיא שניקרא לפונקציות בדתסורס
        {
            DataSource.Initialize();
        }
        public void AddDrone()//יוצר רחפן
        {
           
        }
        public void AddStation(int id)//יוצר לקוח
        {
          
        }
        public void AddCustomer(int id)//יוצר תחנת בסיס
        {

        }
        public void AddParcel(int id)//יוצר הזמנה
        {
             
        }
    }
}
