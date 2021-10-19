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
        public void AddBase(string name, double lat, double lng, int chargSlots) {  }
        public void AddDrone(string model, int maxWeightInt) { }
        public void AddCustomer(string name, string phone, double lat, double lng) {  }
        public void AddParcel(int senderId, int targetId, int weightInt, int priorityInt) {  }
        public void linkParcel(int parcel, int drone) { }
        public void PickParcel(int parcel) { }
        public void ParcelToCustomer(int parcel) { }
        public void DroneToBase(int station,int drone) { }
        public void FreeDrone(int drone) { }
        public Station GetStation(int station) { return new Station(); }
        public Drone GetDrone(int drone) { return new Drone(); }
        public Customer GetCustomer(int customer) { return new Customer(); }
        public Parcel GetParcerl(int parcel) { return new Parcel(); }
        public Station[] GetAllStations() { return DataSource.BaseStations; }
        public Drone[] GetAllDrones() { return DataSource.Drones; }
        public Customer[] GetAllCustomers() { return DataSource.Customers; }
        public Parcel[] GetAllParcels() { return DataSource.Parcels; }
        public Parcel[] GetUnoccupiedParcels() { return DataSource.Parcels; }
        public Station[] GetNotFullStations() { return DataSource.BaseStations; }
    }
}
