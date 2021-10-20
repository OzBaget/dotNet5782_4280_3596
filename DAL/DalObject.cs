﻿using System;
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
        public Station GetBaseStation(int stationId) 
        {
            return DataSource.BaseStations.Find(station => station.Id == stationId);
        }
        public Drone GetDrone(int droneId) 
        {
            return DataSource.Drones.Find(drone => drone.Id == droneId);
        }
        public Customer GetCustomer(int customerId)
        {
            return DataSource.Customers.Find(customer=> customer.Id == customerId);
        }
        public Parcel GetParcerl(int parcelId) 
        {
            return DataSource.Parcels.Find(parcel => parcel.Id == parcelId);
        }
        public void AddBase(string name, double lat, double lng, int chargSlots)
        {
            DataSource.BaseStations.Add(new Station(name, lat, lng, chargSlots));
        }
        public void AddDrone(string model, int maxWeightInt)
        {
            DataSource.Drones.Add(new Drone(model, (WeightCategories)maxWeightInt));
        }
        public void AddCustomer(string name, string phone, double lat, double lng)
        {
            DataSource.Customers.Add(new Customer(name, phone, lat, lng));
        }
        public void AddParcel(int senderId, int targetId, int weightInt, int priorityInt)
        {
            DataSource.Parcels.Add(new Parcel(senderId, targetId, (WeightCategories)weightInt, (Priorities)priorityInt));
        }
        public void linkParcel(int parcelId, int droneId)
        {
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = DataSource.Parcels.IndexOf(parcelTmp);
            parcelTmp.DroneId = droneId;
            parcelTmp.Scheduled = DateTime.Now;
            DataSource.Parcels[index] = parcelTmp;

            Drone droneTmp = GetDrone(droneId);
            index = DataSource.Drones.IndexOf(droneTmp);
            droneTmp.Status = DroneStatuses.Delivery;
            DataSource.Drones[index] = droneTmp;
        }
        public void PickParcel(int parcelId)
        {
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = DataSource.Parcels.IndexOf(parcelTmp);
            parcelTmp.PickedUp = DateTime.Now;
            DataSource.Parcels[index] = parcelTmp;
        }
        public void ParcelToCustomer(int parcelId)
        {
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = DataSource.Parcels.IndexOf(parcelTmp);
            parcelTmp.Delivered = DateTime.Now;
            int droneId = parcelTmp.DroneId;
            DataSource.Parcels[index] = parcelTmp;

            Drone droneTmp = GetDrone(droneId);
            index = DataSource.Drones.IndexOf(droneTmp);
            droneTmp.Status = DroneStatuses.Available;
            DataSource.Drones[index] = droneTmp;
        }
        public void DroneToBase(int station, int drone)
        {
            //TODO
        }
        public void FreeDrone(int drone)
        {
            //TODO
        }

        public List<Station> GetAllStations()
        {
            return DataSource.BaseStations;
        }
        public List<Drone> GetAllDrones()
        {
            return DataSource.Drones;
        }
        public List<Customer> GetAllCustomers()
        {
            return DataSource.Customers;
        }
        public List<Parcel> GetAllParcels()
        {
            return DataSource.Parcels;
        }
        public List<Parcel> GetUnassignedParcels()
        {
            return DataSource.Parcels.FindAll(parcel=>parcel.DroneId == 0);
        }
        public List<Station> GetAvailableStations()
        {
            return DataSource.BaseStations.FindAll(station=>station.FreeChargeSlots != 0);
        }




    }
}
