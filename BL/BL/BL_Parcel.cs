using System;
using System.Collections.Generic;
using IBL.BO;

namespace BL
{
    public partial class BL
    {
        public void AddParcel(Parcel parcel)
        {
            parcel.DateCreated = DateTime.Now;
            parcel.DateScheduled = DateTime.MinValue;
            parcel.DatePickup = DateTime.MinValue;
            parcel.DateDeliverd = DateTime.MinValue;
            try
            {
                DalObject.AddParcel(parcel.Sender.Id, parcel.Target.Id, (IDAL.DO.WeightCategories)parcel.Weight, (IDAL.DO.Priorities)parcel.Prioritie, parcel.DateCreated, parcel.DateScheduled, parcel.DatePickup, parcel.DateDeliverd);
            }
            catch (IDAL.DO.IdAlreadyExistsException ex)
            {
                throw new IBL.BL.IdAlreadyExistsException(ex.Message, ex.Id);
            }
        }
        public Parcel GetParcerl(int parcelId)
        {
            try
            {
                IDAL.DO.Parcel tmpParcel = DalObject.GetParcerl(parcelId);
                Parcel newParcel = new();
                newParcel.Id = tmpParcel.Id;
                newParcel.Prioritie = (Priorities)tmpParcel.Priority;
                newParcel.Weight = (WeightCategories)tmpParcel.Weight;
                newParcel.DateCreated = tmpParcel.Requsted;
                newParcel.DateScheduled = tmpParcel.Scheduled;
                newParcel.DatePickup = tmpParcel.PickedUp;
                newParcel.DateDeliverd = tmpParcel.Delivered;

                newParcel.Target = new();
                newParcel.Target.Id = tmpParcel.TargetId;
                newParcel.Target.Name = DalObject.GetCustomer(tmpParcel.TargetId).Name;

                newParcel.Sender = new();
                newParcel.Sender.Id = tmpParcel.SenderId;
                newParcel.Sender.Name = DalObject.GetCustomer(tmpParcel.SenderId).Name;

                return newParcel;

            }
            catch (IDAL.DO.IdNotFoundException ex)
            {

                throw new IBL.BL.IdNotFoundException(ex.Message, ex.Id);
            }
        }

        public IEnumerable<ParcelToList> GetAllParcels()
        {
            List<ParcelToList> parcels = new();
            foreach (IDAL.DO.Parcel oldParcel in DalObject.GetAllParcels())
            {
                ParcelToList parcel = new();
                parcel.Id = oldParcel.Id;
                parcel.SenderName = GetCustomer(oldParcel.SenderId).Name;
                parcel.TargetName = GetCustomer(oldParcel.TargetId).Name;
                parcel.Priority = (Priorities)oldParcel.Priority;
                parcel.Weight = (WeightCategories)oldParcel.Weight;

                if (oldParcel.Requsted!=DateTime.MinValue)
                    parcel.Status = ParcelStatus.Created;

                if (oldParcel.Scheduled!=DateTime.MinValue)
                    parcel.Status = ParcelStatus.Scheduled;

                if (oldParcel.PickedUp!=DateTime.MinValue)
                    parcel.Status = ParcelStatus.PickUp;

                if (oldParcel.Delivered!=DateTime.MinValue)
                    parcel.Status = ParcelStatus.Deliverd;

                parcels.Add(parcel);
            }
            return parcels;
        }

        public IEnumerable<ParcelToList> GetUnassignedParcels()
        {
            List<ParcelToList> parcels = new();
            foreach (ParcelToList parcel in GetAllParcels())
                if (parcel.Id == 0)
                    parcels.Add(parcel);
            return parcels;
        }

    }
}
