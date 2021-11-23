using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelToList
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string TargetName { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public ParcelStatus Status { get; set; }

        public override string ToString()
        {
            return
                $"ID:                            {Id}\n" +
                $"Sender name:                   {SenderName}\n" +
                $"Target name:                   {TargetName}\n" +
                $"Weight:                        {Weight}\n" +
                $"Priority:                      {Priority}\n" +
                $"Status:                        {Status}";
        }
    }
}
