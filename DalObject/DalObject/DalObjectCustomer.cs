using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DO;


namespace Dal
{
    sealed partial class DalObject : DalApi.IDal
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int id,string name, string phone, double lat, double lng)
        {
            bool customerExists = false;

            foreach (Customer customer in DataSource.Customers)
                if (customer.Id == id)
                    customerExists = true;

            if (customerExists)
                throw new IdAlreadyExistsException($"Customer with ID #{id} already exists!", id);

            DataSource.Customers.Add(new Customer(id, name, phone, lat, lng));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int customerId)
        {
            DataSource.Customers.Remove(GetCustomer(customerId));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int customerId, string name, string phone)
        {
            Customer tmpCustomer= GetCustomer(customerId);
            DeleteCustomer(customerId);
            AddCustomer(tmpCustomer.Id, name, phone, tmpCustomer.Lat, tmpCustomer.Lng);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return new List<Customer>(DataSource.Customers);
        }
    }
}
