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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository;
        }
        public async Task<Company> Create(Company Company) => await _repository.Create(Company);

        public async Task<bool> Delete(Guid id) => await _repository.Delete(id);

        public async Task<Company> Edit(Company Company) => await _repository.Edit(Company);

        public async Task<List<Company>> GetAll() => _repository.GetAll().ToList();

        public async Task<Company> GetById(Guid id) => _repository.GetById(id).FirstOrDefault();
    }
}
