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
        /// <summary>
        /// returns base station by ID
        /// </summary>
        /// <param name="stationId"> the base station ID</param>
        /// <returns>Return a Station object of the requsted ID (by value)</returns>
        public Station GetBaseStation(int stationId) 
        {
            return DataSource.BaseStations.Find(station => station.Id == stationId);
        }
        /// <summary>
        /// returns drone by ID
        /// </summary>
        /// <param name="droneId"> the drone ID</param>
        /// <returns>Drone object of the requsted ID (by value)</returns>
        public Drone GetDrone(int droneId) 
        {
            return DataSource.Drones.Find(drone => drone.Id == droneId);
        }
        /// <summary>
        /// returns customer by ID
        /// </summary>
        /// <param name="customerId"> the customer ID</param>
        /// <returns>Customer object of the requsted ID (by value)</returns>
        public Customer GetCustomer(int customerId)
        {
            return DataSource.Customers.Find(customer=> customer.Id == customerId);
        }
        /// <summary>
        /// returns parcel by ID
        /// </summary>
        /// <param name="parcelId"> the parcel ID</param>
        /// <returns>Parcel object of the requsted ID (by value)</returns>
        public Parcel GetParcerl(int parcelId) 
        {
            return DataSource.Parcels.Find(parcel => parcel.Id == parcelId);
        }
        /// <summary>
        /// Add base station to the BaseStations list in DataSource
        /// </summary>
        /// <param name="name">the name of the station</param>
        /// <param name="lat">the latitude of the station</param>
        /// <param name="lng">the longitude of the station</param>
        /// <param name="chargSlots">hw many charge slosts are in the station</param>
        public void AddBase(string name, double lat, double lng, int chargSlots)
        {
            DataSource.BaseStations.Add(new Station(name, lat, lng, chargSlots));
        }
        /// <summary>
        /// Add drone to Drones list in DataSource
        /// </summary>
        /// <param name="model">the modle of the drone</param>
        /// <param name="maxWeightInt">max weight of the drone (0/1/2)</param>
        public void AddDrone(string model, int maxWeightInt)
        {
            DataSource.Drones.Add(new Drone(model, (WeightCategories)maxWeightInt));
        }

        /// <summary>
        /// Add customer to Customers list in DataSource
        /// </summary>
        /// <param name="name">the name of the customer</param>
        /// <param name="phone">the phone of the customer</param>
        /// <param name="lat">the latitude of the customer</param>
        /// <param name="lng">the longitude of the customer</param>
        public void AddCustomer(string name, string phone, double lat, double lng)
        {
            DataSource.Customers.Add(new Customer(name, phone, lat, lng));
        }
        /// <summary>
        /// Add parcel to Parcels list in DataSource
        /// </summary>
        /// <param name="senderId">sender customer ID</param>
        /// <param name="targetId">target customer ID</param>
        /// <param name="weightInt">the weight of the parcel (0/1/2)</param>
        /// <param name="priorityInt">the priority of the parcel (0/1/2)</param>
        public void AddParcel(int senderId, int targetId, int weightInt, int priorityInt)
        {
            DataSource.Parcels.Add(new Parcel(DataSource.Config.parcelId(), senderId, targetId, (WeightCategories)weightInt, (Priorities)priorityInt));
        }
        /// <summary>
        /// link parcel to drone
        /// </summary>
        /// <param name="parcelId">the parcel ID</param>
        /// <param name="droneId">the drone ID</param>
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
        /// <summary>
        /// update parcel pickUp time to the current time
        /// </summary>
        /// <param name="parcelId">the parcel ID to update</param>
        public void PickParcel(int parcelId)
        {
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = DataSource.Parcels.IndexOf(parcelTmp);
            parcelTmp.PickedUp = DateTime.Now;
            DataSource.Parcels[index] = parcelTmp;
        }
        /// <summary>
        /// update parcel Deliverd time to the current time
        /// </summary>
        /// <param name="parcelId">the parcel ID to update</param>
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
        /// <summary>
        /// Charge the drone
        /// </summary>
        /// <param name="stationId">the station ID with the charger</param>
        /// <param name="droneId">the drone ID</param>
        public void DroneToBase(int stationId, int droneId)
        {
            DataSource.Charges.Add(new DroneCharge(droneId, stationId));
            Station stationTmp = GetBaseStation(stationId);
            int index = DataSource.BaseStations.IndexOf(stationTmp);
            stationTmp.FreeChargeSlots--;
            DataSource.BaseStations[index] = stationTmp;


            Drone droneTmp = GetDrone(droneId);
            index = DataSource.Drones.IndexOf(droneTmp);
            droneTmp.Battery= 100;
            DataSource.Drones[index] = droneTmp;
        }
        /// <summary>
        /// Free drone frome charging
        /// </summary>
        /// <param name="droneId">the drone ID to release</param>
        public void FreeDrone(int droneId)
        {
            DroneCharge charger = DataSource.Charges.Find(charger => charger.Droneld == droneId);
            DataSource.Charges.Remove(charger);

            Station stationTmp = GetBaseStation(charger.Stationld);
            int index = DataSource.BaseStations.IndexOf(stationTmp);
            stationTmp.FreeChargeSlots++;
            DataSource.BaseStations[index] = stationTmp;
        }
        /// <summary>
        /// get array of all base satations
        /// </summary>
        /// <returns>array of all base satations</returns>
        public Station[] GetAllStations()
        {
            return DataSource.BaseStations.ToArray();
        }
        /// <summary>
        /// get array of all drones
        /// </summary>
        /// <returns>array of all drones</returns>
        public Drone[] GetAllDrones()
        {
            return DataSource.Drones.ToArray();
        }
        /// <summary>
        /// get array of all customers
        /// </summary>
        /// <returns>array of all customer</returns>
        public Customer[] GetAllCustomers()
        {
            return DataSource.Customers.ToArray();
        }
        /// <summary>
        /// get array of all parcels
        /// </summary>
        /// <returns>array of all parcels</returns>
        public Parcel[] GetAllParcels()
        {
            return DataSource.Parcels.ToArray();
        }
        /// <summary>
        /// get array of all unassigned parcels
        /// </summary>
        /// <returns>array of parcels</returns>
        public Parcel[] GetUnassignedParcels()
        {
            return DataSource.Parcels.FindAll(parcel => parcel.DroneId == 0).ToArray();
        }
        /// <summary>
        /// get all base stations that has free cahrge slots
        /// </summary>
        /// <returns>array of all the base stations that has free charge slots</returns>
        public Station[] GetStationsWithFreeSlots()
        {
            return DataSource.BaseStations.FindAll(station => station.FreeChargeSlots != 0).ToArray();
        }
    }
}
