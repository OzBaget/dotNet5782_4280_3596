﻿using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace BL
{
    internal sealed partial class BL : IBL
    {
        public void StartSimulator(int droneId, Action updateDrone, Func<bool> checkStop)
        {
            new Simulator(Instance, droneId, updateDrone, checkStop);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone drone, int stationId)
        {
            try
            {
                DroneToList newDrone = new();
                newDrone.Id = drone.Id;
                newDrone.Model = drone.Model;
                newDrone.MaxWeight = drone.MaxWeight;
                newDrone.Battery = new Random().Next(20, 41);
                newDrone.Status = DroneStatus.UnderMaintenance;
                newDrone.CurrentLocation = GetStation(stationId).Location;

                DalObject.AddDrone(drone.Id, drone.Model, (DO.WeightCategories)drone.MaxWeight);
                DalObject.DroneToStation(stationId, drone.Id);

                Drones.Add(newDrone);
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new IdAlreadyExistsException(ex.Message, ex.Id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int droneId)
        {
            lock (DalObject)
            {
                int droneIndex = Drones.FindIndex(drone => drone.Id == droneId);
                if (droneIndex == -1)
                    throw new IdNotFoundException($"Can't find drone with ID #{droneId}", droneId);

                DroneToList tmpDrone = Drones[droneIndex];

                Drone newDrone = new();
                newDrone.Id = tmpDrone.Id;
                newDrone.Model = tmpDrone.Model;
                newDrone.Status = tmpDrone.Status;
                newDrone.MaxWeight = tmpDrone.MaxWeight;
                newDrone.CurrentLocation = tmpDrone.CurrentLocation;
                newDrone.Battery = tmpDrone.Battery;
                ParcelInTransfer parcel = new();
                if (tmpDrone.ParcelId != null)
                {
                    Parcel tmpParcel = GetParcel(tmpDrone.ParcelId.Value);
                    parcel.Id = tmpParcel.Id.Value;
                    parcel.Prioritie = tmpParcel.Prioritie;
                    parcel.Weight = tmpParcel.Weight;
                    parcel.Sender = tmpParcel.Sender;
                    parcel.Target = tmpParcel.Target;

                    parcel.PickupLocation = new();
                    parcel.PickupLocation.Latitude = DalObject.GetCustomer(tmpParcel.Sender.Id).Lat;
                    parcel.PickupLocation.Longitude = DalObject.GetCustomer(tmpParcel.Sender.Id).Lng;

                    parcel.TargetLocation = new();
                    parcel.TargetLocation.Latitude = DalObject.GetCustomer(tmpParcel.Target.Id).Lat;
                    parcel.TargetLocation.Longitude = DalObject.GetCustomer(tmpParcel.Target.Id).Lng;

                    parcel.IsInTransfer = tmpParcel.DatePickup != null && tmpParcel.DateDeliverd == null;
                    if (parcel.IsInTransfer)
                        parcel.Distance = calculateDist(newDrone.CurrentLocation, parcel.TargetLocation);
                    else
                        parcel.Distance = calculateDist(newDrone.CurrentLocation, parcel.PickupLocation);
                }
                newDrone.Parcel = parcel;
                return newDrone;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetAllDrones()
        {
            return Drones;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetFilterdDrones(WeightCategories? weight, DroneStatus? status)
        {
            lock (DalObject) lock (Drones)
                {
                    if (weight == null && status != null)
                    {
                        return Drones.FindAll(d => d.Status == status);
                    }
                    if (weight != null && status == null)
                    {
                        return Drones.FindAll(d => d.MaxWeight == weight);
                    }
                    if (weight != null && status != null)
                    {
                        return Drones.FindAll(d => d.MaxWeight == weight && d.Status == status);
                    }
                    return Drones;
                }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int DroneToStation(int droneId)
        {
            lock (DalObject) lock (Drones)
                {
                    int droneIndex = Drones.FindIndex(drone => drone.Id == droneId);
                    if (droneIndex == -1)
                        throw new IdNotFoundException($"Can't find drone with ID #{droneId}", droneId);
                    DroneToList myDrone = Drones[droneIndex];//copy by ref..
                    if (Drones[droneIndex].Status != DroneStatus.Available)
                        throw new CantSendDroneToChargeException("Drone is not available!");

                    foreach (BaseStationToList station in GetStationsWithFreeSlots())
                    {
                        double batteryNeeded = batteryNeedForTrip(GetStation(station.Id).Location, myDrone.CurrentLocation);
                        if (myDrone.Battery >= batteryNeeded)
                        {
                            myDrone.Battery -= batteryNeeded;
                            myDrone.Status = DroneStatus.UnderMaintenance;
                            myDrone.CurrentLocation = GetStation(station.Id).Location;
                            DalObject.DroneToStation(station.Id, droneId);
                            return station.Id;
                        }
                    }
                }
            throw new CantSendDroneToChargeException("There is no station that the drone is able to charge at!");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double FreeDrone(int droneId)
        {
            lock (DalObject) lock (Drones)
                {
                    int droneIndex = Drones.FindIndex(drone => drone.Id == droneId);
                    if (droneIndex == -1)
                        throw new IdNotFoundException($"Can't find drone with ID #{droneId}", droneId);

                    DroneToList myDrone = Drones[droneIndex];//copy by ref
                    if (myDrone.Status != DroneStatus.UnderMaintenance)
                        throw new CantReleaseDroneFromChargeException("Drone is not in charging!");

                    TimeSpan chargeTime = DalObject.FreeDrone(droneId);

                    myDrone.Battery = (int)(chargeTime.TotalMinutes * chargingRate) + myDrone.Battery < 100 ? myDrone.Battery + (int)(chargeTime.TotalMinutes * chargingRate) : 100;
                    myDrone.Status = DroneStatus.Available;
                    return myDrone.Battery;
                }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(int droneId, string model, double? newBattery = null, Location newLoc = null)
        {

            try
            {
                lock (DalObject) lock (Drones)
                    {
                        DalObject.UpdateDrone(droneId, model);
                        int droneIndex = Drones.FindIndex(drone => drone.Id == droneId);
                        Drones[droneIndex].Model = model;
                        if (newBattery != null)
                            Drones[droneIndex].Battery = newBattery.Value;
                        if (newLoc != null)
                            Drones[droneIndex].CurrentLocation = newLoc;
                    }
            }
            catch (DO.IdNotFoundException ex)
            {
                throw new IdNotFoundException(ex.Message, ex.Id);
            }
        }





        /// <summary>
        /// calculate the batrry needed for getting from point A to point B with spacific onditions
        /// </summary>
        /// <param name="dest">point A</param>
        /// <param name="currentLoc">point B</param>
        /// <param name="empty">if dorne isn't empty-false</param>
        /// <param name="weight">if the drone isn't empty, the wight of the parcel that it carrying</param>
        /// <returns>the precnts battry needed</returns>
        private double batteryNeedForTrip(Location dest, Location currentLoc, WeightCategories? weight = null)
        {
            double powerUse = 0;
            if (weight == null)
                powerUse = powerUseEmpty;
            else
            {
                switch (weight)
                {
                    case WeightCategories.Light:
                        powerUse = powerUseLight;
                        break;
                    case WeightCategories.Middle:
                        powerUse = powerUseMiddle;
                        break;
                    case WeightCategories.Heavy:
                        powerUse = powerUseHeavy;
                        break;
                    default:
                        break;
                }
            }
            return (double)(calculateDist(currentLoc, dest) * powerUse);
        }
    }
}
