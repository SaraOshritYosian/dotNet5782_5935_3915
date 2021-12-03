﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class CustomerToList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pone { get; set; }
        public int NumberOfPackagesSentAndDelivered { get; set; }//NumberOfPackagesSentAndDelivered
        public int NumberOfPackagesSentAndNotDelivered { get; set; }//NumberOfPackagesSentAndNotDelivered
        public int NumberOfPackagesGet { get; set; }//NumberOfPackagesGet מספר חבילות קבל
        public int SeveralPackagesOnTheWayToTheCustomer { get; set; }//SeveralPackagesOnTheWayToTheCustomer מספר חבילות בדרך ללקוח
        public override string ToString()
        {

            return base.ToString() + string.Format("the id is:{0,6},\t name is:{1,-3})\n", Id, Name);
        }

    }
}
