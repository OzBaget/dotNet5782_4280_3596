using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BL;
using IBL.BO;

namespace BL
{
    public partial class BL
    {
        public void AddStation(BaseStation station)
        {
            try
            {
                DalObject.AddStation(station.Id, station.Name, station.Location.Latitude, station.Location.Longitude, station.NumFreeChargers);
            }
            catch (IDAL.DO.IdAlreadyExistsException ex)
            {
                throw new IBL.BL.IdAlreadyExistsException(ex.Message, ex.Id);
            }
        }
        public BaseStation GetStation(int stationId)
        {
            try
            {
                IDAL.DO.Station tmpStation= DalObject.GetStation(stationId);
                BaseStation newStation = new();
                newStation.Id = tmpStation.Id;
                newStation.Name = tmpStation.Name;
                newStation.Location = new();
                newStation.Location.Latitude=tmpStation.Lat;
                newStation.Location.Longitude=tmpStation.Lng;
                newStation.NumFreeChargers = tmpStation.FreeChargeSlots;
                newStation.DronesInCharging = getDronesInChraging(tmpStation.Id);
                return newStation;
            }catch(IDAL.DO.IdNotFoundException ex)
            {
                throw new IBL.BL.IdNotFoundException(ex.Message, ex.Id);
            }
        }
        public IEnumerable<BaseStationToList> GetAllStations()
        {
            List<BaseStationToList> stations = new();
            foreach (IDAL.DO.Station oldStation in DalObject.GetAllStations())
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
        public void UpdateStation(int stationId, string name, string input)
        {
            try
            {
                if (name == "")
                    name = GetStation(stationId).Name;
                int numChargers;
                if (input == "")
                    numChargers = GetStation(stationId).NumFreeChargers;
                else
                    numChargers = int.Parse(input);
                if (numChargers < GetStation(stationId).DronesInCharging.Count)
                    throw new LessChargersThanDronesInCharchingException($"Too few chargers. you have {GetStation(stationId).DronesInCharging.Count} drones in charging");
                numChargers -= GetStation(stationId).DronesInCharging.Count;

                DalObject.UpdateStation(stationId, name, numChargers);
            }
            catch (IDAL.DO.IdNotFoundException ex)
            {
                throw new IBL.BL.IdNotFoundException(ex.Message, ex.Id);
            }
                catch (LessChargersThanDronesInCharchingException ex)
            {
                throw new IBL.BL.LessChargersThanDronesInCharchingException(ex.Message);
            }
        }
        public IEnumerable<BaseStationToList> GetStationsWithFreeSlots()
        {
            return GetAllStations().Where(station => station.NumFreeChragers > 0);
        }

        private List<DroneInCharging> getDronesInChraging(int stationId)
        {
            List<DroneInCharging> drones = new();
            foreach (IDAL.DO.DroneCharge charger in DalObject.GetAllDroneCharge())
            {
                if (charger.Stationld == stationId) 
                {
                    DroneInCharging drone=new();
                    drone.Id = charger.Droneld;
                    drone.Battery = GetDrone(charger.Droneld).Battery;
                    drones.Add(drone);
                } 
            }
        return drones;
        }


    }
}
