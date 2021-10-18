using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requsted { get; set; }
            public int DroneId { get; set; }  //TODO: default: 0
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }

            public override string ToString()
            {
                string details = $"Parcel ID: {Id}\n" +
                    $"Sender ID: {SenderId}\n" +
                    $"Target ID: {TargetId}\n" +
                    $"Weight: {Weight}\n" +
                    $"Priority: {Priority}\n" +
                    $"Assigned Drone ID: {DroneId}\n"+            
                    $"Requsted Time: {Requsted.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.GetCultureInfo("en-us", "en"))}";
                if (Scheduled != DateTime.MinValue) //Scheduled !=null
                    details += $"\nScheduled Time: {Scheduled.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.GetCultureInfo("en-us", "en"))}";

                if (PickedUp != DateTime.MinValue) //PickedUp !=null
                    details += $"\nPickedUp Time: {PickedUp.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.GetCultureInfo("en-us", "en"))}";

                if (Delivered != DateTime.MinValue) //Delivered !=null
                    details += $"\nDelivered Time: {Delivered.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.GetCultureInfo("en-us", "en"))}";
                
                return details;
            }
        }
    }
}
