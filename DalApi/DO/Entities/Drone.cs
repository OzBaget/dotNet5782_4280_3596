namespace DO
{
    public struct Drone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public bool IsActived { get; set; }
        public override string ToString()
        {
            return $"Drone ID: {Id}\n" +
                $"Modle: {Model}\n" +
                $"Max Weight: {MaxWeight}\n";
        }
    }
}
