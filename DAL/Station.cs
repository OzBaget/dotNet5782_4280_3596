using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Lng { get; set; }
            public double Lat { get; set; }
            public int FreeChargeSlots { get; set; }
            public override string ToString()
            {
                return $"Station Id: {Id}\n" +
                    $"Name: {Name}\n" +
                   $"Position: {Lng},{Lat}\n" +
                    $"ChargeSlots: {FreeChargeSlots}";
            }
        }
    }
}
