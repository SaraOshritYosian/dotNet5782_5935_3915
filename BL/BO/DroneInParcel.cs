﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
  public  class DroneInParcel
    {
        public int Id { get; set; }
        public double StatusBatter { get; set; }
        public Location CurrentLocation { get; set; }
    }
}
