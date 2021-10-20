using System;
using System.Collections.Generic;
using IDAL.DO;

namespace DalObject
{
    class DataSource
    {
        internal static List<Drone>Drones = new List<Drone>();
        internal static List<Station> BaseStations = new List<Station>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();

        internal class Config
        {
            int id = 0;
            public static int parcelId() { return ++id;}
        }

        public static void Initialize()
        {
            Random r = new Random();

            //Initialize BaseStations
            string[] baseNames = { "Jerusalem", "Tel-Aviv" };
            double[] baseLngs = { 35.212140, 34.8 };
            double[] baseLats = { 31.765975, 32.083333 };

            for (int i = 0; i < 2; i++)
            {
                Station myStation = new Station(baseNames[i], baseLats[i], baseLngs[i], r.Next(5));
                BaseStations.Add(myStation);
            }

            //Initialize Drones
            for (int i = 0; i < 5; i++)
            {
                Drone myDrone = new Drone();
                myDrone.Id = r.Next();
                myDrone.Model = "MK"+r.Next(1,4).ToString();
                myDrone.MaxWeight = (WeightCategories)r.Next(0, 3);
                myDrone.Status = (DroneStatuses)r.Next(0, 3);
                myDrone.Battery = r.Next(1, 101);
                Drones.Add(myDrone);
            }

            //Initialize Customers
            string[] names = { "Oz", "Ohad", "Abraham", "Yizeck", "Jecobe", "Joshf", "Shimon", "Reuven", "Moshe", "David" };

            for (int i = 0; i < 10; i++)
            {
                Customer myCustomer = new Customer();
                myCustomer.Id = r.Next();
                myCustomer.Name = names[i];
                myCustomer.Phone = "+972" + r.Next(100000000, 999999999).ToString();//ten digits phone number
                myCustomer.Lng = (double)r.Next(345000, 355000) / 10000;//v  
                myCustomer.Lat = (double)r.Next(315000, 330000) / 10000;//somewhere in Israel
                Customers.Add(myCustomer);
            }


            //Initialize Parcels

            //array of droneId of every Parcel; To make sure that there won't be collisions.
            int[] dronesIds = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int j = 0;
            foreach (Drone drone in Drones)
            {
                if (drone.Status == DroneStatuses.Delivery)// we need to make them really under delivery.
                {
                    dronesIds[j] = drone.Id;
                }
                j++;
            }

            for (int i = 0; i < 10; i++)
            {
                Parcel myParcel = new Parcel();
                myParcel.Id = Config.parcelId();
                myParcel.SenderId = Customers[r.Next(0, 10)].Id;
                myParcel.TargetId = Customers[r.Next(0, 10)].Id; //TODO: make sure that both sender and target are diffrent..
                myParcel.DroneId = dronesIds[i];
                myParcel.Requsted = randomDateBetween(DateTime.Now.AddDays(-2), DateTime.Now);//date&time in the last two days
                if (myParcel.DroneId != 0)//myParcel is under delivery
                {
                    myParcel.Weight = Drones.Find(drone => drone.Id == myParcel.DroneId).MaxWeight;
                    myParcel.Scheduled = randomDateBetween(myParcel.Requsted, DateTime.Now);
                    myParcel.PickedUp = randomDateBetween(myParcel.Scheduled, DateTime.Now);
                }
                else
                    myParcel.Weight = (WeightCategories)r.Next(0, 3);
                //Scheduled and PickedUp aren't assigend because they didnt happend..

                myParcel.Priority = (Priorities)r.Next(0, 3);

                Parcels.Add(myParcel);
            }
            //TODO: update config.
            // do not listen to anything that Oz says. In generaly, leave the degree and go recruit to the IDF 

        }
        private static DateTime randomDateBetween(DateTime start, DateTime end)
        {
            Random r = new Random();
            return start.AddMinutes(r.Next((int)(end - start).TotalMinutes));
        }
    }



    
}
