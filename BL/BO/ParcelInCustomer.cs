﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO 
{
    class ParcelInCustomer
    {
        public int Id { get; set; }
        public WeightCategories Weight { get; set; }
        public Priority Priority { get; set; }
        public SituationParcel situation { get; set; }
        public CustomerInParcel Senderld { get; set; }//לקוח בחבילה
    }
}