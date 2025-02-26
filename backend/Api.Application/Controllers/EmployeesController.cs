using Microsoft.AspNetCore.Mvc;
using Api.Domain.Interfaces.Services;
using Api.Domain.Entities;
using Api.Domain.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Api.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeeService employeeService, IMapper mapper, ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDtoCreate employeeDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid employee data received: {@EmployeeDto}", employeeDto);
                return BadRequest(ModelState);
            }
            var employeeEntity = _mapper.Map<EmployeeEntity>(employeeDto);
            var result = await _employeeService.CreateAsync(employeeEntity);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeDtoUpdate employeeDto)
        {
            var employeeEntity = _mapper.Map<EmployeeEntity>(employeeDto);
            employeeEntity.Id = id;
            var result = await _employeeService.UpdateAsync(employeeEntity);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _employeeService.DeleteAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _employeeService.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _employeeService.GetByNameAsync(name);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<EmployeeDtoResult>>(employees);
            return Ok(result);
        }
    }
}
