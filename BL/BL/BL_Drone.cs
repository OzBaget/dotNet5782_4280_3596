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
    }
}
