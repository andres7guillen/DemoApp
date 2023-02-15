using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IEmployeeService
    {
        Task<Employee> Create(Employee employee);
        Task<EmployeeDTO> GetById(Guid id);
        Task<IEnumerable<EmployeeDTO>> GetAll();
        Task<Employee> Edit(Employee employee);
        Task<bool> Delete(Guid id);
    }
}
