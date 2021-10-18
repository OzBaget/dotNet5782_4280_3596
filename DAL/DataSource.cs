using System;

namespace DalObject
{
    class DataSource
    {
        internal static IDAL.DO.Drone[] Drones = new IDAL.DO.Drone[10];
        internal static IDAL.DO.Station[] BaseStations = new IDAL.DO.Station[5];
        internal static IDAL.DO.Customer[] Customers = new IDAL.DO.Customer[100];
        internal static IDAL.DO.Parcel[] Parcels = new IDAL.DO.Parcel[1000];
        internal static Config config = new Config();
        public static void Initialize()
        {
            Random r = new Random();

            //Initialize BaseStations
            string[] baseNames = { "Jerusalem", "Tel-Aviv" };
            double[] baseLngs = { 35.212140, 34.8 };
            double[] baseLats = { 31.765975, 32.083333 };

            for (int i = 0; i < 2; i++)
            {
                BaseStations[i] = new IDAL.DO.Station();
                BaseStations[i].Id = r.Next();
                BaseStations[i].Name = baseNames[i];
                BaseStations[i].Lng = baseLngs[i];
                BaseStations[i].Lat = baseLats[i];
                BaseStations[i].FreeChargeSlots = r.Next(5);
            }

            //Initialize Drones
            for (int i = 0; i < 5; i++)
            {
                Drones[i].Id = r.Next();
                Drones[i].Model = "MK"+r.Next(1,4).ToString();
                Drones[i].MaxWeight = (IDAL.DO.WeightCategories)r.Next(0, 3);
                Drones[i].Status = (IDAL.DO.DroneStatuses)r.Next(0, 3);
                Drones[i].Battery = r.Next(1, 101);
            }

            //Initialize Customers
            string[] names = { "Oz", "Ohad", "Abraham", "Yizeck", "Jecobe", "Joshf", "Shimon", "Reuven", "Moshe", "David" };

            for (int i = 0; i < 10; i++)
            {
                Customers[i].Id = r.Next();
                Customers[i].Name = names[i];
                Customers[i].Phone = "+972" + r.Next(100000000, 999999999).ToString();//ten digits phone number
                Customers[i].Lng = (double)r.Next(345000, 355000) / 10000;//v  
                Customers[i].Lat = (double)r.Next(315000, 330000) / 10000;//somewhere in Israel
            }


            //Initialize Parcels

            //array of droneId of every Parcel; To make sure that there won't be collisions.
            int[] dronesIds = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int j = 0;
            foreach (IDAL.DO.Drone drone in Drones)
            {
                if (drone.Status == IDAL.DO.DroneStatuses.Delivery)// we need to make them really under delivery.
                {
                    dronesIds[j] = drone.Id;
                }
                j++;
            }

            for (int i = 0; i < 10; i++)
            {
                Parcels[i].Id = i;
                Parcels[i].SenderId = Customers[r.Next(0, 10)].Id;
                Parcels[i].TargetId = Customers[r.Next(0, 10)].Id; //TODO: make sure that both sender and target are diffrent..
                Parcels[i].DroneId = dronesIds[i];
                Parcels[i].Requsted = randomDateBetween(DateTime.Now.AddDays(-2), DateTime.Now);//date&time in the last two days
                if (Parcels[i].DroneId != 0)//parcel[i] is under delivery
                {
                    Parcels[i].Weight = Array.Find(Drones,
                        drone => drone.Id == Parcels[i].DroneId).MaxWeight;
                    Parcels[i].Scheduled = randomDateBetween(Parcels[i].Requsted, DateTime.Now);
                    Parcels[i].PickedUp = randomDateBetween(Parcels[i].Scheduled, DateTime.Now);
                }
                else
                    Parcels[i].Weight = (IDAL.DO.WeightCategories)r.Next(0, 3);
                    //Scheduled and PickedUp aren't assigend because they didnt happend..

                Parcels[i].Priority = (IDAL.DO.Priorities)r.Next(0, 3);
            }
            config.parcelId = 10;//Because we have 10 drones.

        }
        private static DateTime randomDateBetween(DateTime start, DateTime end)
        {
            Random r = new Random();
            return start.AddMinutes(r.Next((int)(end - start).TotalMinutes));
        }
    }



    class Config
    {
        internal static int DronesIndex = 0;
        internal static int StationsIndex = 0;
        internal static int CustomersIndex = 0;
        internal static int ParcelIndex = 0;

        public int parcelId { get; set; }

    }
}
