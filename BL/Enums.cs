using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
    {
    namespace BO
    {

        public class Enums
        {
            public enum StatusDrone { available, InMaintenance, delivered }//מצב הרחפן drone ststus
       
            public enum WeightCategories { Light, Medium, Heavy }//סוג החבילה parcel type
            public enum Priority { simple, quick, emergency }//עדיפות למשלוח dellivery priority
            public enum SituationParcel { Created, associated, collected, provided }//מצב חבילה נוצר, שוייך, נאסף,סןפק
            
        }
    }
}
