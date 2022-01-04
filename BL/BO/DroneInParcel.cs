using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enums
{
  public  class DroneInParcel
    {
        public int Id { get; set; }
        public double StatusBatter { get; set; }
        public Location LocationDroneInParcel { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
