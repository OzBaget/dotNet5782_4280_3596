using System;
using System.Collections.Generic;
using IDAL.DO;
namespace DalObject
{
    public partial class DalObject
    {
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
        
        public void AddCustomer(int id,string name, string phone, double lat, double lng,Permissions permission)
        {
            bool customerExists = false;

            foreach (Customer customer in DataSource.Customers)
                if (customer.Id == id)
                    customerExists = true;

            if (customerExists)
                throw new IdAlreadyExistsException($"Customer with ID #{id} already exists!", id);

            DataSource.Customers.Add(new Customer(id, name, phone, lat, lng,permission));
        }

        public void DeleteCustomer(int customerId)
        {
            DataSource.Customers.Remove(GetCustomer(customerId));
        }

        public void UpdateCustomer(int customerId, string name, string phone)
        {
            Customer tmpCustomer= GetCustomer(customerId);
            DeleteCustomer(customerId);
            AddCustomer(tmpCustomer.Id, name, phone, tmpCustomer.Lat, tmpCustomer.Lng,tmpCustomer.Permission);
        }
        
        public IEnumerable<Customer> GetAllCustomers()
        {
            return new List<Customer>(DataSource.Customers);
        }
    }
}
