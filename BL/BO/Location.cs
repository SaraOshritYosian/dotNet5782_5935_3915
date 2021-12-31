using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlApi
{
    namespace BO
    {
        public class Location
        {
            public Double Longitude { get; set; }
            public Double Latitude { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
}
