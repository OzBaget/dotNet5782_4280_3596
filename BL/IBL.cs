using System;
using System.Collections.Generic;
using IBL.BO;

namespace IBL
{
    public interface IBL
    {

        /// <summary>
        /// Gets id returns the station base
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        BaseStation GetStation(int stationId);
        /// <summary>
        /// Gets id returns the drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        Drone GetDrone(int droneId);
        /// <summary>
        /// Gets id returns the customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Customer GetCustomer(int customerId);
        /// <summary>
        /// Gets id returns the parcel
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        Parcel GetParcel(int parcelId);
        /// <summary>
        /// Get station add it to the data base
        /// </summary>
        /// <param name="station"></param>
        void AddStation(BO.BaseStation station);
        /// <summary>
        /// Get drone add it to the data base
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="idStation"></param>
        void AddDrone(BO.Drone drone,int idStation);
        /// <summary>
        /// Get customer add it to the data base
        /// </summary>
        /// <param name="customer"></param>
        void AddCustomer(BO.Customer customer);
        /// <summary>
        /// Get parcel and add it to the data base
        /// </summary>
        /// <param name="parcel"></param>
        void AddParcel(BO.Parcel parcel);
         /// <summary>
         ///  Link parcel to drone
         /// </summary>
         /// <param name="droneId"></param>
        int linkParcel(int droneId);
        /// <summary>
        /// Pick parcel to the drone
        /// </summary>
        /// <param name="dronelId"></param>
        void PickParcel(int dronelId);
        /// <summary>
        /// Assigning a package to a customer
        /// </summary>
        /// <param name="droneID"></param>
        void ParcelToCustomer(int droneID);
        /// <summary>
        /// Charge the drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        int DroneToStation(int droneId);
        /// <summary>
        /// Reales the drone from charging
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="droneTime"></param>
        int FreeDrone(int droneId,TimeSpan droneTime);
        /// <summary>
        /// Update the model's drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="model"></param>
        void UpdateDrone(int droneId, string model);
        /// <summary>
        /// Update the datas' base staion
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="name"></param>
        /// <param name="input"></param>
        void UpdateStation(int droneId, string name, string input);
        /// <summary>
        /// Update the datas' cutomer 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        void UpdateCustomer(int customerId, string name, string phone);
        /// <summary>
        /// Returns list of all stations
        /// </summary>
        /// <returns></returns>
        IEnumerable<BaseStationToList> GetAllStations();
        /// <summary>
        /// Returns list of all drones
        /// </summary>
        /// <returns></returns>
        IEnumerable<DroneToList> GetAllDrones();
        /// <summary>
        /// Returns list of all customers
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerToList> GetAllCustomers();
        /// <summary>
        /// Returns list of all parcels
        /// </summary>
        /// <returns></returns>
        IEnumerable<ParcelToList> GetAllParcels();
        /// <summary>
        /// Returns list of all unassigned parcels
        /// </summary>
        /// <returns></returns>
        IEnumerable<ParcelToList> GetUnassignedParcels();
        /// <summary>
        /// Returns list of all stations with free chargers
        /// </summary>
        /// <returns></returns>
        IEnumerable<BaseStationToList> GetStationsWithFreeSlots();

        IEnumerable<DroneToList> GetFilterdDrones(Predicate<DroneToList> filter);
        IEnumerable<DroneToList> GetFilterdParcels(Predicate<ParcelToList> filter);
    }
}
