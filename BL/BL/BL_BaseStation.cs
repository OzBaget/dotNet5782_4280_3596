using System.Collections.Generic;
using BO;
using BlApi;
using System.Runtime.CompilerServices;

namespace BL
{
    sealed partial class BL : IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(BaseStation station)
        {
            try
            {

                DalObject.AddStation(station.Id, station.Name, station.Location.Latitude, station.Location.Longitude, station.NumFreeChargers);
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new IdAlreadyExistsException(ex.Message, ex.Id);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation GetStation(int stationId)
        {
            try
            {
              


                    DO.Station tmpStation = DalObject.GetStation(stationId);
                    BaseStation newStation = new();
                    newStation.Id = tmpStation.Id;
                    newStation.Name = tmpStation.Name;
                    newStation.Location = new();
                    newStation.Location.Latitude = tmpStation.Lat;
                    newStation.Location.Longitude = tmpStation.Lng;
                    newStation.NumFreeChargers = tmpStation.FreeChargeSlots;
                    newStation.DronesInCharging = getDronesInChraging(tmpStation.Id);
                    return newStation;
                
            }
            catch (DO.IdNotFoundException ex)
            {
                throw new IdNotFoundException(ex.Message, ex.Id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStationToList> GetAllStations()
        {
            List<BaseStationToList> stations = new();
            foreach (DO.Station oldStation in DalObject.GetAllStations())
            {
                BaseStationToList newStation = new();
                newStation.Id = oldStation.Id;
                newStation.Name = oldStation.Name;
                newStation.NumFreeChragers = oldStation.FreeChargeSlots;
                newStation.NumFullChragers = getDronesInChraging(oldStation.Id).Count;
                stations.Add(newStation);
            }
            return stations;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdateStation(int stationId, string name, string input)
        {
            try
            {
                lock (DalObject)
                {
                    BaseStation myStation = GetStation(stationId);
                    if (name == "")
                        name = myStation.Name;
                    int numChargers;
                    if (input == "")
                        numChargers = myStation.NumFreeChargers;
                    else
                        numChargers = int.Parse(input);
                    if (numChargers < myStation.DronesInCharging.Count)
                        throw new LessChargersThanDronesInCharchingException($"Too few chargers. you have {myStation.DronesInCharging.Count} drones in charging");
                    numChargers -= myStation.DronesInCharging.Count;

                    DalObject.UpdateStation(stationId, name, numChargers);
                }
            }
            catch (DO.IdNotFoundException ex)
            {
                throw new IdNotFoundException(ex.Message, ex.Id);
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStationToList> GetStationsWithFreeSlots()
        {
            List<BaseStationToList> filterdStations = new();
            foreach (DO.Station oldStation in DalObject.GetFilterdStations(station => station.FreeChargeSlots > 0))
            {
                BaseStationToList newStation = new();
                newStation.Id = oldStation.Id;
                newStation.Name = oldStation.Name;
                newStation.NumFreeChragers = oldStation.FreeChargeSlots;
                newStation.NumFullChragers = getDronesInChraging(newStation.Id).Count;
                filterdStations.Add(newStation);
            }
            return filterdStations;
        }

        /// <summary>
        /// returns all drones that are in charge in station
        /// </summary>
        /// <param name="stationId">the station ID</param>
        /// <returns>list of drones</returns>
        private List<DroneInCharging> getDronesInChraging(int stationId)
        {
            List<DroneInCharging> drones = new();
            foreach (DO.DroneCharge charger in DalObject.GetAllDroneCharge())
            {
                if (charger.Stationld == stationId)
                {
                    DroneInCharging drone = new();
                    drone.Id = charger.Droneld;
                    drone.Battery = GetDrone(charger.Droneld).Battery;
                    drones.Add(drone);
                }
            }
            return drones;
        }
    }
}
