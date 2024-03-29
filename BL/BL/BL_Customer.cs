﻿using BlApi;
using BO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BL
{
    internal sealed partial class BL : IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer customer)
        {
            try
            {
                DalObject.AddCustomer(customer.Id, customer.Name, customer.Phone, customer.Location.Latitude, customer.Location.Longitude);
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new IdAlreadyExistsException(ex.Message, ex.Id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int customerId)
        {
            try
            {
                //lock (DalObject)
                {
                    DO.Customer tmpCustomer = DalObject.GetCustomer(customerId);
                    Customer newCustomer = new();
                    newCustomer.Id = tmpCustomer.Id;
                    newCustomer.Name = tmpCustomer.Name;
                    newCustomer.Phone = tmpCustomer.Phone;
                    newCustomer.Location = new();
                    newCustomer.Location.Latitude = tmpCustomer.Lat;
                    newCustomer.Location.Longitude = tmpCustomer.Lng;
                    newCustomer.ReceivedParcels = new();
                    newCustomer.SentParcels = new();
                    //update ReceivedParcels and WaitsToSendParcels
                    foreach (var parcel in DalObject.GetAllParcels())
                    {
                        if (parcel.Delivered != null && parcel.TargetId == tmpCustomer.Id)
                        {
                            ParcelInCustomer parcelInCustomer = new();
                            parcelInCustomer.Id = parcel.Id;
                            parcelInCustomer.Prioritie = (Priorities)parcel.Priority;
                            parcelInCustomer.Weight = (WeightCategories)parcel.Weight;
                            parcelInCustomer.StatusParcel = ParcelStatus.Deliverd;
                            CustomerInParcel customer = new();
                            customer.Id = parcel.SenderId;
                            customer.Name = DalObject.GetCustomer(parcel.SenderId).Name;
                            parcelInCustomer.Customer = customer;
                            newCustomer.ReceivedParcels.Add(parcelInCustomer);
                        }

                        if (parcel.PickedUp == null && parcel.SenderId == tmpCustomer.Id)
                        {
                            ParcelInCustomer parcelInCustomer = new();
                            parcelInCustomer.Id = parcel.Id;
                            parcelInCustomer.Prioritie = (Priorities)parcel.Priority;
                            parcelInCustomer.Weight = (WeightCategories)parcel.Weight;

                            if (parcel.Scheduled != null)
                                parcelInCustomer.StatusParcel = ParcelStatus.Scheduled;
                            else
                                parcelInCustomer.StatusParcel = ParcelStatus.Created;

                            CustomerInParcel customer = new();
                            customer.Id = parcel.TargetId;
                            customer.Name = DalObject.GetCustomer(parcel.TargetId).Name;
                            parcelInCustomer.Customer = customer;
                            newCustomer.SentParcels.Add(parcelInCustomer);
                        }
                    }
                    return newCustomer;
                }
            }
            catch (DO.IdNotFoundException ex)
            {
                throw new IdNotFoundException(ex.Message, ex.Id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetAllCustomers()
        {
            lock (DalObject)
            {
                List<CustomerToList> customers = new();
                foreach (DO.Customer oldCustomer in DalObject.GetAllCustomers())
                {
                    CustomerToList newCustomer = new();
                    newCustomer.Id = oldCustomer.Id;
                    newCustomer.Name = oldCustomer.Name;
                    newCustomer.Phone = oldCustomer.Phone;
                    newCustomer.ParcelsDelivered = 0;
                    newCustomer.ParcelsSent = 0;
                    newCustomer.ParcelsReceived = 0;
                    newCustomer.ParcelsInProccesToHim = 0;

                    foreach (var parcel in DalObject.GetAllParcels())
                    {
                        if (parcel.SenderId == newCustomer.Id && parcel.Delivered != null)
                            newCustomer.ParcelsDelivered++;
                        if (parcel.SenderId == newCustomer.Id && parcel.Delivered == null)
                            newCustomer.ParcelsSent++;
                        if (parcel.TargetId == newCustomer.Id && parcel.Delivered != null)
                            newCustomer.ParcelsReceived++;
                        if (parcel.TargetId == newCustomer.Id && parcel.Delivered == null)
                            newCustomer.ParcelsInProccesToHim++;
                    }
                    customers.Add(newCustomer);
                }
                return customers;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int customerId, string name, string phone)
        {
            try
            {
                if (name == "")
                    name = GetCustomer(customerId).Name;
                if (phone == "")
                    phone = GetCustomer(customerId).Phone;
                DalObject.UpdateCustomer(customerId, name, phone);
            }
            catch (DO.IdNotFoundException ex)
            {
                throw new IdNotFoundException(ex.Message, ex.Id);
            }
        }
    }
}
