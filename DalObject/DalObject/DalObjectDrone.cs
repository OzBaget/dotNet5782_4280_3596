using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DO;

namespace Dal
{
    sealed partial class DalObject : DalApi.IDal
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int droneId)
        {
            DataSource.Drones.Remove(GetDrone(droneId));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(int droneId, string model)
        {
            Drone tmpDrone = GetDrone(droneId);
            DeleteDrone(droneId);
            AddDrone(tmpDrone.Id, model, tmpDrone.MaxWeight);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] GetPowerUse()
        {
            double[] powerUse = { DataSource.Config.Free,
                DataSource.Config.LightPacket, 
                DataSource.Config.MediumPacket, 
                DataSource.Config.HeavyPacket };
            return powerUse;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetChargingRate()
        {
            return DataSource.Config.ChargingRate;
        }




        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneToStation(int stationId, int droneId)
        {
            DroneCharge myDC = new();
            myDC.Droneld = droneId;
            myDC.Stationld = stationId;
            myDC.PlugedIn = DateTime.Now;
            DataSource.Charges.Add(myDC);
            Station stationTmp = GetStation(stationId);
            int index = DataSource.BaseStations.IndexOf(stationTmp);
            stationTmp.FreeChargeSlots--;
            DataSource.BaseStations[index] = stationTmp;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public TimeSpan FreeDrone(int droneId)
        {
            DroneCharge charger = DataSource.Charges.Find(charger => charger.Droneld == droneId);
            DataSource.Charges.Remove(charger);

            Station stationTmp = GetStation(charger.Stationld);
            int index = DataSource.BaseStations.IndexOf(stationTmp);
            stationTmp.FreeChargeSlots++;
            DataSource.BaseStations[index] = stationTmp;
            return (DateTime.Now - charger.PlugedIn).Value;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetAllDrones()
        {
            return new List<Drone>(DataSource.Drones);
        }
    }
}
