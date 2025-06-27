using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<Customer> GetAllCustomers() => _customerRepository.GetAll();

        public Customer GetCustomerById(string id) => _customerRepository.GetById(id);

        public void AddCustomer(Customer customer)
        {
            _customerRepository.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.Update(customer);
        }

        public void DeleteCustomer(string id)
        {

            _customerRepository.Delete(id);
        }
    }
}
