using DO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dal
{
    internal sealed partial class DalXml : DalApi.IDal
    {
        public void AddDrone(int id, string model, WeightCategories maxWeight)
        {
            bool droneExists = false;
            List<Drone> myList = loadXmlToList<Drone>();
            foreach (Drone drone in myList)
                if (drone.Id == id)
                    droneExists = true;
            if (droneExists)
                throw new IdAlreadyExistsException($"Drone with ID #{id} already exists!", id);
            Drone myDrone = new();
            myDrone.Id = id;
            myDrone.Model = model;
            myDrone.MaxWeight = maxWeight;
            myDrone.IsActived = true;
            myList.Add(myDrone);
            saveListToXml(myList);
        }

        public Drone GetDrone(int droneId)
        {
            bool droneExists = false;
            List<Drone> myList = loadXmlToList<Drone>();
            foreach (Drone drone in myList)
                if (drone.Id == droneId && drone.IsActived)
                    droneExists = true;

            if (!droneExists)
                throw new IdNotFoundException($"Can't find drone with ID #{droneId}", droneId);

            return myList.Find(drone => drone.Id == droneId);
        }

        public void DeleteDrone(int droneId)
        {
            List<Drone> myList = loadXmlToList<Drone>();
            myList.Remove(GetDrone(droneId));
            saveListToXml(myList);
        }

        public void UpdateDrone(int droneId, string model)
        {
            Drone tmpDrone = GetDrone(droneId);
            DeleteDrone(droneId);
            AddDrone(tmpDrone.Id, model, tmpDrone.MaxWeight);
        }

        public double[] GetPowerUse()
        {
            double[] powerUse = { Config.Free,
                Config.LightParcel,
                Config.MediumParcel,
                Config.HeavyParcel };
            return powerUse;
        }
        public double GetChargingRate()
        {
            return Config.ChargingRate;
        }




        public void DroneToStation(int stationId, int droneId)
        {
            List<DroneCharge> myChargeList = loadXmlToList<DroneCharge>();
            DroneCharge myDC = new();
            myDC.Droneld = droneId;
            myDC.Stationld = stationId;
            myDC.PlugedIn = DateTime.Now;
            myChargeList.Add(myDC);
            saveListToXml(myChargeList);

            Station stationTmp = GetStation(stationId);
            List<Station> myStationList = loadXmlToList<Station>();
            int index = myStationList.IndexOf(stationTmp);
            stationTmp.FreeChargeSlots--;
            myStationList[index] = stationTmp;
            saveListToXml(myStationList);
        }


        public TimeSpan FreeDrone(int droneId)
        {
            List<DroneCharge> myChargeList = loadXmlToList<DroneCharge>();
            DroneCharge charger = myChargeList.Find(charger => charger.Droneld == droneId);
            myChargeList.Remove(charger);
            saveListToXml(myChargeList);

            Station stationTmp = GetStation(charger.Stationld);
            List<Station> myStationList = loadXmlToList<Station>();
            int index = myStationList.IndexOf(stationTmp);
            stationTmp.FreeChargeSlots++;
            myStationList[index] = stationTmp;
            saveListToXml(myStationList);
            return (DateTime.Now - charger.PlugedIn).Value;
        }


        public IEnumerable<Drone> GetAllDrones()
        {
            return loadXmlToList<Drone>().Where(d => d.IsActived);
        }
    }
}
