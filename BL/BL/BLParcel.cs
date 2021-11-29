﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL.DO;

namespace IBL
{
  public partial  class BLParcel
    {
private IDAL.DO.Parcel minDis(List<IDAL.DO.Parcel>parcels,Location location)
        {
            List<double> listDis = new List<double>();
            foreach(var obj in parcels)
            {

                Location locationSender = GetCustomer(obj.Senderld).location;//לסדר פעולתGetCustomer 
                listDis.Add(getdis(location, locationSender));//getdisצריך פעולת 
            }
            return parcels[listDis.FindIndex(x => x== listDis.Min())];
        }
        public void AddParcel(BO.Parcel parcel)
        {
            IDAL.DO.Parcel p = new IDAL.DO.Parcel();
            p.Senderld = parcel.Senderld.Id;
            p.Targetld = parcel.Targetld.Id;
            p.Weight = (IDAL.DO.WeightCategories)parcel.Weight;
            p.Priority = (IDAL.DO.WeightCategories)parcel.Priority;
            p.Requested = DateTime.Now;
            p.Scheduled = new DateTime();
            p.PichedUp = new DateTime();
            p.Delivered = new DateTime();
            try
            {

            }
        }
    }
}
