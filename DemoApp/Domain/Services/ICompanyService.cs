using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ICompanyService
    {
        Task<Company> Create(Company Company);
        Task<Company> GetById(Guid id);
        Task<List<Company>> GetAll();
        Task<Company> Edit(Company Company);
        Task<bool> Delete(Guid id);
    }
}
