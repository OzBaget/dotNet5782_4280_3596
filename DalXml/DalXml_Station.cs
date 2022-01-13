using DO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dal
{
    internal sealed partial class DalXml : DalApi.IDal
    {

        public Station GetStation(int stationId)
        {
            bool stationExists = false;
            List<Station> myList = loadXmlToList<Station>();
            foreach (Station station in myList)
                if (station.Id == stationId && station.IsActived)
                    stationExists = true;

            if (!stationExists)
                throw new IdNotFoundException($"Can't find station with ID #{stationId}", stationId);

            return myList.Find(station => station.Id == stationId);
        }


        public void AddStation(int id, string name, double lat, double lng, int chargSlots)
        {
            List<Station> myList = loadXmlToList<Station>();

            foreach (Station station in myList)
            {
                if (station.Id == id)
                {
                    throw new IdAlreadyExistsException($"Station with ID #{id} already exists!", id);
                }
            }
            Station myStation = new();
            myStation.Id = id;
            myStation.Name = name;
            myStation.Lat = lat;
            myStation.Lng = lng;
            myStation.FreeChargeSlots = chargSlots;
            myStation.IsActived = true;
            myList.Add(myStation);
            saveListToXml(myList);
        }
        public void DeleteStation(int id)
        {
            List<Station> myList = loadXmlToList<Station>();
            int index = myList.IndexOf(GetStation(id));
            Station myStation = myList[index];
            myStation.IsActived = false;
            myList[index] = myStation;
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
            return loadXmlToList<Station>().Where(s => s.IsActived);
        }

        /// <summary>
        /// get all base stations that has free cahrge slots
        /// </summary>
        /// <returns>array of all the base stations that has free charge slots</returns>
        public IEnumerable<Station> GetFilterdStations(Predicate<Station> filter)
        {
            return loadXmlToList<Station>().Where(s => filter(s) && s.IsActived);
        }
    }
}
