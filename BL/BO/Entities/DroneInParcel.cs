namespace BO
{
    public class DroneInParcel
    {
        public int Id { get; set; }
        public double Battery { get; set; }
        public Location CurrentLocation { get; set; }
        public override string ToString()
        {
            string toString =
                $"ID:           {Id}\n" +
                $"Battery:      {Battery}%\n" +
                $"Location:     {CurrentLocation}";

            return toString;
        }
    }
}
