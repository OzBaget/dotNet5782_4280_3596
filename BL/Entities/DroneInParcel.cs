﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneInParcel
    {
        public int Id { get; set; }
        public double Battery { get; set; }
        public Location CurrentLocation { get; set; }
        public override string ToString()
        {
            string toString =
                $"ID:                            {Id}\n" +
                $"Battery:                       {Battery}%\n" +
                $"Location:                      {CurrentLocation}";

            return toString;
        }
    }
}