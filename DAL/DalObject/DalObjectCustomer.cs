using System;
using System.Collections.Generic;
using IDAL.DO;
namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// returns customer by ID
        /// </summary>
        /// <param name="customerId"> the customer ID</param>
        /// <returns>Customer object of the requsted ID (by value)</returns>
        public Customer GetCustomer(int customerId)
        {
            bool customerExists = false;

            foreach (Customer customer in DataSource.Customers)
                if (customer.Id == customerId)
                    customerExists = true;

            if (!customerExists)
                throw new IdNotFoundException($"Can't find customer with ID #{customerId}", customerId);

            return DataSource.Customers.Find(customer => customer.Id == customerId);
        }

        /// <summary>
        /// Add customer to Customers list in DataSource
        /// </summary>
        /// <param name="name">the name of the customer</param>
        /// <param name="phone">the phone of the customer</param>
        /// <param name="lat">the latitude of the customer</param>
        /// <param name="lng">the longitude of the customer</param>
        public void AddCustomer(int id,string name, string phone, double lat, double lng)
        {
            bool customerExists = false;

            foreach (Customer customer in DataSource.Customers)
                if (customer.Id == id)
                    customerExists = true;

            if (customerExists)
                throw new IdAlreadyExistsException($"Customer with ID #{id} already exists!", id);

            DataSource.Customers.Add(new Customer(name, phone, lat, lng));
        }

        public void DeleteCustomer(int customerId)
        {
            DataSource.Customers.Remove(GetCustomer(customerId));
        }

        public void UpdateCustomer(int customerId, string name, string phone)
        {
            Customer tmpCustomer= GetCustomer(customerId);
            DeleteCustomer(customerId);
            AddCustomer(tmpCustomer.Id, name, phone, tmpCustomer.Lat, tmpCustomer.Lng);
        }

        /// <summary>
        /// get array of all customers
        /// </summary>
        /// <returns>array of all customer</returns>
        public IEnumerable<Customer> GetAllCustomers()
        {
            return new List<Customer>(DataSource.Customers);
        }


    }
}
