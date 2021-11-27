﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{

        public class Drone
        {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories Weight { get; set; }
        public StatusBattery StatusBatter { get; set; }
        public StatusDrone StatusDrone { get; set; }
       
        public PackageInTransfer PackageInTransfe { get; set; }
        public Location CurrentLocation { get; set; }
    }

}