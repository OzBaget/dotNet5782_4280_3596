namespace BO
{
    public class CustomerToList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int ParcelsDelivered { get; set; }
        public int ParcelsSent { get; set; }
        public int ParcelsReceived { get; set; }
        public int ParcelsInProccesToHim { get; set; }

        public override string ToString()
        {
            return
                $"ID:                            {Id}\n" +
                $"Name:                          {Name}\n" +
                $"Phone:                         {Phone}\n" +
                $"Parcels delivered:             {ParcelsDelivered}\n" +
                $"Parcels sent:                  {ParcelsSent}\n" +
                $"Parcels received:              {ParcelsReceived}\n" +
                $"Parcels in the way to him:     {ParcelsInProccesToHim}";
        }
    }
}
