using System;
using System.Collections.Generic;
using IBL.BO;

namespace BL
{
    public partial class BL
    {
        public Customer GetCustomer(int customerId)
        {
            try
            {
                IDAL.DO.Customer tmpCustomer= DalObject.GetCustomer(customerId);
                Customer newCustomer = new();
                newCustomer.Id = tmpCustomer.Id;
                newCustomer.Name = tmpCustomer.Name;
                newCustomer.Phone = tmpCustomer.Phone;
                newCustomer.Location.Latitude = tmpCustomer.Lat;
                newCustomer.Location.Longitude = tmpCustomer.Lng;
                newCustomer.ReceivedParcels = getReceivedParcels(tmpCustomer.Id);
                newCustomer.WaitsToSendParcels = getWaitsToSendParcels(tmpCustomer.Id);
                return newCustomer;
            }
            catch(IDAL.DO.IdNotFoundException ex)
            {
                throw new IBL.BL.IdNotFoundException(ex.Message, ex.Id);
            }
        }


        public IEnumerable<CustomerToList> GetAllCustomers()
        {
            List<CustomerToList> customers = new();
            foreach (IDAL.DO.Customer oldCustomer in DalObject.GetAllCustomers())
            {
                CustomerToList newCustomer = new();
                newCustomer.Id = oldCustomer.Id;
                newCustomer.Name = oldCustomer.Name;
                newCustomer.Phone = oldCustomer.Phone;
                newCustomer.ParcelsDelivered = getDeliverdParcels(oldCustomer.Id).Count;
                newCustomer.ParcelsSent=getSentParcels(oldCustomer.Id).Count;
                newCustomer.ParcelsReceived = getReceivedParcels(oldCustomer.Id).Count;
                newCustomer.ParcelsInProccesToHim= getInProccesToHimParcels(oldCustomer.Id).Count;
                customers.Add(newCustomer);
            }
            return customers;
        }

        

        private Customer getCustomerByName(string name)
        {
            foreach (IDAL.DO.Customer customer in DalObject.GetAllCustomers())
            {
                if (customer.Name==name)
                {
                    return GetCustomer(customer.Id);
                }
            }
            throw new IBL.BL.IdNotFoundException($"Cannt find customer with name '{name}'", -1);
        }
    }
}
