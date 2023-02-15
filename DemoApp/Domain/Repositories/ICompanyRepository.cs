using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company> Create(Company Company);
        IQueryable<Company> GetById(Guid id);
        IQueryable<Company> GetAll();
        Task<Company> Edit(Company Company);
        Task<bool> Delete(Guid id);
    }
}
