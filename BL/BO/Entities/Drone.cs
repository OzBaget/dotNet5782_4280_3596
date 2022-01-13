namespace BO
{
    public class Drone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double Battery { get; set; }
        public DroneStatus Status { get; set; }
        public ParcelInTransfer Parcel { get; set; }
        public Location CurrentLocation { get; set; }

        public override string ToString()
        {
            string tostringText =
                $"ID:                            {Id}\n" +
                $"Model:                         {Model}\n" +
                $"Max weight:                    {MaxWeight}\n" +
                $"Battery:                       {Battery}%\n" +
                $"Status:                        {Status}\n" +
                $"Current location:              {CurrentLocation}";
            if (Parcel.Id != 0)
                tostringText += $"\n        ============Parcel=============\n\t{Parcel.ToString().Replace("\n", "\n\t")}\n";
            return tostringText;
        }
    }
}
