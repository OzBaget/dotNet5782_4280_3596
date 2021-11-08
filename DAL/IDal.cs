using System.Collections.Generic;
using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        Station GetBaseStation(int stationId);
        Drone GetDrone(int droneId);
        Customer GetCustomer(int customerId);
        Parcel GetParcerl(int parcelId);
        void AddBase(string name, double lat, double lng, int chargSlots);
        void AddDrone(string model, int maxWeightInt);
        void AddCustomer(string name, string phone, double lat, double lng);
        int AddParcel(int senderId, int targetId, int weightInt, int priorityInt);
        void linkParcel(int parcelId, int droneId);
        void PickParcel(int parcelId);
        void ParcelToCustomer(int parcelId);
        void DroneToBase(int stationId, int droneId);
        void FreeDrone(int droneId);
        IEnumerable<Station> GetAllStations();
        IEnumerable<Drone> GetAllDrones();
        IEnumerable<Customer> GetAllCustomers();
        IEnumerable<Parcel> GetAllParcels();
        IEnumerable<Parcel> GetUnassignedParcels();
        IEnumerable<Station> GetStationsWithFreeSlots();
        double[] GetPowerUse();
        DalObject a = new DalObject;
    }
}
