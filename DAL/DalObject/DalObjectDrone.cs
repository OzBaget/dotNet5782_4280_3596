using System.Collections.Generic;
using IDAL.DO;
namespace DalObject
{
    public partial class DalObject
    {

        /// <summary>
        /// Add drone to Drones list in DataSource
        /// </summary>
        /// <param name="model">the modle of the drone</param>
        /// <param name="maxWeightInt">max weight of the drone (0/1/2)</param>
        public void AddDrone(int id, string model, WeightCategories maxWeight)
        {
            bool droneExists = false;
            foreach (Drone drone in DataSource.Drones)
                if (drone.Id == id)
                    droneExists = true;
            if(droneExists)
                throw new IdAlreadyExistsException($"Drone with ID #{id} already exists!", id);

            DataSource.Drones.Add(new Drone(id,model, maxWeight));
        }
        /// <summary>
        /// returns drone by ID
        /// </summary>
        /// <param name="droneId"> the drone ID</param>
        /// <returns>Drone object of the requsted ID (by value)</returns>
        public Drone GetDrone(int droneId)
        {
            bool droneExists = false;

            foreach (Drone drone in GetAllDrones())
                if (drone.Id == droneId)
                    droneExists = true;

            if (!droneExists)
                throw new IdNotFoundException($"Can't find drone with ID #{droneId}", droneId);

            return DataSource.Drones.Find(drone => drone.Id == droneId);
        }

        public void DeleteDrone(int droneId)
        {
            DataSource.Drones.Remove(GetDrone(droneId));
        }

        public void UpdateDrone(int droneId, string model)
        {
            Drone tmpDrone = GetDrone(droneId);
            DeleteDrone(droneId);
            AddDrone(tmpDrone.Id, model, tmpDrone.MaxWeight);
        }

        public double[] GetPowerUse()
        {
            double[] powerUse = { DataSource.Config.Free,
                DataSource.Config.LightPacket, 
                DataSource.Config.MediumPacket, 
                DataSource.Config.HeavyPacket };
            return powerUse;
        }
        public double GetChargingRate()
        {
            return DataSource.Config.ChargingRate;
        }

        

        /// <summary>
        /// Charge the drone
        /// </summary>
        /// <param name="stationId">the station ID with the charger</param>
        /// <param name="droneId">the drone ID</param>
        public void DroneToStation(int stationId, int droneId)
        {
            DataSource.Charges.Add(new DroneCharge(droneId, stationId));
            Station stationTmp = GetStation(stationId);
            int index = DataSource.BaseStations.IndexOf(stationTmp);
            stationTmp.FreeChargeSlots--;
            DataSource.BaseStations[index] = stationTmp;


            Drone droneTmp = GetDrone(droneId);
            index = DataSource.Drones.IndexOf(droneTmp);
            DataSource.Drones[index] = droneTmp;
        }

        /// <summary>
        /// Free drone frome charging
        /// </summary>
        /// <param name="droneId">the drone ID to release</param>
        public void FreeDrone(int droneId)
        {
            DroneCharge charger = DataSource.Charges.Find(charger => charger.Droneld == droneId);
            DataSource.Charges.Remove(charger);

            Station stationTmp = GetStation(charger.Stationld);
            int index = DataSource.BaseStations.IndexOf(stationTmp);
            stationTmp.FreeChargeSlots++;
            DataSource.BaseStations[index] = stationTmp;
        }

        /// <summary>
        /// get array of all drones
        /// </summary>
        /// <returns>array of all drones</returns>
        public IEnumerable<Drone> GetAllDrones()
        {
            return new List<Drone>(DataSource.Drones);
        }
    }
}
