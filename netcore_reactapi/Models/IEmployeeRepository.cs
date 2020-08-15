
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using netcore_reactapi.ViewModel;

namespace netcore_reactapi.Models
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeelst();

        Task<Employee> GetEmployee(int? Empid);

        Task<int> AddEmployee(Employee Employeeinfo);

        Task<int> DeleteEmployee(int? Empid);

        Task UpdateEmployee(Employee Employeeinfo);
    }
}