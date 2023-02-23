
using ApplicationDemoApp.Commands;
using ApplicationDemoApp.Queries;
using AutoMapper;
using DemoApi.Converts;
using DemoApi.Models;
using Domain.Entities;
using Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CompanyController(ICompanyService service, IMapper mapper, IMediator mediator)
        {
            _service = service;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CompanyModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var companyEntity = _mapper.Map<Company>(model);
                    var result = _mediator.Send(new CreateCompanyCommand() { Company = companyEntity });

                    return Ok(result.Id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //var list = await _service.GetAll();
                var query = new GetAllCompaniesQuery();
                var list = await _mediator.Send(query);
                return Ok(_mapper.Map<IEnumerable<Company>, IEnumerable<CompanyModel>>(list));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Guid idGuid = Guid.Parse(id);
            var query = new GetCompanyByIdQuery(idGuid);
            var companyResult = await _mediator.Send(query);
            
            if (companyResult == null)
                return NotFound();
            return Ok(CompanyConvert.toModel(companyResult));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]CompanyModel model)
        {
            var companyEntity = _mapper.Map<CompanyModel, Company>(model);
            var companyUpdated = await _mediator.Send(new UpdateCompanyCommand() { Company = companyEntity });
            return Ok(companyUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var guidId = Guid.Parse(id);
            var result = await _mediator.Send(new DeleteCompanyCommand() { Id = guidId });
            return Ok(result);
        }
    }
}
