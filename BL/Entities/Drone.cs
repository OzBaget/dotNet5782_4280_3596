using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Drone
    {
        public int Id;
        public string Model;
        public WeightCategories MaxWeight;
        public int Battery;
        public DroneStatus Status;
        public ParcelInTransfer Parcel;
        public Location CurrentLocation;

        public override string ToString()
        {
            string tostringText =
                $"ID:                            {Id}\n" +
                $"Model:                         {Model}\n" +
                $"Max weight:                    {MaxWeight}\n" +
                $"Battery:                       {Battery}%\n" +
                $"Status:                        {Status}\n"+
                $"Current location:              {CurrentLocation}";
            if (Parcel.Id != 0)
                tostringText += $"========Parcel=========================\n\t{Parcel.ToString().Replace("\n", "\n\t")}\n";
            return tostringText;
        }
    }
}
