using DO;
using System.Collections.Generic;
using System.Linq;


namespace Dal
{
    internal sealed partial class DalXml : DalApi.IDal
    {
        public Customer GetCustomer(int customerId)
        {
            bool customerExists = false;

            foreach (Customer customer in loadXmlToList<Customer>())
                if (customer.Id == customerId && customer.IsActived)
                    customerExists = true;

            if (!customerExists)
                throw new IdNotFoundException($"Can't find customer with ID #{customerId}", customerId);

            return loadXmlToList<Customer>().Find(customer => customer.Id == customerId);
        }

        public void AddCustomer(int id, string name, string phone, double lat, double lng)
        {
            bool customerExists = false;
            List<Customer> myList = loadXmlToList<Customer>();
            foreach (Customer customer in myList)
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
            myList.Add(myCustomer);
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
            Customer tmpCustomer = GetCustomer(customerId);
            DeleteCustomer(customerId);
            AddCustomer(tmpCustomer.Id, name, phone, tmpCustomer.Lat, tmpCustomer.Lng);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return loadXmlToList<Customer>().Where(c => c.IsActived);
        }
    }
}
