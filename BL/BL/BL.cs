using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        IDAL.IDal DalObject;
        static List<DroneToList> Drones = new List<DroneToList>();
        double powerUseEmpty;
        double powerUseLight;
        double powerUseMiddle;
        double powerUseHeavy;
        double chargingRate;

        public BL()
        {
            DalObject = new DalObject.DalObject();
            //precnt to meter
            powerUseEmpty = DalObject.GetPowerUse()[0];
            powerUseLight = DalObject.GetPowerUse()[1];
            powerUseMiddle = DalObject.GetPowerUse()[2];
            powerUseHeavy = DalObject.GetPowerUse()[3];

            chargingRate = DalObject.GetChargingRate();//precent to hour


            foreach (IDAL.DO.Drone drone in DalObject.GetAllDrones())
            {
                DroneToList myDrone = new();
                myDrone.Id = drone.Id;
                myDrone.Model = drone.Model;
                myDrone.MaxWeight =(WeightCategories) drone.MaxWeight;

                foreach (IDAL.DO.Parcel parcel in DalObject.GetAllParcels())
                {
                    if (parcel.Delivered == DateTime.MinValue && parcel.Scheduled != DateTime.MinValue &&  parcel.DroneId == myDrone.Id) //parcel havn't deliverd but scheduled
                    {
                        myDrone.Status = DroneStatus.Delivery;
                        if (parcel.PickedUp== DateTime.MinValue)
                        {
                            myDrone.CurrentLocation = getClosestStation(GetCustomer(parcel.SenderId).Location).Location;
                        }
                        else
                        {
                            myDrone.CurrentLocation = GetCustomer(parcel.SenderId).Location;
                        }

                        myDrone.PacrelId = parcel.Id;
                        int minBattery = batteryNeedForDest(GetCustomer(parcel.TargetId).Location, myDrone.CurrentLocation, false, (WeightCategories)parcel.Weight)+
                            batteryNeedForDest(getClosestStation(GetCustomer(parcel.TargetId).Location).Location, myDrone.CurrentLocation);
                        
                        if (minBattery < 100)
                        {
                            myDrone.Battery = new Random().Next(minBattery, 101);
                        }
                        else
                            myDrone.Battery = 100;
                        break;
                    }
                }
                if (myDrone.Status != DroneStatus.Delivery) 
                {
                    myDrone.Status = (DroneStatus)new Random().Next(0, 2);
                    if (myDrone.Status == DroneStatus.UnderMaintenance)
                    {
                        BaseStation chargerStation = GetStation(GetAllStations().ElementAt(new Random().Next(GetAllStations().Count())).Id);
                        myDrone.CurrentLocation = chargerStation.Location;
                        myDrone.Battery = new Random().Next(0, 21);
                        DalObject.DroneToStation(chargerStation.Id, myDrone.Id);
                    }
                    if (myDrone.Status == DroneStatus.Available)
                    {
                        int index = new Random().Next(GetAllCustomers().Where(customer => customer.ParcelsReceived > 0).Count());
                        myDrone.CurrentLocation = GetCustomer(GetAllCustomers().ElementAt(index).Id).Location;

                        int minBattry = batteryNeedForDest(myDrone.CurrentLocation, getClosestStation(myDrone.CurrentLocation).Location);
                        if (minBattry>100)
                        {
                            minBattry = 100;
                        }
                        
                        myDrone.Battery = new Random().Next(minBattry, 101);
                    }
                }
                Drones.Add(myDrone);
            }
        }

        

        public void ParcelToCustomer(int droneID)
        {
            throw new NotImplementedException();
        }




        public double[] GetPowerUse()
        {
            throw new NotImplementedException();
        }

        

        

        

        

        public void PickParcel(int parcelId)
        {   
            throw new NotImplementedException();
        }

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
