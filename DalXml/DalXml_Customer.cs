using System;
using System.Collections.Generic;
using DO;


namespace Dal
{
    sealed partial class DalXml : DalApi.IDal
    {
        public Customer GetCustomer(int customerId)
        {
            bool customerExists = false;

            foreach (Customer customer in loadXmlToList<Customer>())
                if (customer.Id == customerId)
                    customerExists = true;

            if (!customerExists)
                throw new IdNotFoundException($"Can't find customer with ID #{customerId}", customerId);

            return loadXmlToList<Customer>().Find(customer => customer.Id == customerId);
        }
        
        public void AddCustomer(int id,string name, string phone, double lat, double lng)
        {
            bool customerExists = false;
            List<Customer> myList = loadXmlToList<Customer>();
            foreach (Customer customer in myList)
                if (customer.Id == id)
                    customerExists = true;

            if (customerExists)
                throw new IdAlreadyExistsException($"Customer with ID #{id} already exists!", id);
            myList.Add(new Customer(id, name, phone, lat, lng));
            saveListToXml(myList);
        }

        public void DeleteCustomer(int customerId)
        {
            List<Customer> myList = loadXmlToList<Customer>();
            myList.Remove(GetCustomer(customerId));
            saveListToXml(myList);
        }

        public void UpdateCustomer(int customerId, string name, string phone)
        {
            Customer tmpCustomer= GetCustomer(customerId);
            DeleteCustomer(customerId);
            AddCustomer(tmpCustomer.Id, name, phone, tmpCustomer.Lat, tmpCustomer.Lng);
        }
        
        public IEnumerable<Customer> GetAllCustomers()
        {
            return loadXmlToList<Customer>();
        }
    }
}
