using System;
using System.Collections.Generic;
using IBL.BO;

namespace BL
{
    public partial class BL
    {
        public IEnumerable<ParcelToList> GetAllParcels()
        {
            List<ParcelToList> parcels = new();
            foreach (IDAL.DO.Parcel oldParcel in DalObject.GetAllParcels())
            {
                ParcelToList parcel = new();
                parcel.Id = oldParcel.Id;
                parcel.Sender = GetCustomer(oldParcel.SenderId).Name;
                parcel.Receiver = GetCustomer(oldParcel.TargetId).Name;
                parcel.Prioritie = (Priorities)oldParcel.Priority;
                parcel.Weight = (WeightCategories)oldParcel.Weight;

                if (oldParcel.Requsted!=DateTime.MinValue)
                    parcel.StatusParcel = ParcelStatus.Created;

                if (oldParcel.Scheduled!=DateTime.MinValue)
                    parcel.StatusParcel = ParcelStatus.Scheduled;

                if (oldParcel.PickedUp!=DateTime.MinValue)
                    parcel.StatusParcel = ParcelStatus.PickUp;

                if (oldParcel.Delivered!=DateTime.MinValue)
                    parcel.StatusParcel = ParcelStatus.Deliverd;

                parcels.Add(parcel);
            }
            return parcels;
        }

        private List<ParcelInCustomer> getReceivedParcels(int customerId)
        {
            List<ParcelInCustomer> parcels = new();
            foreach (IDAL.DO.Parcel parcel in DalObject.GetAllParcels())
            {
                if (parcel.Delivered != DateTime.MinValue && parcel.TargetId == customerId)
                {
                    ParcelInCustomer parcelInCustomer = new();
                    parcelInCustomer.Id = parcel.Id;
                    parcelInCustomer.Prioritie = (Priorities)parcel.Priority;
                    parcelInCustomer.Weight = (WeightCategories)parcel.Weight;

                    if (parcel.Requsted != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.Created;

                    if (parcel.Scheduled != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.Scheduled;

                    if (parcel.PickedUp != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.PickUp;

                    if (parcel.Delivered != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.Deliverd;

                    parcels.Add(parcelInCustomer);
                }
            }
            return parcels;
        }
        private List<ParcelInCustomer> getWaitsToSendParcels(int customerId)
        {
            List<ParcelInCustomer> parcels = new();
            foreach (IDAL.DO.Parcel parcel in DalObject.GetAllParcels())
            {
                if (parcel.PickedUp == DateTime.MinValue && parcel.SenderId == customerId)
                {
                    ParcelInCustomer parcelInCustomer = new();
                    parcelInCustomer.Id = parcel.Id;
                    parcelInCustomer.Prioritie = (Priorities)parcel.Priority;
                    parcelInCustomer.Weight = (WeightCategories)parcel.Weight;

                    if (parcel.Requsted != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.Created;

                    if (parcel.Scheduled != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.Scheduled;

                    if (parcel.PickedUp != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.PickUp;

                    if (parcel.Delivered != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.Deliverd;

                    parcels.Add(parcelInCustomer);
                }
            }
            return parcels;
        }
        private List<ParcelInCustomer> getDeliverdParcels(int customerId)
        {
            List<ParcelInCustomer> parcels = new();
            foreach (IDAL.DO.Parcel parcel in DalObject.GetAllParcels())
            {
                if (parcel.Delivered != DateTime.MinValue && parcel.SenderId == customerId)
                {
                    ParcelInCustomer parcelInCustomer = new();
                    parcelInCustomer.Id = parcel.Id;
                    parcelInCustomer.Prioritie = (Priorities)parcel.Priority;
                    parcelInCustomer.Weight = (WeightCategories)parcel.Weight;

                    if (parcel.Requsted != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.Created;

                    if (parcel.Scheduled != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.Scheduled;

                    if (parcel.PickedUp != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.PickUp;

                    if (parcel.Delivered != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.Deliverd;

                    parcels.Add(parcelInCustomer);
                }
            }
            return parcels;
        }
        private List<ParcelInCustomer> getSentParcels(int customerId)
        {
            List<ParcelInCustomer> parcels = new();
            foreach (IDAL.DO.Parcel parcel in DalObject.GetAllParcels())
            {
                if (parcel.Delivered == DateTime.MinValue && parcel.SenderId == customerId)
                {
                    ParcelInCustomer parcelInCustomer = new();
                    parcelInCustomer.Id = parcel.Id;
                    parcelInCustomer.Prioritie = (Priorities)parcel.Priority;
                    parcelInCustomer.Weight = (WeightCategories)parcel.Weight;

                    if (parcel.Requsted != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.Created;

                    if (parcel.Scheduled != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.Scheduled;

                    if (parcel.PickedUp != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.PickUp;

                    if (parcel.Delivered != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.Deliverd;

                    parcels.Add(parcelInCustomer);
                }
            }
            return parcels;
        }
        private List<ParcelInCustomer> getInProccesToHimParcels(int customerId)
        {
            List<ParcelInCustomer> parcels = new();
            foreach (IDAL.DO.Parcel parcel in DalObject.GetAllParcels())
            {
                if (parcel.Delivered != DateTime.MinValue && parcel.TargetId == customerId)
                {
                    ParcelInCustomer parcelInCustomer = new();
                    parcelInCustomer.Id = parcel.Id;
                    parcelInCustomer.Prioritie = (Priorities)parcel.Priority;
                    parcelInCustomer.Weight = (WeightCategories)parcel.Weight;

                    if (parcel.Requsted != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.Created;

                    if (parcel.Scheduled != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.Scheduled;

                    if (parcel.PickedUp != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.PickUp;

                    if (parcel.Delivered != DateTime.MinValue)
                        parcelInCustomer.StatusParcel = ParcelStatus.Deliverd;

                    parcels.Add(parcelInCustomer);
                }
            }
            return parcels;
        }

    }
}
