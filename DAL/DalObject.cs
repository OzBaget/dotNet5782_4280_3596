using System;
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
        public void AddBase(IDAL.DO.Station station) { }
        public void AddDrone(IDAL.DO.Drone drone) { }
        public void AddCustomer(IDAL.DO.Customer customer) { }
        public void linkParcel(int parcel, int drone) { }
        public void PickParcel(int parcel) { }
        public void ParcelToCustomer(int parcel) { }
        public void DroneToBase(int station,int drone) { }
        public void FreeDrone(int drone) { }
        public IDAL.DO.Station GetStation(int station) { return new IDAL.DO.Station(); }
        public IDAL.DO.Drone GetDrone(int drone) { return new IDAL.DO.Drone(); }
        public IDAL.DO.Customer GetCustomer(int customer) { return new IDAL.DO.Customer(); }
        public IDAL.DO.Parcel GetParcerl(int parcel) { return new IDAL.DO.Parcel(); }
        public IDAL.DO.Station[] GetAllStations() { return DataSource.BaseStations; }

        public IDAL.DO.Drone[] GetAllDrones() { return DataSource.Drones; }
        public IDAL.DO.Customer[] GetAllCustomers() { return DataSource.Customers; }
        public IDAL.DO.Parcel[] GetAllParcerls() { return DataSource.Parcels; }
    }
}
