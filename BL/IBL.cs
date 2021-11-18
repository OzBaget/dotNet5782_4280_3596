using System;
using System.Collections.Generic;

namespace IBL
{
    public interface IBL
    {


        BO.BaseStation GetStation(int stationId);
        BO.Drone GetDrone(int droneId);
        BO.Customer GetCustomer(int customerId);
        BO.Parcel GetParcel(int parcelId);
        void AddStation(BO.BaseStation station);
        void AddDrone(BO.Drone drone,int idStation);
        void AddCustomer(BO.Customer customer);
        void AddParcel(BO.Parcel parcel);
        void linkParcel(int parcelId);
        void PickParcel(int parcelId);
        void ParcelToCustomer(int droneID);
        int DroneToStation(int droneId);
        void FreeDrone(int droneId,double droneTime);
        void UpdateDrone(int droneId, string model);
        void UpdateStation(int droneId, string name, int numChargers);
        void UpdateCustomer(int customerId, string name, string phone);

        IEnumerable<BO.BaseStationToList> GetAllStations();
        IEnumerable<BO.DroneToList> GetAllDrones();
        IEnumerable<BO.CustomerToList> GetAllCustomers();
        IEnumerable<BO.ParcelToList> GetAllParcels();
        IEnumerable<BO.ParcelToList> GetUnassignedParcels();
        IEnumerable<BO.BaseStationToList> GetStationsWithFreeSlots();
    }
}
