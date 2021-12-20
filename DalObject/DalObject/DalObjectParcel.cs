using System.Collections.Generic;
using DO;
using System;

namespace Dal
{
    public partial class DalObject : DalApi.IDal
    {

        public Parcel GetParcerl(int parcelId)
        {
            bool parcelExists = false;

            foreach (Parcel parcel in DataSource.Parcels)
                if (parcel.Id == parcelId)
                    parcelExists = true;

            if (!parcelExists)
                throw new IdNotFoundException($"Can't find parcel with ID #{parcelId}", parcelId);

            return DataSource.Parcels.Find(parcel => parcel.Id == parcelId);
        }

        
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
            DataSource.Parcels.Add(myParcel);
        }

        public void DeleteParcel(int parcelId)
        {
            DataSource.Parcels.Remove(GetParcerl(parcelId));
        }

        
        public void linkParcel(int parcelId, int droneId)
        {
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = DataSource.Parcels.IndexOf(parcelTmp);
            parcelTmp.DroneId = droneId;
            parcelTmp.Scheduled = DateTime.Now;
            DataSource.Parcels[index] = parcelTmp;  
        }

        
        public void PickParcel(int parcelId)
        {
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = DataSource.Parcels.IndexOf(parcelTmp);
            parcelTmp.PickedUp = DateTime.Now;
            DataSource.Parcels[index] = parcelTmp;
        }

        
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

       
        public IEnumerable<Parcel> GetAllParcels()
        {
            return new List<Parcel>(DataSource.Parcels);
        }

        public IEnumerable<Parcel> GetFilterdParcels(Predicate<Parcel> filter)
        {
            return DataSource.Parcels.FindAll(filter);
        }
    }
}
