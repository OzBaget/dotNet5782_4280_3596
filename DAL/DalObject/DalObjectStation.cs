using System.Collections.Generic;
using IDAL.DO;
namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// returns base station by ID
        /// </summary>
        /// <param name="stationId"> the base station ID</param>
        /// <returns>Return a Station object of the requsted ID (by value)</returns>
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

        /// <summary>
        /// Add base station to the BaseStations list in DataSource
        /// </summary>
        /// <param name="name">the name of the station</param>
        /// <param name="lat">the latitude of the station</param>
        /// <param name="lng">the longitude of the station</param>
        /// <param name="chargSlots">hw many charge slosts are in the station</param>
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

        /// <summary>
        /// get array of all base satations
        /// </summary>
        /// <returns>array of all base satations</returns>
        public IEnumerable<Station> GetAllStations()
        {
            return new List<Station>(DataSource.BaseStations);
        }

        /// <summary>
        /// get all base stations that has free cahrge slots
        /// </summary>
        /// <returns>array of all the base stations that has free charge slots</returns>
        public IEnumerable<Station> GetStationsWithFreeSlots()
        {
            return DataSource.BaseStations.FindAll(station => station.FreeChargeSlots != 0);
        }
    }
}
