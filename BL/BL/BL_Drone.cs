using System;
using IBL.BO;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL
    {
        public void AddDrone(Drone drone, int stationId)
        {
            try
            {
                DalObject.AddDrone(drone.Id, drone.Model, (IDAL.DO.WeightCategories)drone.MaxWeight);

                DroneToList newDrone = new();
                newDrone.Id = drone.Id;
                newDrone.Model = drone.Model;
                newDrone.MaxWeight = drone.MaxWeight;
                newDrone.Battery = new Random().Next(20, 41);
                newDrone.Status = DroneStatus.UnderMaintenance;
                newDrone.CurrentLocation = GetStation(stationId).Location;
                Drones.Add(newDrone);
            }
            catch (IDAL.DO.IdAlreadyExistsException ex)
            { 
                throw new IBL.BL.IdAlreadyExistsException(ex.Message, ex.Id);
            }
        }
        public Drone GetDrone(int droneId)
        {
            int droneIndex = Drones.FindIndex(drone => drone.Id == droneId);
            if (droneIndex==-1)
                throw new IBL.BL.IdNotFoundException($"Can't find drone with ID #{droneId}", droneId);
            DroneToList tmpDrone = Drones[droneIndex];
            Drone newDrone = new();
            newDrone.Id = tmpDrone.Id;
            newDrone.Model = tmpDrone.Model;
            newDrone.Status = tmpDrone.Status;
            newDrone.MaxWeight = tmpDrone.MaxWeight;
            newDrone.CurrentLocation = tmpDrone.CurrentLocation;
            newDrone.Battery = tmpDrone.Battery;

            ParcelInTransfer parcel = new();
            Parcel tmpParcel = GetParcerl(tmpDrone.PacrelId);
            parcel.Id = tmpParcel.Id;
            parcel.Prioritie = tmpParcel.Prioritie;
            parcel.Weight = tmpParcel.Weight;
            parcel.Sender = tmpParcel.Sender;
            parcel.Target = tmpParcel.Target;
            parcel.PickupLocation = GetCustomer(tmpParcel.Sender.Id).Location;
            parcel.TargetLocation = GetCustomer(tmpParcel.Target.Id).Location;
            parcel.IsInTransfer = tmpParcel.DatePickup != DateTime.MinValue;
            parcel.Distance = calculateDist(parcel.PickupLocation, parcel.TargetLocation);

            newDrone.Parcel = parcel;
            return newDrone;

        }
        public IEnumerable<DroneToList> GetAllDrones()
        {
            return Drones;
        }

        public void DroneToStation(int droneId)
        {
            Drone myDrone = GetDrone(droneId);
            if (myDrone.Status != DroneStatus.Available)
                throw new IBL.BL.CantSendDroneToChargeException("Drone is not available!");

            foreach (BaseStationToList station in GetStationsWithFreeSlots())
            {
                int batteryNeeded = batteryNeedForDest(GetStation(station.Id).Location, myDrone.CurrentLocation);
                if (myDrone.Battery >= batteryNeeded)
                {
                    myDrone.Battery = myDrone.Battery - batteryNeeded;
                    myDrone.CurrentLocation = GetStation(station.Id).Location;
                    myDrone.Status = DroneStatus.UnderMaintenance;

                    DalObject.DroneToStation(station.Id,droneId);
                    return;
                }
            }
            throw new IBL.BL.CantSendDroneToChargeException("There is no station that the drone is able to charge at!");
        }

        public void UpdateDrone(int id, string model)
        {
            try
            {
                DalObject.UpdateDrone(id, model);
                int droneIndex = Drones.FindIndex(drone => drone.Id == id);
                Drones[droneIndex].Model = model;                
            }
            catch (IDAL.DO.IdNotFoundException ex)
            {
                throw new IBL.BL.IdNotFoundException(ex.Message, ex.Id);
            }
        }

        private int batteryNeedForDest(Location dest, Location currentLoc, bool empty = true, WeightCategories weight=WeightCategories.Light)
        {
            double powerUse = 0;
            if (empty)
                powerUse = powerUseEmpty;
            else
            {
                switch (weight)
                {
                    case WeightCategories.Light:
                        powerUse = powerUseLight;
                    break;
                    case WeightCategories.Middle:
                        powerUse = powerUseLight;
                        break;
                    case WeightCategories.Heavy:
                        powerUse = powerUseHeavy;
                        break;
                    default:
                        break;
                }
            }
            return (int)(calculateDist(currentLoc, dest) * powerUse);
        }
    }
}
