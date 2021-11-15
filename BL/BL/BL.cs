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
                    if (parcel.Delivered != DateTime.MinValue && parcel.DroneId == myDrone.Id)//parcel havn't deliverd but schudled
                    {
                        myDrone.Status = DroneStatus.Delivery;
                        if (parcel.PickedUp== DateTime.MinValue)
                        {
                            myDrone.CurrentLocation = GetClosestStation(GetCustomer(parcel.SenderId).Location).Location;
                        }
                        else
                        {
                            myDrone.CurrentLocation = GetCustomer(parcel.SenderId).Location;
                        }
                        int minBattery = 0;
                        double distancToCover = CalculateDist(myDrone.CurrentLocation, GetCustomer(parcel.TargetId).Location)+
                            CalculateDist(GetCustomer(parcel.TargetId).Location,GetClosestStation(GetCustomer(parcel.TargetId).Location).Location);
                        switch (parcel.Weight)
                        {
                            case IDAL.DO.WeightCategories.Light:
                                minBattery = (int)(distancToCover * powerUseLight);
                                break;
                            case IDAL.DO.WeightCategories.Middle:
                                minBattery = (int)(distancToCover * powerUseMiddle);
                                break;
                            case IDAL.DO.WeightCategories.Heavy:
                                minBattery = (int)(distancToCover * powerUseHeavy);
                                break;
                            default:
                                minBattery = 0;
                                break;
                        }
                        myDrone.Battery = new Random().Next(minBattery, 101);
                        break;
                    }
                    else
                    {
                        myDrone.Status = (DroneStatus)new Random().Next(0, 2);
                        if (myDrone.Status==DroneStatus.UnderMaintenance)
                        {
                            myDrone.CurrentLocation = GetAllStations().ElementAt(new Random().Next(GetAllStations().Count())).Location;
                            myDrone.Battery = new Random().Next(0, 21);
                        }
                        if (myDrone.Status==DroneStatus.Available)
                        {
                            myDrone.CurrentLocation=GetAllCustomers().Where(customer => customer.Reciver.Count() > 0).ElementAt(new Random().Next(GetAllCustomers().Where(customer => customer.Reciver.Count() > 0).Count())).Location;
                            myDrone.Battery = new Random().Next((int)(CalculateDist(myDrone.CurrentLocation, GetClosestStation(myDrone.CurrentLocation).Location)*powerUseEmpty), 101);
                        }
                    }
                }
                Drones.Add(myDrone);
            }
        }

        

        public void AddCustomer(int id, string name, string phone, double lat, double lng)
        {
            throw new NotImplementedException();
        }

        public void AddDrone(int id, string model, int maxWeightInt)
        {
            throw new NotImplementedException();
        }

        public int AddParcel(int senderId, int targetId, int weightInt, int priorityInt)
        {
            throw new NotImplementedException();
        }

        public void DroneToBase(int droneId)
        {
            throw new NotImplementedException();
        }

        public void FreeDrone(int droneId,int drineTime)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAllDrones()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetAllParcels()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BaseStation> GetAllStations()
        {
            
            throw new NotImplementedException();
        }

        public BaseStation GetBaseStation(int stationId)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public Drone GetDrone(int droneId)
        {
            throw new NotImplementedException();
        }

        public Parcel GetParcerl(int parcelId)
        {
            throw new NotImplementedException();
        }

        public double[] GetPowerUse()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BaseStation> GetStationsWithFreeSlots()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetUnassignedParcels()
        {
            throw new NotImplementedException();
        }

        public void linkParcel(int droneId)
        {
            throw new NotImplementedException();
        }

        public void ParcelToCustomer(int parcelId)
        {
            throw new NotImplementedException();
        }

        public void PickParcel(int parcelId)
        {
            throw new NotImplementedException();
        }

        private BaseStation GetClosestStation(Location loc)
        {
            return GetAllStations().OrderBy(station => CalculateDist(station.Location, loc)).First();
        }

        /// <summary>
        /// calculate Haversine dist between two coords
        /// </summary>
        /// <param name="lat1">latituse of point 1</param>
        /// <param name="lng1">longtude of point 1</param>
        /// <param name="lat2">latituse of point 2</param>
        /// <param name="lng2">longtude of point 2</param>
        /// <returns>the distance bitween the two coords in meters</returns>
        private double CalculateDist(Location loc1, Location loc2)
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

        public void AddBase(string name, double lat, double lng, int chargSlots)
        {
            throw new NotImplementedException();
        }

        public void AddDrone(string model, int maxWeightInt)
        {
            throw new NotImplementedException();
        }

        public void AddCustomer(string name, string phone, double lat, double lng)
        {
            throw new NotImplementedException();
        }

        public void UpdateDroneName(int id, string model)
        {
            throw new NotImplementedException();
        }

        
    }

    
}
