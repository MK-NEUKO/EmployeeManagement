using System.Collections;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
    {
        var allEmployees = await _employeeRepository.GetAllAsync();
        return Ok(allEmployees);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Employee>> GetEmployeeById(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
    {
        await _employeeRepository.AddEmployeeAsync(employee);
        return CreatedAtAction(nameof(GetEmployeeById), new {id = employee.Id}, employee);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
    {
        if (id != employee.Id)
        {
            return BadRequest();
        }

        await _employeeRepository.UpdateEmployeeAsync(employee);
        return CreatedAtAction(nameof(GetEmployeeById), new {id = employee.Id}, employee);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteEmployeeById(int id)
    {
        await _employeeRepository.DeleteEmployeeAsync(id);
        return NoContent();
    }
}