﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;



 namespace BO
    {

         public enum StatusDrone { available, InMaintenance, delivered }//מצב הרחפן drone ststus

          public enum WeightCategories { Light, Medium, Heavy }//סוג החבילה parcel type
          public enum Priority { simple, quick, emergency }//עדיפות למשלוח dellivery priority
          public enum StatusParcel { Created, associated, collected, provided }//מצב חבילה נוצר, שוייך, נאסף,סןפק

     
   
}
