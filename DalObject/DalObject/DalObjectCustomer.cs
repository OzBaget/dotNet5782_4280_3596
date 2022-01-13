using DO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


namespace Dal
{
    internal sealed partial class DalObject : DalApi.IDal
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int customerId)
        {
            bool customerExists = false;

            foreach (Customer customer in DataSource.Customers)
                if (customer.Id == customerId && customer.IsActived)
                    customerExists = true;

            if (!customerExists)
                throw new IdNotFoundException($"Can't find customer with ID #{customerId}", customerId);

            return DataSource.Customers.Find(customer => customer.Id == customerId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int id, string name, string phone, double lat, double lng)
        {
            bool customerExists = false;

            foreach (Customer customer in DataSource.Customers)
                if (customer.Id == id)
                    customerExists = true;

            if (customerExists)
                throw new IdAlreadyExistsException($"Customer with ID #{id} already exists!", id);
            Customer myCustomer = new();
            myCustomer.Id = id;
            myCustomer.Name = name;
            myCustomer.Phone = phone;
            myCustomer.Lat = lat;
            myCustomer.Lng = lng;
            myCustomer.IsActived = true;
            DataSource.Customers.Add(myCustomer);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int customerId)
        {
            int index = DataSource.Customers.IndexOf(GetCustomer(customerId));
            Customer myCustomer = DataSource.Customers[index];
            myCustomer.IsActived = false;
            DataSource.Customers[index] = myCustomer;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int customerId, string name, string phone)
        {
            int index = DataSource.Customers.IndexOf(GetCustomer(customerId));
            Customer myCustomer = DataSource.Customers[index];
            myCustomer.Name = name;
            myCustomer.Phone = phone;
            DataSource.Customers[index] = myCustomer;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return DataSource.Customers.Where(c => c.IsActived);
        }
    }
}
