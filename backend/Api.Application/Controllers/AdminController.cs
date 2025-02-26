using Microsoft.AspNetCore.Mvc;
using Api.Domain.Interfaces.Services;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public AdminController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;
        }

        [HttpPost("employees")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDtoCreate employeeDto)
        {
            var result = await _adminService.CreateEmployeeAsync(employeeDto);
            return CreatedAtAction(nameof(CreateEmployee), new { id = result.Id }, result);
        }

        [HttpDelete("employees/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await _adminService.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}
