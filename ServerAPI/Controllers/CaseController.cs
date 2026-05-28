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
    private readonly ICaseUpdateService _caseUpdateService;

    public CaseController(ICaseRepository caseRepository, ICaseUpdateService caseUpdateService)
    {
        _caseRepository = caseRepository;
        _caseUpdateService = caseUpdateService;
    }

    [HttpPost]
    public async Task<IActionResult> SendCase([FromBody] Cases newCase)
    {
        if (newCase is null) return BadRequest("Case payload is required.");

        await _caseRepository.SaveCase(newCase);
        return Ok(newCase);
    }

    [HttpPost("{caseId}/updates")]
    public async Task<IActionResult> AddCaseComment(int caseId, [FromBody] string commentMessage)
    {
        if (caseId <= 0) return BadRequest("Invalid case id.");
        if (string.IsNullOrWhiteSpace(commentMessage)) return BadRequest("Comment message is required.");

        await _caseUpdateService.Build(caseId, "A comment has been added by staff", true, commentMessage);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCasesById(int id)
    {
        if (id <= 0) return BadRequest("Invalid user id.");

        var currentCases = await _caseRepository.GetCasesById(id);
        if (currentCases is null) return NotFound("Cases not found");
        return Ok(currentCases);
    }

    [HttpGet("single/{id}")]
    public async Task<IActionResult> GetCaseByCaseId(int id)
    {
        if (id <= 0) return BadRequest("Invalid case id.");

        var currentCase = await _caseRepository.GetCaseByCaseId(id);
        if (currentCase is null) return NotFound($"Case {id} not found");
        return Ok(currentCase);
    }

    [HttpPut("{caseId}/assign/{employeeId}")]
    public async Task<IActionResult> AssignCase(int caseId, int employeeId)
    {
        if (caseId <= 0) return BadRequest("Invalid case id.");
        if (employeeId <= 0) return BadRequest("Invalid employee id.");

        var success = await _caseRepository.AssignCase(caseId, employeeId);
        if (!success) return BadRequest("Case already assigned");

        await _caseUpdateService.Build(caseId, "Employee has been assigned to the case", false, null);
        return Ok();
    }

    [HttpPut("{caseId}/release")]
    public async Task<IActionResult> ReleaseCase(int caseId)
    {
        if (caseId <= 0) return BadRequest("Invalid case id.");

        var success = await _caseRepository.ReleaseCase(caseId);
        if (!success) return BadRequest("Could not release case");
        return Ok();
    }

    [HttpGet("my/{employeeId}")]
    public async Task<IActionResult> GetMyCasesById(int employeeId)
    {
        if (employeeId <= 0) return BadRequest("Invalid employee id.");

        var cases = await _caseRepository.GetMyCasesById(employeeId);
        if (cases is null) return NotFound($"No cases found with employeeId {employeeId}");
        return Ok(cases);
    }

    [HttpPut("{caseId}/status")]
    public async Task<IActionResult> UpdateStatus(int caseId, [FromBody] string status)
    {
        if (caseId <= 0) return BadRequest("Invalid case id.");
        if (string.IsNullOrWhiteSpace(status)) return BadRequest("Status is required.");

        var success = await _caseRepository.UpdateStatus(caseId, status);
        if (!success) return BadRequest("Could not update case status");

        await _caseUpdateService.Build(caseId, $"Status updated to {status}", false, null);
        return Ok();
    }

    [HttpPut("{caseId}/time")]
    public async Task<IActionResult> UpdateTime(int caseId, [FromBody] DateTime timeEst)
    {
        if (caseId <= 0) return BadRequest("Invalid case id.");
        if (timeEst == default) return BadRequest("Invalid time estimate.");

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
        if (cases is null) return NotFound("Filtered Cases not found");
        return Ok(cases);
    }
}