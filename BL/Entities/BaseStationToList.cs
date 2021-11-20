using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class BaseStationToList
    {
        public int Id;
        public string Name;
        public int NumFreeChragers;
        public int NumFullChragers;

        public override string ToString()
        {
            return
                $"ID:                            {Id}\n" +
                $"Name                           {Name}\n" +
                $"Free Chargers:                 {NumFreeChragers}\n" +
                $"Occupied Chargers:             {NumFullChragers}";
        }
    }

}
