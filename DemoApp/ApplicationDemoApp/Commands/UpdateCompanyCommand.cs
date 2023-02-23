using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDemoApp.Commands
{
    public class UpdateCompanyCommand : IRequest<Company>
    {
        public Company Company { get; set; }
    }

    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Company> 
    {
        private readonly ICompanyRepository _companyRepository;

        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Company> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            return await _companyRepository.Edit(request.Company);
        }
    }

}
