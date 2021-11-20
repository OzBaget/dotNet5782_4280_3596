using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Parcel
    {
        public int Id;
        public CustomerInParcel Sender;
        public CustomerInParcel Target;
        public WeightCategories Weight;
        public Priorities Prioritie;
        public DroneInParcel Drone;
        public DateTime DateCreated;
        public DateTime DateScheduled;
        public DateTime DatePickup;
        public DateTime DateDeliverd;
        public override string ToString()
        {
            string toString =
                $"ID:                 {Id}\n" +
                $"Sender:             {Sender}\n" +
                $"Receiver:           {Target}\n" +
                $"Weight status:      {Weight}\n" +
                $"Prioritie status:   {Prioritie}\n" +
                $"Parcel's Drone :    {Drone}\n" +
                $"Created date:       {DateCreated}\n" +
                $"Scheduled date:     {DateScheduled}\n" +
                $"Pick up date:       {DatePickup}\n" +
                $"Deliverd date:      {DateDeliverd}\n";

            return toString;
        }
    }
}
