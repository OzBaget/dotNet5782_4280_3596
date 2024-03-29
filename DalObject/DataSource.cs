﻿using DO;
using System;
using System.Collections.Generic;

namespace Dal
{
    internal class DataSource
    {
        internal static List<Drone> Drones = new List<Drone>();
        internal static List<Station> BaseStations = new List<Station>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal static List<DroneCharge> Charges = new List<DroneCharge>();

        internal class Config
        {
            //Precent To Meter
            public static double Free { get; } = 0.0001;
            public static double LightPacket { get; } = 0.0002;
            public static double MediumPacket { get; } = 0.0004;
            public static double HeavyPacket { get; } = 0.0008;
            public static double ChargingRate { get; } = 5; //precent to second
            public static int ParcelId { get; set; }
        }
        /// <summary>
        /// Initialize all lists with random data
        /// </summary>
        public static void Initialize()
        {
            Random r = new Random();

            #region Initialize BaseStations
            string[] baseNames = { "Jerusalem", "Tel-Aviv" };
            double[] baseLngs = { 35.212140, 34.8 };
            double[] baseLats = { 31.765975, 32.083333 };

            for (int i = 0; i < 2; i++)
            {
                Station myStation = new();
                myStation.Id = r.Next();
                myStation.Name = baseNames[i];
                myStation.Lat = baseLats[i];
                myStation.Lng = baseLngs[i];
                myStation.FreeChargeSlots = r.Next(5, 15);
                myStation.IsActived = true;
                BaseStations.Add(myStation);
            }
            #endregion

            #region Initialize Drones
            for (int i = 0; i < 5; i++)
            {
                Drone myDrone = new Drone();
                myDrone.Id = r.Next();
                myDrone.Model = "MK" + r.Next(1, 4).ToString();
                myDrone.MaxWeight = (WeightCategories)r.Next(0, 3);
                myDrone.IsActived = true;
                Drones.Add(myDrone);
            }
            #endregion

            #region Initialize Customers
            string[] names = { "Oz", "Ohad", "Abraham", "Yizeck", "Jecobe", "Joshf", "Shimon", "Reuven", "Moshe", "David", "Shmuel", "Eyal", "Levi", "Dan", "Gad", "Judah", "Asher", "Joshef", "Naphtali", "Daniel" };

            for (int i = 0; i < 20; i++)
            {
                Customer myCustomer = new Customer();
                myCustomer.Id = r.Next();
                myCustomer.Name = names[i];
                myCustomer.Phone = "+972" + r.Next(100000000, 999999999).ToString();//ten digits phone number
                myCustomer.Lng = (double)r.Next(345000, 355000) / 10000;//v         
                myCustomer.Lat = (double)r.Next(315000, 330000) / 10000;//somewhere in Israel
                myCustomer.IsActived = true;
                Customers.Add(myCustomer);
            }
            #endregion

            #region Initialize Parcels
            int parcelCount = 100;
            //array of droneId of every Parcel; To make sure that there won't be collisions.
            int[] dronesIds = new int[parcelCount];
            for (int i = 0; i < dronesIds.Length; i++)

                dronesIds[i] = 0;

            int j = 0;
            foreach (Drone drone in Drones)
            {
                if (r.Next(2) == 0)
                    dronesIds[j] = drone.Id;
                j++;
            }

            for (int i = 0; i < parcelCount; i++)
            {
                Parcel myParcel = new Parcel();
                myParcel.Id = ++Config.ParcelId;
                myParcel.IsActived = true;
                while (myParcel.TargetId == myParcel.SenderId)
                {
                    myParcel.SenderId = Customers[r.Next(0, 10)].Id;
                    myParcel.TargetId = Customers[r.Next(0, 10)].Id;
                }
                myParcel.DroneId = dronesIds[i];
                myParcel.Requsted = randomDateBetween(DateTime.Now.AddDays(-2), DateTime.Now);//date&time in the last two days
                if (myParcel.DroneId != 0)//myParcel is under delivery
                {
                    myParcel.Weight = Drones.Find(drone => drone.Id == myParcel.DroneId).MaxWeight;
                    myParcel.Scheduled = randomDateBetween((DateTime)myParcel.Requsted, DateTime.Now);
                    myParcel.PickedUp = randomDateBetween((DateTime)myParcel.Scheduled, DateTime.Now);
                }
                else
                {
                    myParcel.Weight = (WeightCategories)r.Next(0, 3);
                    if (r.Next(0, 2) == 0)
                    {
                        myParcel.Scheduled = randomDateBetween((DateTime)myParcel.Requsted, DateTime.Now);
                        myParcel.PickedUp = randomDateBetween((DateTime)myParcel.Scheduled, DateTime.Now);
                        myParcel.Delivered = randomDateBetween((DateTime)myParcel.PickedUp, DateTime.Now);
                    }

                }
                //Scheduled and PickedUp aren't assigend because they didnt happend..

                myParcel.Priority = (Priorities)r.Next(0, 3);
                Parcels.Add(myParcel);
            }
            #endregion
        }

        /// <summary>
        /// retrun random date&time between two dates
        /// </summary>
        /// <param name="start">the min date&time</param>
        /// <param name="end">the max date&time</param>
        /// <returns>date&time between the two dates</returns>
        private static DateTime randomDateBetween(DateTime start, DateTime end)
        {
            Random r = new Random();
            return start.AddMinutes(r.Next((int)(end - start).TotalMinutes));
        }
    }
}
