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
        public CustomerInParcel Senderld { get; set; }//לקוח שולח
        public CustomerInParcel Targetld { get; set; }//לקוח מקבל
        public WeightCategories Weight { get; set; }
        public Priority Priority { get; set; }
        public SituationParcel situatinOfParcel { get; set; }//מצב חבילה

    }
}
