using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> Create(Employee employee);
        IQueryable<Employee> GetById(Guid id);
        IQueryable<Employee> GetAll();
        Task<Employee> Edit(Employee employee);
        Task<bool> Delete(Guid id);
    }
}
