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
    public class CreateCompanyCommand : IRequest<Company>
    {
        public Company Company { get; set; }
    }

    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Company>
    {
        private readonly ICompanyRepository _companyRepository;
        public CreateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<Company> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            return await _companyRepository.Create(request.Company);
        }
    }
}
