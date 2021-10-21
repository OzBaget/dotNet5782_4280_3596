using System;
using IDAL.DO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DalObject

{
    public class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }
        //returns the base station which has thiis id
        public Station GetBaseStation(int stationId) 
        {
            return DataSource.BaseStations.Find(station => station.Id == stationId);
        }
        //returns the drone which has thiis id
        public Drone GetDrone(int droneId) 
        {
            return DataSource.Drones.Find(drone => drone.Id == droneId);
        }
        //returns the customer which has thiis id
        public Customer GetCustomer(int customerId)
        {
            return DataSource.Customers.Find(customer=> customer.Id == customerId);
        }
        //returns the parcel which has thiis id
        public Parcel GetParcerl(int parcelId) 
        {
            return DataSource.Parcels.Find(parcel => parcel.Id == parcelId);
        }
        //add base to the list
        public void AddBase(string name, double lat, double lng, int chargSlots)
        {
            DataSource.BaseStations.Add(new Station(name, lat, lng, chargSlots));
        }
        //add drone to the list
        public void AddDrone(string model, int maxWeightInt)
        {
            DataSource.Drones.Add(new Drone(model, (WeightCategories)maxWeightInt));
        }
        //add customer to the list
        public void AddCustomer(string name, string phone, double lat, double lng)
        {
            DataSource.Customers.Add(new Customer(name, phone, lat, lng));
        }
        //add parcel to the list
        public void AddParcel(int senderId, int targetId, int weightInt, int priorityInt)
        {
            DataSource.Parcels.Add(new Parcel(senderId, targetId, (WeightCategories)weightInt, (Priorities)priorityInt));
        }
        //link the drone to the parcel
        public void linkParcel(int parcelId, int droneId)
        {
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = DataSource.Parcels.IndexOf(parcelTmp);
            parcelTmp.DroneId = droneId;
            parcelTmp.Scheduled = DateTime.Now;
            DataSource.Parcels[index] = parcelTmp;

            Drone droneTmp = GetDrone(droneId);
            index = DataSource.Drones.IndexOf(droneTmp);
            droneTmp.Status = DroneStatuses.Delivery;
            DataSource.Drones[index] = droneTmp;
        }
        //set the time that the parcel was picked by the drone
        public void PickParcel(int parcelId)
        {
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = DataSource.Parcels.IndexOf(parcelTmp);
            parcelTmp.PickedUp = DateTime.Now;
            DataSource.Parcels[index] = parcelTmp;
        }
        //set the time that the parcel was picked by the customer
        public void ParcelToCustomer(int parcelId)
        {
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = DataSource.Parcels.IndexOf(parcelTmp);
            parcelTmp.Delivered = DateTime.Now;
            int droneId = parcelTmp.DroneId;
            DataSource.Parcels[index] = parcelTmp;

            Drone droneTmp = GetDrone(droneId);
            index = DataSource.Drones.IndexOf(droneTmp);
            droneTmp.Status = DroneStatuses.Available;
            DataSource.Drones[index] = droneTmp;
        }
        //Charge the drone
        public void DroneToBase(int stationId, int droneId)
        {
            DataSource.Charges.Add(new DroneCharge(droneId, stationId));
            Drone droneTmp = GetDrone(droneId);
            int index = DataSource.Drones.IndexOf(droneTmp);
            droneTmp.Battery= 100;
            DataSource.Drones[index] = droneTmp;
        }
        //Free drone frome charging
        public void FreeDrone(int droneId)
        {
            DataSource.Charges.Remove(DataSource.Charges.Find(charger => charger.Droneld == droneId));
        }
        //Return array of base satations
        public Station[] GetAllStations()
        {
            return DataSource.BaseStations.ToArray();
        }
        //Return array of drones
        public Drone[] GetAllDrones()
        {
            return DataSource.Drones.ToArray();
        }
        //Return array of customers
        public Customer[] GetAllCustomers()
        {
            return DataSource.Customers.ToArray();
        }
        //Return array of parcels
        public Parcel[] GetAllParcels()
        {
            return DataSource.Parcels.ToArray();
        }
        //Return array of unsigned parcels
        public Parcel[] GetUnassignedParcels()
        {
            return DataSource.Parcels.FindAll(parcel => parcel.DroneId == 0).ToArray();
        }
        //return all the base stations that has free cahrge slots
        public Station[] GetStationsWithFreeSlots()
        {
            return DataSource.BaseStations.FindAll(station => station.FreeChargeSlots != 0).ToArray();
        }
    }
}
