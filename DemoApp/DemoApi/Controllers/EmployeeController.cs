using AutoMapper;
using DemoApi.Converts;
using DemoApi.Models;
using Domain.DTO;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService service, IMapper mapper)
        {
            _service= service;
            _mapper= mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EmployeeModel model) 
        { 
            var employeeEntitie = EmployeeConvert.toEntitie(model);
            var result = await _service.Create(employeeEntitie);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {   
            var result = await _service.GetAll();
            return Ok(_mapper.Map<IEnumerable<EmployeeDTO>, IEnumerable<EmployeeModel>>(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) 
        {
            var idGuid = Guid.Parse(id);
            var result = await _service.GetById(idGuid);
            if(result == null)
                return NotFound();
            return Ok(_mapper.Map<EmployeeDTO, EmployeeModel>( result));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EmployeeModel model) 
        {
            var employeeEntitie = EmployeeConvert.toEntitie(model);
            var result = await _service.Edit(employeeEntitie);
            return Ok(EmployeeConvert.toModel(result));
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
