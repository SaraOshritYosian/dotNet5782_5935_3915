using System;

namespace DAL
{
    namespace DO
    {


        public struct Drone//מייצג רחפן
        {

            public string Id { get; set; }
            public string Model { get; set; }
            public Status StatusDrone { get; set; }
            public WeightCategories Weight { get; set; }
            public string Battery { get; set; }
            public override string ToString()
            {
                return $"drone: Id={Id}, Model={Model}, StatusDrone={StatusDrone},Weight={Weight},Battery={Battery}";


            }


        }
    }
}
