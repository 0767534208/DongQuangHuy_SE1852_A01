using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DongQuangHuyWPF.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        private readonly CustomerService _customerService;
        private Customer? _selectedCustomer;
        private string _searchQuery = string.Empty;
        private List<Customer> _customers;

        public CustomerViewModel(CustomerService customerService)
        {
            _customerService = customerService;
            LoadCustomers();
            
            AddCustomerCommand = new ReplyCommand(param => { });
            UpdateCustomerCommand = new ReplyCommand(param => { });
            DeleteCustomerCommand = new ReplyCommand(param => {  });
        }

        public List<Customer> Customers
        {
            get => _customers;
            set => SetField(ref _customers, value);
        }

        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetField(ref _selectedCustomer, value);
        }

        public ICommand AddCustomerCommand { get; }
        public ICommand UpdateCustomerCommand { get; }
        public ICommand DeleteCustomerCommand { get; }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                SetField(ref _searchQuery, value);
                SearchCustomers();
            }
        }

        // Public method to add a customer, called from View's code-behind
        public void AddCustomer(Customer newCustomer)
        {
            if (newCustomer != null)
            {
                _customerService.AddCustomer(newCustomer);
                LoadCustomers();
                SelectedCustomer = newCustomer;
            }
        }

       
        public void UpdateCustomer(Customer updatedCustomer)
        {
            if (updatedCustomer != null)
            {
                _customerService.UpdateCustomer(updatedCustomer);
                LoadCustomers();
                SelectedCustomer = Customers.FirstOrDefault(c => c.CustomerID == updatedCustomer.CustomerID);
            }
        }

        
        public void DeleteCustomer(Customer customerToDelete)
        {
            if (customerToDelete != null)
            {
                _customerService.DeleteCustomer(customerToDelete.CustomerID);
                LoadCustomers();
                SelectedCustomer = null;
            }
        }

        
        private void LoadCustomers()
        {
            Customers = _customerService.GetAllCustomers().ToList();
            Console.WriteLine($"Loaded {Customers.Count} customers.");
        }

        private void SearchCustomers()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                LoadCustomers();
            }
            else
            {
                Customers = _customerService.GetAllCustomers()
                    .Where(c => c.CompanyName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                               c.Phone.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }
    }
}

