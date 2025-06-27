using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class EmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public List<Employee> GetAll() => _context.Employees;

        public Employee GetById(string id) => _context.Employees.FirstOrDefault(e => e.EmployeeID == id);

        public void Add(Employee employee) => _context.Employees.Add(employee);

        public void Update(Employee employee)
        {
            var existingEmployee = GetById(employee.EmployeeID);
            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.UserName = employee.UserName;
                existingEmployee.Password = employee.Password;
                existingEmployee.JobTitle = employee.JobTitle;
            }
        }

        public void Delete(string id)
        {
            var employee = GetById(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
        }
    }
}
