using System;
using System.Collections.Generic;
using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        Station GetStation(int stationId);
        Drone GetDrone(int droneId);
        Customer GetCustomer(int customerId);
        Parcel GetParcerl(int parcelId);

        void AddStation(int id,string name, double lat, double lng, int chargSlots);
        void AddDrone(int id,string model, WeightCategories maxWeight);
        void AddCustomer(int id, string name, string phone, double lat, double lng);
        void AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority, DateTime requsted, DateTime scheduled, DateTime pickedUp, DateTime delivered);

        void DeleteStation(int stationId);                                                                                                              
        void DeleteDrone(int droneId);                                                                                                                 
        void DeleteCustomer(int customerId);
        void DeleteParcel(int parcelId);
        void linkParcel(int parcelId, int droneId);
        void PickParcel(int parcelId);
        void ParcelToCustomer(int parcelId);
        void DroneToStation(int stationId, int droneId);
        void FreeDrone(int droneId);
        void UpdateCustomer(int customerId, string name, string phone);
        void UpdateDrone(int droneId, string model);
        void UpdateStation(int stationId, string name, int numChargers);



        IEnumerable<Station> GetAllStations();
        IEnumerable<Drone> GetAllDrones();
        IEnumerable<Customer> GetAllCustomers();
        IEnumerable<Parcel> GetAllParcels();
        IEnumerable<DroneCharge> GetAllDroneCharge();
        IEnumerable<Parcel> GetUnassignedParcels();
        IEnumerable<Station> GetStationsWithFreeSlots();
        double[] GetPowerUse();
        double GetChargingRate();
    }
}
