using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDemoApp.Queries
{
    public class GetCompanyByIdQuery : IRequest<Company>
    {
        public Guid Id { get; }

        public GetCompanyByIdQuery(Guid id)
        {
            Id = id;
        }

        public class GetCompanyByIdHandler : IRequestHandler<GetCompanyByIdQuery, Company>
        {
            private readonly ICompanyRepository _companyRepository;
            public GetCompanyByIdHandler(ICompanyRepository companyRepository)
            {
                _companyRepository = companyRepository;
            }

            public async Task<Company> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
            {
                return _companyRepository.GetById(request.Id).FirstOrDefault();
            }
        }

    }
}
