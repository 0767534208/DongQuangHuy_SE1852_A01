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
    public class EmployeeViewModel : BaseViewModel
    {
        private readonly EmployeeService _employeeService;
        private Employee _selectedEmployee;
        private string _searchTerm;
        private List<Employee> _allEmployees;

        public EmployeeViewModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
            _allEmployees = _employeeService.GetAllEmployees();
            Employees = new List<Employee>(_allEmployees);

            AddEmployeeCommand = new ReplyCommand(AddEmployee);
            UpdateEmployeeCommand = new ReplyCommand(UpdateEmployee);
            DeleteEmployeeCommand = new ReplyCommand(DeleteEmployee);
            SearchCommand = new ReplyCommand(SearchEmployees);
        }

        public List<Employee> Employees { get; private set; }
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set => SetField(ref _selectedEmployee, value);
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                SetField(ref _searchTerm, value);
                SearchEmployees();
            }
        }

        public ICommand AddEmployeeCommand { get; }
        public ICommand UpdateEmployeeCommand { get; }
        public ICommand DeleteEmployeeCommand { get; }
        public ICommand SearchCommand { get; }

        public void AddEmployee(object parameter)
        {
            var newEmployee = new Employee();
            _employeeService.AddEmployee(newEmployee);
            Employees.Add(newEmployee);
            SelectedEmployee = newEmployee;
        }

        public void UpdateEmployee(object parameter)
        {
            if (SelectedEmployee != null)
            {
                _employeeService.UpdateEmployee(SelectedEmployee);
            }
        }

        public void DeleteEmployee(object parameter)
        {
            if (SelectedEmployee != null)
            {
                _employeeService.DeleteEmployee(SelectedEmployee.EmployeeID);
                Employees.Remove(SelectedEmployee);
                SelectedEmployee = null;
            }
        }

        public void SearchEmployees(object parameter = null)
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                Employees = new List<Employee>(_allEmployees);
            }
            else
            {
                Employees = _allEmployees
                    .Where(e => e.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                               e.EmployeeID.ToString().Contains(SearchTerm) ||
                               e.JobTitle.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            OnPropertyChanged(nameof(Employees));
        }
    }
}
