using Core;
using Interface;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Interface;

namespace ServerAPI.Controllers;


[ApiController]
[Route("departments")]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentsRepository _departmentsRepository;

    public DepartmentsController(IDepartmentsRepository departmentsRepository)
    {
        _departmentsRepository = departmentsRepository;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllDepartments()
    {
        var departments = await _departmentsRepository.GetAllDepartments();

        if (departments == null || departments.Count == 0)
            return NotFound("Ingen afdelinger fundet");

        return Ok(departments);
    }

    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
        var department = await _departmentsRepository.GetDepartmentById(id);

        if (department is null)
            return NotFound($"Afdeling med id {id} blev ikke fundet");

        return Ok(department);
    }
}