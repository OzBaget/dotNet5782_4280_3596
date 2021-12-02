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

                DalObject.DroneToStation(stationId, drone.Id);
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
            if (tmpDrone.ParcelId != 0) 
            {
            Parcel tmpParcel = GetParcel(tmpDrone.ParcelId);
            parcel.Id = tmpParcel.Id;
            parcel.Prioritie = tmpParcel.Prioritie;
            parcel.Weight = tmpParcel.Weight;
            parcel.Sender = tmpParcel.Sender;
            parcel.Target = tmpParcel.Target;
            parcel.PickupLocation = GetCustomer(tmpParcel.Sender.Id).Location;
            parcel.TargetLocation = GetCustomer(tmpParcel.Target.Id).Location;
                parcel.IsInTransfer = tmpParcel.DatePickup != null && tmpParcel.DateDeliverd == null;
            parcel.Distance = calculateDist(parcel.PickupLocation, parcel.TargetLocation);
            }
            newDrone.Parcel = parcel;
            return newDrone;

        }
        public IEnumerable<DroneToList> GetAllDrones()
        {
            return Drones;
        }

        public int DroneToStation(int droneId)
        {
            int droneIndex = Drones.FindIndex(drone => drone.Id == droneId);
            if (droneIndex == -1)
                throw new IBL.BL.IdNotFoundException($"Can't find drone with ID #{droneId}", droneId);
            DroneToList myDrone = Drones[droneIndex];//copy by ref..
            if (Drones[droneIndex].Status != DroneStatus.Available)
                throw new IBL.BL.CantSendDroneToChargeException("Drone is not available!");

            foreach (BaseStationToList station in GetStationsWithFreeSlots())
            {
                int batteryNeeded = batteryNeedForTrip(GetStation(station.Id).Location, myDrone.CurrentLocation);
                if (myDrone.Battery >= batteryNeeded)
                {
                    myDrone.Battery -= batteryNeeded;
                    myDrone.Status = DroneStatus.UnderMaintenance;
                    myDrone.CurrentLocation = GetStation(station.Id).Location;
                    DalObject.DroneToStation(station.Id, droneId);
                    return station.Id;
                }
            }
            throw new IBL.BL.CantSendDroneToChargeException("There is no station that the drone is able to charge at!");
        }
        public int FreeDrone(int droneId, TimeSpan droneTime)
        {
            int droneIndex = Drones.FindIndex(drone => drone.Id == droneId);
            if (droneIndex == -1)
                throw new IBL.BL.IdNotFoundException($"Can't find drone with ID #{droneId}", droneId);

            DroneToList myDrone = Drones[droneIndex];//copy by ref
            if (myDrone.Status != DroneStatus.UnderMaintenance)
                throw new IBL.BL.CantReleaseDroneFromChargeException("Drone is not in charging!");

            DalObject.FreeDrone(droneId);

            myDrone.Battery = (int)(droneTime.TotalHours * chargingRate)+myDrone.Battery < 100 ? myDrone.Battery + (int)(droneTime.TotalHours * chargingRate) : 100;
            myDrone.Status = DroneStatus.Available;
            return myDrone.Battery;
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


        /// <summary>
        /// calculate the batrry needed for getting from point A to point B with spacific onditions
        /// </summary>
        /// <param name="dest">point A</param>
        /// <param name="currentLoc">point B</param>
        /// <param name="empty">if dorne isn't empty-false</param>
        /// <param name="weight">if the drone isn't empty, the wight of the parcel that it carrying</param>
        /// <returns>the precnts battry needed</returns>
        private int batteryNeedForTrip(Location dest, Location currentLoc, bool empty = true, WeightCategories weight=WeightCategories.Light)
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
                        powerUse = powerUseMiddle;
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
