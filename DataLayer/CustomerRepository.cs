using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CustomerRepository
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context)
        {
            _context = context;
        }

        public List<Customer> GetAll() => _context.Customers;

        public Customer GetById(string id) => _context.Customers.FirstOrDefault(c => c.CustomerID == id);

        public void Add(Customer customer) => _context.Customers.Add(customer);

        public void Update(Customer customer)
        {
            var existingCustomer = GetById(customer.CustomerID);
            if (existingCustomer != null)
            {
                existingCustomer.CompanyName = customer.CompanyName;
                existingCustomer.ContactName = customer.ContactName;
                existingCustomer.ContactTitle = customer.ContactTitle;
                existingCustomer.Address = customer.Address;
                existingCustomer.Phone = customer.Phone;
            }
        }

        public void Delete(string id)
        {
            var customer = GetById(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
        }
    }
}
