using Core;
using Interface;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Interface;

namespace ServerAPI.Controllers;


[ApiController]
[Route("cases")]
public class CaseController : ControllerBase
{
    private readonly ICaseRepository _caseRepository;

    public CaseController(ICaseRepository caseRepository)
    {
        _caseRepository = caseRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCases()
    {
        var cases = await _caseRepository.GetAllCases();
        return Ok(cases);
    }


    [HttpPost]
    public async Task<IActionResult> CreateCase([FromBody] Cases newCase)
    {
        await _caseRepository.CreateCase(newCase);
        return Ok(newCase);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCasesById(int id)
    {
        var currentCases = await _caseRepository.GetCasesById(id);
        return Ok(currentCases);
    }
    
    [HttpGet("single/{id}")]
    public async Task<IActionResult> GetCaseByCaseId(int id)
    {
        var currentCase = await _caseRepository.GetCaseByCaseId(id);
        return Ok(currentCase);
    }
    

    [HttpGet("department/{departmentId}")]
    public async Task<IActionResult> GetCasesByDepartment(int departmentId)
    {
        var cases = await _caseRepository.GetCasesByDepartment(departmentId);
        return Ok(cases);
    }

    [HttpPut("{caseId}/assign/{employeeId}")]
    public async Task<IActionResult> AssignCase(int caseId, int employeeId)
    {
        var success = await _caseRepository.AssignCase(caseId, employeeId);

        if (!success)
        {
            return BadRequest("Case already assigned");
        }

        return Ok();
    }

    [HttpPut("{caseId}/release")]
    public async Task<IActionResult> ReleaseCase(int caseId)
    {
        var success = await _caseRepository.ReleaseCase(caseId);
        if (!success) return BadRequest("Could not release case");
        return Ok();
    }
}
