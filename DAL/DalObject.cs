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
            BaseStations[0] = new IDAL.DO.Station();
            BaseStations[0].Id = r.Next();
            BaseStations[0].Name = "The First Station";
            BaseStations[0].Lng = 34.21;
            BaseStations[0].Lat = 33.56;

            BaseStations[1] = new IDAL.DO.Station();
            BaseStations[1].Id = r.Next();
            BaseStations[1].Name = "The Second Station";
            BaseStations[1].Lng = 28.23;
            BaseStations[1].Lat = -12.54;

            Drones[0].Id = r.Next();
            Drones[0].Model = "MK1";
            Drones[0].MaxWeight = IDAL.DO.WeightCategories.Light;
            Drones[0].Status = IDAL.DO.DroneStatuses.Available;

            Drones[1].Id = r.Next();
            Drones[1].Model = "MK1";
            Drones[1].MaxWeight = IDAL.DO.WeightCategories.Light;
            Drones[1].Status = IDAL.DO.DroneStatuses.Delivery;

            Drones[2].Id = r.Next();
            Drones[2].Model = "MK2";
            Drones[2].MaxWeight = IDAL.DO.WeightCategories.Middle;
            Drones[2].Status = IDAL.DO.DroneStatuses.Available;

            Drones[3].Id = r.Next();
            Drones[3].Model = "MK2";
            Drones[3].MaxWeight = IDAL.DO.WeightCategories.Middle;
            Drones[3].Status = IDAL.DO.DroneStatuses.UnderMaintenance;

            Drones[4].Id = r.Next();
            Drones[4].Model = "MK3";
            Drones[4].MaxWeight = IDAL.DO.WeightCategories.Heavy;
            Drones[4].Status = IDAL.DO.DroneStatuses.Available;

            Customers[0].Id = r.Next();
            Customers[0].Name = "Ohad";
            Customers[0].Phone = "+972585161171";
            Customers[0].lng = 32.45;
            Customers[0].lat = 34.65;
            //TODO: Add more 4 customers
            //TODO: Add 5 parcels
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
