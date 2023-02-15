using Data.Context;
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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DemoContext _context;
        public CompanyRepository(DemoContext context)
        {
            _context = context;
        }
        public async Task<Company> Create(Company Company)
        {
            Company.Id = Guid.NewGuid();
            await _context.Companies.AddAsync(Company);
            await _context.SaveChangesAsync();
            return Company;
        }

        public async Task<bool> Delete(Guid id)
        {
            var Company = await _context.Companies.FirstOrDefaultAsync(e => e.Id == id);
            if (Company == null)
                return false;
            _context.Companies.Remove(Company);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Company> Edit(Company Company)
        {
            _context.Companies.Update(Company);
            await _context.SaveChangesAsync();
            return Company;
        }

        public IQueryable<Company> GetAll()
        {
            return _context.Companies
                .Include(c => c.Employees)
                .AsQueryable();
        }

        public IQueryable<Company> GetById(Guid id) => _context.Companies
                .Include(c => c.Employees)
                .Where(e => e.Id == id).AsQueryable();
    }
}
