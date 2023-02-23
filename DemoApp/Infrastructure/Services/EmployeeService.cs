using Domain.DTO;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        public async Task<Employee> Create(Employee employee) => await _repository.Create(employee);

        public async Task<bool> Delete(Guid id) => await _repository.Delete(id);

        public async Task<Employee> Edit(Employee employee) => await _repository.Edit(employee);

        public async Task<IEnumerable<EmployeeDTO>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<EmployeeDTO> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }
    }
}
