using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelToList
    {
        public int Id;
        public string SenderName;
        public string TargetName;
        public WeightCategories Weight;
        public Priorities Priority;
        public ParcelStatus Status;

        public override string ToString()
        {
            return 
                $"ID: {Id}\n" +
                $"Sender name: {SenderName}\n" +
                $"Target name: {TargetName}\n" +
                $"Weight: {Weight}\n" +
                $"Priority: {Priority}\n" +
                $"Status: {Status}";
        }
    }
}
