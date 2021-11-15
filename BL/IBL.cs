using System;
using System.Collections.Generic;

namespace IBL
{
    public interface IBL
    {
        BO.BaseStation GetStation(int stationId);
        BO.Drone GetDrone(int droneId);
        BO.Customer GetCustomer(int customerId);
        BO.Parcel GetParcerl(int parcelId);
        void AddStation(BO.BaseStation station);
        void AddDrone(BO.Drone drone);
        void AddCustomer(BO.Customer customer);
        int AddParcel(BO.Parcel parcel);
        void linkParcel(int parcelId);
        void PickParcel(int parcelId);
        void ParcelToCustomer(int droneID);
        void DroneToStation(int stationId, int droneId);
        void FreeDrone(int droneId,double droneTime);
        void UpdateDroneModel(int droneId, string model);
        void UpdateStation(int droneId, string name, int numChargers);
        void UpdateCustomer(int customerId, string name, string phone);

        IEnumerable<BO.BaseStation> GetAllStations();
        IEnumerable<BO.Customer> GetAllDrones();
        IEnumerable<BO.Customer> GetAllCustomers();
        IEnumerable<BO.Parcel> GetAllParcels();
        IEnumerable<BO.Parcel> GetUnassignedParcels();
        IEnumerable<BO.BaseStation> GetStationsWithFreeSlots();
        double[] GetPowerUse();
    }
}
