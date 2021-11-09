using System.Collections.Generic;
using IDAL.DO;
using IDAL;
using System;
namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// returns parcel by ID
        /// </summary>
        /// <param name="parcelId"> the parcel ID</param>
        /// <returns>Parcel object of the requsted ID (by value)</returns>
        public Parcel GetParcerl(int parcelId)
        {
            return DataSource.Parcels.Find(parcel => parcel.Id == parcelId);
        }

        /// <summary>
        /// Add parcel to Parcels list in DataSource
        /// </summary>
        /// <param name="senderId">sender customer ID</param>
        /// <param name="targetId">target customer ID</param>
        /// <param name="weightInt">the weight of the parcel (0/1/2)</param>
        /// <param name="priorityInt">the priority of the parcel (0/1/2)</param>
        public int AddParcel(int senderId, int targetId, int weightInt, int priorityInt)
        {
            Parcel myParcel = new Parcel(
                ++DataSource.Config.ParcelId, //update Config.parcelId
                senderId,
                targetId,
                (WeightCategories)weightInt,
                (Priorities)priorityInt);
            DataSource.Parcels.Add(myParcel);
            return DataSource.Config.ParcelId;
        }

        /// <summary>
        /// link parcel to drone
        /// </summary>
        /// <param name="parcelId">the parcel ID</param>
        /// <param name="droneId">the drone ID</param>
        public void linkParcel(int parcelId, int droneId)
        {
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = DataSource.Parcels.IndexOf(parcelTmp);
            parcelTmp.DroneId = droneId;
            parcelTmp.Scheduled = DateTime.Now;
            DataSource.Parcels[index] = parcelTmp;

            Drone droneTmp = GetDrone(droneId);
            index = DataSource.Drones.IndexOf(droneTmp);
            DataSource.Drones[index] = droneTmp;
        }

        /// <summary>
        /// update parcel pickUp time to the current time
        /// </summary>
        /// <param name="parcelId">the parcel ID to update</param>
        public void PickParcel(int parcelId)
        {
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = DataSource.Parcels.IndexOf(parcelTmp);
            parcelTmp.PickedUp = DateTime.Now;
            DataSource.Parcels[index] = parcelTmp;
        }

        /// <summary>
        /// update parcel Deliverd time to the current time
        /// </summary>
        /// <param name="parcelId">the parcel ID to update</param>
        public void ParcelToCustomer(int parcelId)
        {
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = DataSource.Parcels.IndexOf(parcelTmp);
            parcelTmp.Delivered = DateTime.Now;
            int droneId = parcelTmp.DroneId;
            DataSource.Parcels[index] = parcelTmp;

            Drone droneTmp = GetDrone(droneId);
            index = DataSource.Drones.IndexOf(droneTmp);
            DataSource.Drones[index] = droneTmp;
        }

        /// <summary>
        /// get array of all parcels
        /// </summary>
        /// <returns>array of all parcels</returns>
        public IEnumerable<Parcel> GetAllParcels()
        {
            return new List<Parcel>(DataSource.Parcels);
        }

        /// <summary>
        /// get array of all unassigned parcels
        /// </summary>
        /// <returns>array of parcels</returns>
        public IEnumerable<Parcel> GetUnassignedParcels()
        {
            return DataSource.Parcels.FindAll(parcel => parcel.DroneId == 0);
        }
    }
}
