﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

namespace DO
{
    public struct Customer//מייצג לקוח
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pone { get; set; }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }
     
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }



}
