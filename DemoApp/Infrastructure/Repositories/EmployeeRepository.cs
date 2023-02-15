using Data.Context;
using Domain.DTO;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DemoContext _context;
        public EmployeeRepository(DemoContext context)
        {
            _context = context;
        }
        public async Task<Employee> Create(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> Delete(Guid id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
                return false;
            _context.Employees.Remove(employee);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Employee> Edit(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public IQueryable<Employee> GetAll()
        {
            return _context.Employees
                .Include(e => e.Company)
                .AsQueryable();
        }

        public IQueryable<Employee> GetById(Guid id)
        {
            return _context.Employees
                .Include(e => e.Company)
                .Where(e => e.Id == id).AsQueryable();
        }
    }
}
