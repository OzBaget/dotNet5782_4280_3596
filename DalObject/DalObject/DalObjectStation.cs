using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Dal
{
    internal sealed partial class DalObject : DalApi.IDal
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int stationId)
        {
            bool stationExists = false;

            foreach (Station station in GetAllStations())
                if (station.Id == stationId && station.IsActived)
                    stationExists = true;

            if (!stationExists)
                throw new IdNotFoundException($"Can't find station with ID #{stationId}", stationId);

            return DataSource.BaseStations.Find(station => station.Id == stationId);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(int id, string name, double lat, double lng, int chargSlots)
        {
            foreach (Station station in DataSource.BaseStations)
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
            DataSource.BaseStations.Add(myStation);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(int id)
        {
            DataSource.BaseStations.Remove(GetStation(id));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int stationId, string name, int numChargers)
        {
            Station tmpStation = GetStation(stationId);
            DeleteStation(stationId);
            AddStation(tmpStation.Id, name, tmpStation.Lat, tmpStation.Lng, numChargers);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetAllStations()
        {
            return DataSource.BaseStations.Where(s => s.IsActived);
        }

        /// <summary>
        /// get all base stations that has free cahrge slots
        /// </summary>
        /// <returns>array of all the base stations that has free charge slots</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetFilterdStations(Predicate<Station> filter)
        {
            return DataSource.BaseStations.Where(s => filter(s) && s.IsActived);
        }
    }
}
