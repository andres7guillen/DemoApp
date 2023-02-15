
using AutoMapper;
using DemoApi.Converts;
using DemoApi.Models;
using Domain.Entities;
using Domain.Services;
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

        public CompanyController(ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CompanyModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var companyEntity = _mapper.Map<Company>(model);
                    var result = await _service.Create(companyEntity);

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
                var list = await _service.GetAll();
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
            var companyResult = await _service.GetById(idGuid);
            if (companyResult == null)
                return NotFound();
            return Ok(CompanyConvert.toModel(companyResult));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]CompanyModel model)
        {
            var companyEntity = _mapper.Map<CompanyModel, Company>(model);
            var companyUpdated = await _service.Edit(companyEntity);
            return Ok(companyUpdated.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var guidId = Guid.Parse(id);
            var result = await _service.Delete(guidId);
            return Ok(result);
        }
    }
}
