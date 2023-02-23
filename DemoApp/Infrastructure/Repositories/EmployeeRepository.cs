using Dapper;
using Data.Context;
using Domain.DTO;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DemoContext _context;
        private readonly DapperContext _dapperContext;
        public EmployeeRepository(DemoContext context, DapperContext dapperContext)
        {
            _context = context;
            _dapperContext = dapperContext;
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

        public async Task<List<EmployeeDTO>> GetAll()
        {
            using (var connection = _dapperContext.CreateConnection()) 
            {
                var employees = await connection.QueryAsync<EmployeeDTO>(
                    sql: "usp_get_all_employees",
                    commandType: CommandType.StoredProcedure);
                return employees.ToList();
            }
        }

        public IQueryable<Employee> GetById(Guid id)
        {
            return _context.Employees
                .Include(e => e.Company)
                .Where(e => e.Id == id).AsQueryable();
        }
    }
}
