using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;
namespace IBL.BO
{
   public class ParcelToLIst
    {
        public int Id { get; set; }
        public string SenderName{ get; set; }//לקוח שולח
        public string TargetName { get; set; }//לקוח מקבל
        public WeightCategories Weight { get; set; }
        public Priority Priority { get; set; }
        public StatusParcel statusParcel { get; set; }//מצב חבילה
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
