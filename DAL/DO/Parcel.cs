using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct  Parcel//משלוח
        {
            public int Id { get; set; }
            public int Senderld { get; set; }//לקוח שולח
            public int Targetld { get; set; }//לקוח מקבל
            public WeightCategories Weight { get; set; }
            public Priority Priority { get; set; }

            public int Droneld { get; set; }//רחפן מבצע
            public DateTime Requested { get; set; }//זמן יצירת חבילהparcel creation time
            public DateTime Scheduled { get; set; }//זמן שיוך חבילה parcel scedualed time
            public DateTime PichedUp { get; set; }//זמן איסוף חבילה parcel pick up tome
            public DateTime Delivered { get; set; }//זמן הגעת חבילה למקבל parcel dellivery time
                                                   //public override string ToString()
                                                   //{
                                                   //    return $"customer: Id={Id}, Senderld={Senderld}, Targetld={Targetld},Weight={Weight},Priority={Priority},Droneld={Droneld},Requested={Requested},Scheduled={Scheduled},PichedUp={PichedUp},Delivered={Delivered}";


            //}
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
}
