using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct Station//תחנה להטעין
        {
            public int Name { get; set; }
            public int Id { get; set; }
            public int ChargeSlots { get; set; }
            public Double Longitude { get; set; }
            public Double Latitude { get; set; }

            //public override string ToString()
            //{
            //    return $"station:  Name={Name} ,Id ={Id}, ChargeSlots={ChargeSlots}, longitude={Longitude},latitude={Latitude}";


            //}
            public override string ToString()
            {
                return this.ToStringProperty();
            }


        }
    }
   
}
