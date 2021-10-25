using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    namespace DO
    {
        public struct Station//תחנה להטעין
        {
            public int Name { get; set; }
            public int Id { get; set; }
            public int ChargeSlots { get; set; }
            public Double longitude { get; set; }
            public Double latitude { get; set; }
            
            public override string ToString()
            {
                return $"station:  Name={Name} ,Id ={Id}, ChargeSlots={ChargeSlots}, longitude={longitude},latitude={latitude}";


            }


        }
    }
}
