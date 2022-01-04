
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;
namespace IBL.BO
{
    public class DroneToList

    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories Weight { get; set; }
        public double StatusBatter { get; set; }
        public StatusDrone StatusDrone { get; set; }

        public int IdParcel { get; set; }//אם יש
        public Location LocationDrone { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
