using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<Employee> GetAllEmployees() => _employeeRepository.GetAll();

        public Employee GetEmployeeById(string id) => _employeeRepository.GetById(id);

        public void AddEmployee(Employee employee)
        {
            _employeeRepository.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.Update(employee);
        }

        public void DeleteEmployee(string id)
        {
            _employeeRepository.Delete(id);
        }
    }
}
