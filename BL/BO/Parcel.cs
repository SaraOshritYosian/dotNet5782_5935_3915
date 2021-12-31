using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BlApi
{
    namespace BO
    {
        public class Parcel
        {
            public int Id { get; set; }
            public CustomerInParcel CustomerInParcelSender { get; set; }//לקוח שולח
            public CustomerInParcel CustomerInParcelTarget { get; set; }//לקוח מקבל
            public WeightCategories Weight { get; set; }
            public Priority Priority { get; set; }
            public DroneInParcel DroneInParcel { get; set; }
            public DateTime Requested { get; set; }//זמן יצירת חבילהparcel creation time
            public DateTime Scheduled { get; set; }//זמן שיוך חבילה parcel scedualed time
            public DateTime PichedUp { get; set; }//זמן איסוף חבילה parcel pick up tome
            public DateTime Delivered { get; set; }//זמן הגעת חבילה למקבל parcel dellivery time

        }
    }
    
}
