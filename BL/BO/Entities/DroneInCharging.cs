namespace BO
{
    public class DroneInCharging
    {
        public int Id { get; set; }
        public double Battery { get; set; }

        public override string ToString()
        {
            return
                $"ID:                            {Id}\n" +
                $"Battery:                       {Battery}%";
        }
    }

}
