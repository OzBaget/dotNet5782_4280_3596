﻿using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    
    public class BL : IBL.IBL
    {

        public void AddBase(string name, double lat, double lng, int chargSlots)
        {
            throw new NotImplementedException();
        }

        public void AddCustomer(string name, string phone, double lat, double lng)
        {
            throw new NotImplementedException();
        }

        public void AddDrone(string model, int maxWeightInt)
        {
            throw new NotImplementedException();
        }

        public int AddParcel(int senderId, int targetId, int weightInt, int priorityInt)
        {
            throw new NotImplementedException();
        }

        public void DroneToBase(int stationId, int droneId)
        {
            throw new NotImplementedException();
        }

        public void FreeDrone(int droneId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAllDrones()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetAllParcels()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BaseSitation> GetAllStations()
        {
            
            throw new NotImplementedException();
        }

        public BaseSitation GetBaseStation(int stationId)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public Drone GetDrone(int droneId)
        {
            throw new NotImplementedException();
        }

        public Parcel GetParcerl(int parcelId)
        {
            throw new NotImplementedException();
        }

        public double[] GetPowerUse()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BaseSitation> GetStationsWithFreeSlots()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetUnassignedParcels()
        {
            throw new NotImplementedException();
        }

        public void linkParcel(int parcelId, int droneId)
        {
            throw new NotImplementedException();
        }

        public void ParcelToCustomer(int parcelId)
        {
            throw new NotImplementedException();
        }

        public void PickParcel(int parcelId)
        {
            throw new NotImplementedException();
        }
    }
}