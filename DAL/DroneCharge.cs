using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct  DroneCharge//הטענת רחפן
        {
            public int Droneld { get; set; }
            public int Stationld { get; set; }
            public override string ToString()
            {
                return $"DroneCharge: Droneld={Droneld}, Stationld={Stationld}";


            }
        }
    }
}
