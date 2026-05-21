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
    private readonly  ICaseUpdateService _caseUpdateService;

    public CaseController(ICaseRepository caseRepository, ICaseUpdateService caseUpdateService)
    {
        _caseRepository = caseRepository;
        _caseUpdateService = caseUpdateService;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> CreateCase([FromBody] Cases newCase)
    {
        await _caseRepository.CreateCase(newCase);
        return Ok(newCase);
    }

    [HttpPost("{caseId}/updates")]
    public async Task<IActionResult> AddCaseComment(int caseId, [FromBody] string commentMessage)
    {
        await _caseUpdateService.Build(caseId, "A comment has been added by staff", true, commentMessage);
        return Ok();
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
    

    [HttpPut("{caseId}/assign/{employeeId}")]
    public async Task<IActionResult> AssignCase(int caseId, int employeeId)
    {
        var success = await _caseRepository.AssignCase(caseId, employeeId);
        if (!success) return BadRequest("Case already assigned");

        await _caseUpdateService.Build(caseId, "Employee has been assigned to the case", false, null);
        return Ok();
    }

    [HttpPut("{caseId}/release")]
    public async Task<IActionResult> ReleaseCase(int caseId)
    {
        var success = await _caseRepository.ReleaseCase(caseId);
        if (!success) return BadRequest("Could not release case");
        return Ok();
    }

    [HttpGet("my/{employeeId}")]
    public async Task<IActionResult> GetMyCasesById(int employeeId)
    {
        var cases = await _caseRepository.GetMyCasesById(employeeId);
        return Ok(cases);
    }

    [HttpPut("{caseId}/status")]
    public async Task<IActionResult> UpdateStatus(int caseId, [FromBody] string status)
    {
        var success = await _caseRepository.UpdateStatus(caseId, status);
        if (!success) return BadRequest("Could not update case status");

        await _caseUpdateService.Build(caseId, $"Status updated to {status}", false, null);
        return Ok();
    }

    [HttpPut("{caseId}/time")]
    public async Task<IActionResult> UpdateTime(int caseId, [FromBody] DateTime timeEst)
    {
        await _caseRepository.UpdateTime(caseId, timeEst);
        await _caseUpdateService.Build(caseId, $"Est. resolution updated to {timeEst.ToShortDateString()}", false, null);
        return Ok();

    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetFilteredCases(
    [FromQuery] int? departmentId,
    [FromQuery] int? employeeId,
    [FromQuery] int? typeId,
    [FromQuery] string? status)
    {
        var cases = await _caseRepository.GetFilteredCases(departmentId, employeeId, typeId, status);
        return Ok(cases);
    }
}
