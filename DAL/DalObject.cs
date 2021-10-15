using System;

namespace DalObject
{
    class DataSource
    {
        internal static IDAL.DO.Drone[] Drones = new IDAL.DO.Drone[10];
        internal static IDAL.DO.Station[] BaseStations = new IDAL.DO.Station[5];
        internal static IDAL.DO.Customer[] Customers = new IDAL.DO.Customer[100];
        internal static IDAL.DO.Parcel[] parcels = new IDAL.DO.Parcel[1000];
        internal Config config = new Config();
        static void Initialize()
        {
            Random r = new Random();

            //Initialize BaseStations
            int[] baseIds = { r.Next(), r.Next() };
            string[] baseNames = { "Jerusalem", "Tel-Aviv" };
            double[] baseLngs = { 31.765975, 32.083333 };
            double[] baseLats = { 35.212140, 34.8 };

            for (int i = 0; i < 2; i++)
            {
                BaseStations[i] = new IDAL.DO.Station();
                BaseStations[i].Id = baseIds[i];
                BaseStations[i].Name=baseNames[i];
                BaseStations[i].Lng = baseLngs[i];
                BaseStations[i].Lat = baseLats[i];
            }

            //Initialize Drone
            int[] droneIds = { r.Next(), r.Next(), r.Next(), r.Next(), r.Next() };
            string[] models = { "MK1", "MK1", "MK2", "MK2", "MK3" };
            IDAL.DO.WeightCategories[] maxWeights = { 
                IDAL.DO.WeightCategories.Light,
                IDAL.DO.WeightCategories.Light,
                IDAL.DO.WeightCategories.Middle,
                IDAL.DO.WeightCategories.Middle,
                IDAL.DO.WeightCategories.Heavy
            };

            IDAL.DO.DroneStatuses[] statuss = {
                IDAL.DO.DroneStatuses.Delivery,
                IDAL.DO.DroneStatuses.Available,
                IDAL.DO.DroneStatuses.Delivery,
                IDAL.DO.DroneStatuses.Available,
                IDAL.DO.DroneStatuses.Delivery
            };

            for (int i = 0; i < 5; i++)
            {
                Drones[i].Id = droneIds[i];
                Drones[i].Model = models[i];
                Drones[i].MaxWeight = maxWeights[i];
                Drones[i].Status = statuss[i];
                Drones[i].Battery = r.Next(0, 100);
            }

            //Initialize Customer
            int[] customersIds = { r.Next(), r.Next(), r.Next(), r.Next(), r.Next(), r.Next(), r.Next(), r.Next(), r.Next(), r.Next() };
            string[] names = { "Ohad", "Oz", "Joshf", "Yizeck", "Abraham", "Haim", "Shimon", "Reuven", "Jecobe", "David" };

            for (int i = 0; i < 10; i++)
            {
                Customers[i].Id = customersIds[i];
                Customers[i].Name = names[i];
                Customers[i].Phone = "+972" + r.Next(100000000, 999999999).ToString();//ten digit phone number
                Customers[i].lng = (double)r.Next(3200, 3400) / 100;
                Customers[i].lat = (double)r.Next(3100, 3500) / 100;//somewhere in Israel
            }
            //TODO: Add 10 parcels



        }
    }

    class Config
    {
        internal static int DronesIndex = 0;
        internal static int StationsIndex = 0;
        internal static int CustomersIndex = 0;
        internal static int ParcelIndex = 0;

        int ParcelId;

    }
}
