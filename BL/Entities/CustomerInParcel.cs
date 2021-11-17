using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class CustomerInParcel
    {
        public int Id;
        public string Name;
        public override string ToString()
        {
            string toString =
                $"ID:            {Id}\n" +
                $"Name:          {Name}\n";
            return toString;
        }
    }
}
