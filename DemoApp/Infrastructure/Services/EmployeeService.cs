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
            var employees = _repository.GetAll().ToList();
            return employees.Select(c => new EmployeeDTO()
            {
                Company = c.Company.Name,
                CompanyId = c.Company.Id,
                DateAdmission = c.DateAdmission,
                Name= c.Name,
                Id = c.Id
            }).ToList();
        }

        public async Task<EmployeeDTO> GetById(Guid id) {
            var employee = _repository.GetById(id).FirstOrDefault();
            return new EmployeeDTO()
            {
                Company = employee.Company.Name,
                DateAdmission = employee.DateAdmission,
                Name = employee.Name,
                Id = employee.Id,
                CompanyId = employee.Company.Id
            };
        }
    }
}
