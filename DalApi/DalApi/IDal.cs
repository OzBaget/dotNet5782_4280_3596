using System;
using System.Collections.Generic;
using DO;

namespace DalApi
{
    public interface IDal
    {
        //==========Geters==========//
        /// <summary>
        /// returns base station by ID
        /// </summary>
        /// <param name="stationId"> the base station ID</param>
        /// <returns>Return a Station object of the requsted ID (by value)</returns>
        Station GetStation(int stationId);

        /// <summary>
        /// returns drone by ID
        /// </summary>
        /// <param name="droneId"> the drone ID</param>
        /// <returns>Drone object of the requsted ID (by value)</returns>
        Drone GetDrone(int droneId);

        /// <summary>
        /// returns customer by ID
        /// </summary>
        /// <param name="customerId"> the customer ID</param>
        /// <returns>Customer object of the requsted ID (by value)</returns>
        Customer GetCustomer(int customerId);

        /// <summary>
        /// returns parcel by ID
        /// </summary>
        /// <param name="parcelId"> the parcel ID</param>
        /// <returns>Parcel object of the requsted ID (by value)</returns>
        Parcel GetParcerl(int parcelId);


        /// <summary>
        /// get IEnumerable of all base satations
        /// </summary>
        /// <returns>IEnumerable of all base satations</returns>
        IEnumerable<Station> GetAllStations();

        /// <summary>
        /// get IEnumerable of all drones
        /// </summary>
        /// <returns>IEnumerable of all drones</returns>
        IEnumerable<Drone> GetAllDrones();

        /// <summary>
        /// get IEnumerable of all customers
        /// </summary>
        /// <returns>IEnumerable of all customer</returns>
        IEnumerable<Customer> GetAllCustomers();

        /// <summary>
        /// get IEnumerable of all parcels 
        /// </summary>
        /// <returns>IEnumerable of all parcels</returns>
        IEnumerable<Parcel> GetAllParcels();

        /// <summary>
        /// returns list of drone chargers
        /// </summary>
        /// <returns>list of droneChargers</returns>
        IEnumerable<DroneCharge> GetAllDroneCharge();


        /// <summary>
        /// get IEnumerable of all unassigned parcels 
        /// </summary>
        /// <returns>IEnumerable of all unassigned parcels</returns>
        IEnumerable<Parcel> GetFilterdParcels(Predicate<Parcel> filter);

        /// <summary>
        /// get IEnumerable of all the stations with free slots
        /// </summary>
        /// <returns>IEnumerable odf stations</returns>
        IEnumerable<Station> GetFilterdStations(Predicate<Station> filter);

        /// <summary>
        /// returns power uses of drone
        /// </summary>
        /// <returns> array like this: {empty, light, medium, heavy}</returns>
        double[] GetPowerUse();

        /// <summary>
        /// returns the charging rate of drone
        /// </summary>
        /// <returns>precent to hour double</returns>
        double GetChargingRate();

        //==========Adders==========//

        /// <summary>
        /// Add base station to the BaseStations list in DataSource
        /// </summary>
        /// <param name="id">the ID of the station</param>
        /// <param name="name">the name of the station</param>
        /// <param name="lat">the latitude of the station</param>
        /// <param name="lng">the longitude of the station</param>
        /// <param name="chargSlots">hw many charge slosts are in the station</param>
        void AddStation(int id, string name, double lat, double lng, int chargSlots);

        /// <summary>
        /// Add drone to Drones list in DataSource
        /// </summary>
        /// <param name="id">the ID of the drone</param>
        /// <param name="model">the modle of the drone</param>
        /// <param name="maxWeight">max weight of the drone</param>
        void AddDrone(int id, string model, WeightCategories maxWeight);

        /// <summary>
        /// Add customer to Customers list in DataSource
        /// </summary>
        /// <param name="id">the ID of the customer</param>
        /// <param name="name">the name of the customer</param>
        /// <param name="phone">the phone of the customer</param>
        /// <param name="lat">the latitude of the customer</param>
        /// <param name="lng">the longitude of the customer</param>
        void AddCustomer(int id, string name, string phone, double lat, double lng);

        /// <summary>
        /// Add parcel to Parcels list in DataSource
        /// </summary>
        /// <param name="senderId">sender customer ID</param>
        /// <param name="targetId">target customer ID</param>
        /// <param name="weight">the weight of the parcel</param>
        /// <param name="priority">the priority of the parcel</param>
        /// <param name="requsted">the creation date of the parcel</param>
        /// <param name="scheduled">the link time of the parcel</param>
        /// <param name="pickedUp">the pick up date of the parcel</param>
        /// <param name="delivered">the delivery date of the parcel</param>
        void AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority, DateTime? requsted, DateTime? scheduled, DateTime? pickedUp, DateTime? delivered);


        //==========Deleters==========//


        /// <summary>
        /// Deletes station by ID
        /// </summary>
        /// <param name="stationId">the station ID</param>
        void DeleteStation(int stationId);

        /// <summary>
        /// Deletes drone by ID
        /// </summary>
        /// <param name="droneId">the drone ID</param>
        void DeleteDrone(int droneId);

        /// <summary>
        /// Deletes cutomer by ID 
        /// </summary>
        /// <param name="customerId">the customer ID</param>
        void DeleteCustomer(int customerId);

        /// <summary>
        /// Deletes parcel by ID 
        /// </summary>
        /// <param name="parcelId">the parcel ID</param>
        void DeleteParcel(int parcelId);


        //==========Updaters==========//


        /// <summary>
        /// links parcel to drone, and update schudlde time
        /// </summary>
        /// <param name="parcelId">the parcel ID</param>
        /// <param name="droneId">the drone ID</param>
        void linkParcel(int parcelId, int droneId);

        /// <summary>
        /// update parcel pickUp time to the current time
        /// </summary>
        /// <param name="parcelId">the parcel ID to update</param>
        void PickParcel(int parcelId);

        /// <summary>
        /// update parcel Deliverd time to the current time
        /// </summary>
        /// <param name="parcelId">the parcel ID to update</param>
        void ParcelToCustomer(int parcelId);

        /// <summary>
        /// send drone to charge in station
        /// </summary>
        /// <param name="stationId">the station ID to charge at</param>
        /// <param name="droneId">the drone ID</param>
        void DroneToStation(int stationId, int droneId);

        /// <summary>
        /// release drone from charging
        /// </summary>
        /// <param name="droneId">the drone ID to release</param>
        void FreeDrone(int droneId);

        /// <summary>
        /// updates customer ditails
        /// </summary>
        /// <param name="customerId">the customr ID to update</param>
        /// <param name="name">new name</param>
        /// <param name="phone">new phone</param>
        void UpdateCustomer(int customerId, string name, string phone);

        /// <summary>
        /// updates drone's model
        /// </summary>
        /// <param name="droneId">the drone ID to update</param>
        /// <param name="model">new model</param>
        void UpdateDrone(int droneId, string model);

        /// <summary>
        /// updates station's ditails
        /// </summary>
        /// <param name="stationId">the station ID to update</param>
        /// <param name="name">new name</param>
        /// <param name="numChargers">total chargers in station</param>
        void UpdateStation(int stationId, string name, int numChargers);


    }
}
