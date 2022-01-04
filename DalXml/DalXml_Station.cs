using System;
using System.Collections.Generic;
using DO;

namespace Dal
{
    sealed partial class DalXml : DalApi.IDal
    {

        public Station GetStation(int stationId)
        {
            bool stationExists = false;
            List<Station> myList = loadXmlToList<Station>();
            foreach (Station station in myList)
                if (station.Id == stationId)
                    stationExists = true;
            
            if (!stationExists)
                throw new IdNotFoundException($"Can't find station with ID #{stationId}", stationId);

            return myList.Find(station => station.Id == stationId);
        }

       
        public void AddStation(int id,string name, double lat, double lng, int chargSlots)
        {
            List<Station> myList = loadXmlToList<Station>();

            foreach (Station station in myList)
            {
                if (station.Id==id)
                {
                    throw new IdAlreadyExistsException($"Station with ID #{id} already exists!", id);
                }
            }
            myList.Add(new Station(id, name, lat, lng, chargSlots));
            saveListToXml(myList);
        }
        public void DeleteStation(int id)
        {
            List<Station> myList = loadXmlToList<Station>();
            myList.Remove(GetStation(id));
            saveListToXml(myList);
        }
        public void UpdateStation(int stationId, string name, int numChargers)
        {
            Station tmpStation = GetStation(stationId);
            DeleteStation(stationId);
            AddStation(tmpStation.Id, name, tmpStation.Lat, tmpStation.Lng, numChargers);
        }

        
        public IEnumerable<Station> GetAllStations()
        {
            return loadXmlToList<Station>();
        }

        /// <summary>
        /// get all base stations that has free cahrge slots
        /// </summary>
        /// <returns>array of all the base stations that has free charge slots</returns>
        public IEnumerable<Station> GetFilterdStations(Predicate<Station> filter)
        {
            return loadXmlToList<Station>().FindAll(filter);
        }
    }
}
