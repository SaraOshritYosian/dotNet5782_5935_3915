using System;
using System.Collections.Generic;
using System.Text;


    namespace DO
    {
        public struct Station//תחנה להטעין
        {
            public int Name { get; set; }
            public int Id { get; set; }
            public int ChargeSlots { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }

           
            public override string ToString()
            {
                return this.ToStringProperty();
            }


        }
    }

