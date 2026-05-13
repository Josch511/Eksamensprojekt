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
    
}
