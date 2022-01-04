using System;
using System.Collections.Generic;
using System.Text;

//namespace IDAL
//{
    namespace IDAL.DO
{
        public struct  DroneCharge//הטענת רחפן
        {
            public int Droneld { get; set; }
            public int Stationld { get; set; }
            //public override string ToString()
            //{
            //    return $"DroneCharge: Droneld={Droneld}, Stationld={Stationld}";


            //}
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    //}
}
