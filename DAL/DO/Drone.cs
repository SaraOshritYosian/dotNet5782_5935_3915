using System;
using System.Collections.Generic;
using System.Text;

//namespace IDAL
//{
    namespace DO
    {
        public struct Drone//מייצג רחפן
        {

            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories Weight { get; set; }
            //public Status StatusDrone { get; set; }
            //public double Battery { get; set; }
            //public override string ToString()
            //{
            //    //return $"drone: Id={Id}, Model={Model}, StatusDrone={StatusDrone},Weight={Weight},Battery={Battery}";

            //    return $"drone: Id={Id}, Model={Model},Weight={Weight}";
            //}
            public override string ToString()
            {
                return this.ToStringProperty();
            }

        }
    //}
}
