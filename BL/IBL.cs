using System;
using System.Collections.Generic;

namespace IBL
{
    interface IBL
    {
        BO.BaseStation GetBaseStation(int stationId);
        BO.Drone GetDrone(int droneId);
        BO.Customer GetCustomer(int customerId);
        BO.Parcel GetParcerl(int parcelId);
        void AddBase(BO.BaseStation station);
        void AddDrone(BO.Drone drone);
        void AddCustomer(BO.Customer customer);
        int AddParcel(BO.Parcel parcel);
        void linkParcel(int parcelId, int droneId);
        void PickParcel(int parcelId);
        void ParcelToCustomer(int parcelId);
        void DroneToBase(int stationId, int droneId);
        void FreeDrone(int droneId);
        void UpdateDroneName(int id, string model);
        void UpdateStation(int id, string name, int numChargers);
        IEnumerable<BO.BaseStation> GetAllStations();
        IEnumerable<BO.Customer> GetAllDrones();
        IEnumerable<BO.Customer> GetAllCustomers();
        IEnumerable<BO.Parcel> GetAllParcels();
        IEnumerable<BO.Parcel> GetUnassignedParcels();
        IEnumerable<BO.BaseStation> GetStationsWithFreeSlots();
        double[] GetPowerUse();
    }
}
