using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneBL
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public Status StatusDrone { get; set; }
            public WeightCategories Weight { get; set; }

        }
    }
}
