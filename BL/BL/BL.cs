using BlApi;
using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BL
{
    internal sealed partial class BL : IBL
    {
        private static List<DroneToList> Drones = new List<DroneToList>();
        private double powerUseEmpty;
        private double powerUseLight;
        private double powerUseMiddle;
        private double powerUseHeavy;
        private double chargingRate;
        internal IDal DalObject = DalFactory.GetDal();
        private static readonly BL instance = new BL();
        public static BL Instance { get => instance; }

        private BL()
        {
            //precnt to meter
            powerUseEmpty = DalObject.GetPowerUse()[0];
            powerUseLight = DalObject.GetPowerUse()[1];
            powerUseMiddle = DalObject.GetPowerUse()[2];
            powerUseHeavy = DalObject.GetPowerUse()[3];

            chargingRate = DalObject.GetChargingRate();//precent to mintues


            foreach (DO.Drone drone in DalObject.GetAllDrones())
            {
                DroneToList myDrone = new();
                myDrone.Id = drone.Id;
                myDrone.Model = drone.Model;
                myDrone.MaxWeight = (WeightCategories)drone.MaxWeight;

                foreach (DO.Parcel parcel in DalObject.GetAllParcels())
                {
                    if (parcel.Delivered == null && parcel.Scheduled != null && parcel.DroneId == myDrone.Id) //parcel havn't deliverd but scheduled
                    {
                        myDrone.ParcelId = parcel.Id;
                        myDrone.Status = DroneStatus.Delivery;
                        if (parcel.PickedUp == null)
                        {
                            myDrone.CurrentLocation = getClosestStation(GetCustomer(parcel.SenderId).Location).Location;
                        }
                        else
                        {
                            myDrone.CurrentLocation = GetCustomer(parcel.SenderId).Location;
                        }

                        double minBattery = batteryNeedForTrip(GetCustomer(parcel.TargetId).Location, myDrone.CurrentLocation, (WeightCategories)parcel.Weight) +//battry needed for delivering the parcel
                            batteryNeedForTrip(getClosestStation(GetCustomer(parcel.TargetId).Location).Location, myDrone.CurrentLocation); //battry for arriving closest station to customer.

                        myDrone.Battery = minBattery < 100 ? new Random().Next((int)minBattery, 101) : 100;

                    }
                }
                if (myDrone.Status != DroneStatus.Delivery)
                {
                    if (DalObject.GetAllDroneCharge().Any(charger => charger.Droneld == myDrone.Id))
                    {
                        DO.DroneCharge dc = DalObject.GetAllDroneCharge().First(charger => charger.Droneld == myDrone.Id);
                        myDrone.Status = DroneStatus.UnderMaintenance;
                        myDrone.Battery = new Random().Next(0, 21);
                        myDrone.CurrentLocation = GetStation(dc.Stationld).Location;
                    }
                    else
                    {
                        myDrone.Status = (DroneStatus)new Random().Next(2);

                        if (myDrone.Status == DroneStatus.UnderMaintenance)
                        {
                            DO.Station chargerStation = DalObject.GetAllStations().ElementAt(new Random().Next(GetAllStations().Count()));
                            myDrone.CurrentLocation = new();
                            myDrone.CurrentLocation.Latitude = chargerStation.Lat;
                            myDrone.CurrentLocation.Longitude = chargerStation.Lng;
                            myDrone.Battery = new Random().Next(0, 21);
                            DalObject.DroneToStation(chargerStation.Id, myDrone.Id);
                        }
                        if (myDrone.Status == DroneStatus.Available)
                        {
                            int index = new Random().Next(GetAllCustomers().Count(customer => customer.ParcelsReceived > 0));
                            myDrone.CurrentLocation = GetCustomer(GetAllCustomers().Where(customer => customer.ParcelsReceived > 0).ElementAt(index).Id).Location;

                            double minBattry = batteryNeedForTrip(myDrone.CurrentLocation, getClosestStation(myDrone.CurrentLocation).Location);
                            if (minBattry > 100)
                            {
                                minBattry = 100;
                            }

                            myDrone.Battery = new Random().Next((int)minBattry, 101);
                        }
                    }
                }

                Drones.Add(myDrone);
            }
        }


        /// <summary>
        /// get the closest station of spasific location
        /// </summary>
        /// <param name="loc">the location</param>
        /// <returns>BaseStation</returns>
        private BaseStation getClosestStation(Location loc)
        {
            return GetStation(GetAllStations().OrderBy(station => calculateDist(GetStation(station.Id).Location, loc)).First().Id);
        }

        /// <summary>
        /// calculate Haversine dist between two coords
        /// </summary>
        /// <param name="lat1">latituse of point 1</param>
        /// <param name="lng1">longtude of point 1</param>
        /// <param name="lat2">latituse of point 2</param>
        /// <param name="lng2">longtude of point 2</param>
        /// <returns>the distance bitween the two coords in meters</returns>
        private double calculateDist(Location loc1, Location loc2)
        {
            const double Radios = 6371000;//meters
            //deg to radians
            double lat1 = loc1.Latitude * Math.PI / 180;
            double lat2 = loc2.Latitude * Math.PI / 180;
            double lng1 = loc1.Longitude * Math.PI / 180;
            double lng2 = loc2.Longitude * Math.PI / 180;

            //Haversine formula
            double a = Math.Pow(Math.Sin((lat2 - lat1) / 2), 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Pow(Math.Sin((lng2 - lng1) / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return Radios * c;
        }
    }
}
