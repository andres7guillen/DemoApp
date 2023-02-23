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
    public class GetAllCompaniesQuery : IRequest<List<Company>>
    {
        public class GetAllCompaniesHandler : IRequestHandler<GetAllCompaniesQuery, List<Company>>
        {
            private ICompanyRepository _repository;

            public GetAllCompaniesHandler(ICompanyRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Company>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
            {
                return _repository.GetAll().ToList();
            }
        }
    }
}
