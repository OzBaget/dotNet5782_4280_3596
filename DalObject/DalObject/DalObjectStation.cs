using System;
using System.Collections.Generic;
using DO;

namespace Dal
{
    public partial class DalObject
    {
        
        public Station GetStation(int stationId)
        {
            bool stationExists = false;

            foreach (Station station in GetAllStations())
                if (station.Id == stationId)
                    stationExists = true;
            
            if (!stationExists)
                throw new IdNotFoundException($"Can't find station with ID #{stationId}", stationId);

            return DataSource.BaseStations.Find(station => station.Id == stationId);
        }

       
        public void AddStation(int id,string name, double lat, double lng, int chargSlots)
        {
            foreach (Station station in DataSource.BaseStations)
            {
                if (station.Id==id)
                {
                    throw new IdAlreadyExistsException($"Station with ID #{id} already exists!", id);
                }
            }
            DataSource.BaseStations.Add(new Station(id, name, lat, lng, chargSlots));
        }
        public void DeleteStation(int id)
        {
            DataSource.BaseStations.Remove(GetStation(id));
        }
        public void UpdateStation(int stationId, string name, int numChargers)
        {
            Station tmpStation = GetStation(stationId);
            DeleteStation(stationId);
            AddStation(tmpStation.Id, name, tmpStation.Lat, tmpStation.Lng, numChargers);
        }

        
        public IEnumerable<Station> GetAllStations()
        {
            return new List<Station>(DataSource.BaseStations);
        }

        /// <summary>
        /// get all base stations that has free cahrge slots
        /// </summary>
        /// <returns>array of all the base stations that has free charge slots</returns>
        public IEnumerable<Station> GetFilterdStations(Predicate<Station> filter)
        {
            return DataSource.BaseStations.FindAll(filter);
        }
    }
}
