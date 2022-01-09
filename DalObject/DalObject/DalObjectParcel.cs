using System.Collections.Generic;
using DO;
using System;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Dal
{
    sealed partial class DalObject : DalApi.IDal
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcerl(int parcelId)
        {
            bool parcelExists = false;

            foreach (Parcel parcel in DataSource.Parcels)
                if (parcel.Id == parcelId && parcel.IsActived)
                    parcelExists = true;

            if (!parcelExists)
                throw new IdNotFoundException($"Can't find parcel with ID #{parcelId}", parcelId);

            return DataSource.Parcels.Find(parcel => parcel.Id == parcelId);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority, DateTime? requsted, DateTime? scheduled, DateTime? pickedUp, DateTime? delivered)
        {
            bool customerExists = false;
            foreach (Customer customer in DataSource.Customers)
                if (customer.Id == senderId)
                    customerExists = true;
            if (!customerExists)
                throw new IdNotFoundException($"Can't find sender (#{senderId}", senderId);

            customerExists = false;
            foreach (Customer customer in DataSource.Customers)
                if (customer.Id == targetId)
                    customerExists = true;
            if (!customerExists)
                throw new IdNotFoundException($"Can't find target (#{targetId}", targetId);


            Parcel myParcel = new();
            myParcel.Id = ++DataSource.Config.ParcelId;
            myParcel.SenderId = senderId;
            myParcel.TargetId = targetId;
            myParcel.Weight = weight;
            myParcel.Priority = priority;
            myParcel.Requsted = requsted;
            myParcel.Scheduled = scheduled;
            myParcel.PickedUp = pickedUp;
            myParcel.Delivered = delivered;
            myParcel.IsActived = true;
            DataSource.Parcels.Add(myParcel);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int parcelId)
        {
            Parcel myParcel = GetParcerl(parcelId);
            int parcelIndex = DataSource.Parcels.IndexOf(myParcel);
            myParcel.IsActived = false;
            DataSource.Parcels[parcelIndex] = myParcel;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void linkParcel(int parcelId, int droneId)
        {
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = DataSource.Parcels.IndexOf(parcelTmp);
            parcelTmp.DroneId = droneId;
            parcelTmp.Scheduled = DateTime.Now;
            DataSource.Parcels[index] = parcelTmp;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PickParcel(int parcelId)
        {
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = DataSource.Parcels.IndexOf(parcelTmp);
            parcelTmp.PickedUp = DateTime.Now;
            DataSource.Parcels[index] = parcelTmp;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ParcelToCustomer(int parcelId)
        {
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = DataSource.Parcels.IndexOf(parcelTmp);
            parcelTmp.Delivered = DateTime.Now;
            int droneId = parcelTmp.DroneId;
            parcelTmp.DroneId = 0;
            DataSource.Parcels[index] = parcelTmp;

            Drone droneTmp = GetDrone(droneId);
            index = DataSource.Drones.IndexOf(droneTmp);
            DataSource.Drones[index] = droneTmp;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetAllParcels()
        {
            return new List<Parcel>(DataSource.Parcels.Where(p => p.IsActived));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetFilterdParcels(Predicate<Parcel> filter)
        {
            return DataSource.Parcels.Where(p => filter(p) && p.IsActived);
        }
    }
}
