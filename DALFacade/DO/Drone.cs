using System;
using System.Collections.Generic;
using System.Text;


    namespace DO
    {
        public struct Drone//מייצג רחפן
        {

            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories Weight { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }

        }
    }

