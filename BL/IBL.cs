using System;
using System.Collections.Generic;

namespace IBL
{
    interface IBL
    {
        BO.BaseSitation GetBaseStation(int stationId);
        BO.Drone GetDrone(int droneId);
        BO.Customer GetCustomer(int customerId);
        BO.Parcel GetParcerl(int parcelId);
        void AddBase(string name, double lat, double lng, int chargSlots);
        void AddDrone(string model, int maxWeightInt);
        void AddCustomer(string name, string phone, double lat, double lng);
        int AddParcel(int senderId, int targetId, int weightInt, int priorityInt);
        void linkParcel(int parcelId, int droneId);
        void PickParcel(int parcelId);
        void ParcelToCustomer(int parcelId);
        void DroneToBase(int stationId, int droneId);
        void FreeDrone(int droneId);
        IEnumerable<BO.BaseSitation> GetAllStations();
        IEnumerable<BO.Customer> GetAllDrones();
        IEnumerable<BO.Customer> GetAllCustomers();
        IEnumerable<BO.Parcel> GetAllParcels();
        IEnumerable<BO.Parcel> GetUnassignedParcels();
        IEnumerable<BO.BaseSitation> GetStationsWithFreeSlots();
        double[] GetPowerUse();
    }
}
