using EmployeeApi.Application.Abstractions;
using EmployeeApi.Application.DTOs.Employees;
using EmployeeApi.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.API.Controllers;
 
    [ApiController]
    [Route("api/v1/employees")] 
   public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(
        IEmployeeService service)
    {
        _service = service;
    }

    // GET /api/v1/employees/10
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmployeeResponse>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var employee =
            await _service.GetByIdAsync(
                id,
                cancellationToken);

        return Ok(employee);
    }

    // GET /api/v1/employees?page=1&pageSize=20
    [HttpGet]
    public async Task<ActionResult<
        PagedResponse<EmployeeResponse>>> Search(
        [FromQuery] EmployeeSearchRequest request,
        CancellationToken cancellationToken)
    {
        var result =
            await _service.SearchAsync(
                request,
                cancellationToken);

        return Ok(result);
    }

    // POST
    [HttpPost]
    public async Task<ActionResult<EmployeeResponse>> Create(
        [FromBody] CreateEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        var employee =
            await _service.CreateAsync(
                request,
                cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = employee.Id },
            employee);
    }

    // PUT
    [HttpPut("{id:int}")]
    public async Task<ActionResult<EmployeeResponse>> Update(
        int id,
        [FromBody] UpdateEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        var employee =
            await _service.UpdateAsync(
                id,
                request,
                cancellationToken);

        return Ok(employee);
    }

    // DELETE
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(
            id,
            cancellationToken);

        return NoContent();
    }
} 
