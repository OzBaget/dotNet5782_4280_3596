using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneInCharging
    {
        public int Id;
        public double Battery;

        public override string ToString()
        {
            return
                $"ID:                            {Id}\n" +
                $"Battery:                       {Battery}%";
        }
    }

}
