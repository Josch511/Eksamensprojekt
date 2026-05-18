using Core;
using Interface;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Repository;
using ServerAPI.Interface;

public class CaseRepository : ICaseRepository
{
    private readonly IMongoCollection<Cases> _cases;

    public CaseRepository(AuthenticationRepo authRepo)
    {
        _cases = authRepo.db.GetCollection<Cases>("cases");
    }

    public async Task CreateCase(Cases newcase)
    {
        var highestCase = await _cases
            .Find(_ => true)
            .SortByDescending(c => c._id)
            .FirstOrDefaultAsync();

        if (highestCase == null)
        {
            newcase._id = 1;
        }
        else
        {
            newcase._id = highestCase._id + 1;
        }

        await _cases.InsertOneAsync(newcase);
    }

    public async Task<List<Cases>> GetCasesById(int id)
    {
        return await _cases.Find(c => c.userId == id).ToListAsync();
    }

    public async Task<List<Cases>> GetAllCases()
    {
        return await _cases.Find(_ => true).ToListAsync();
    }
    
    public async Task<Cases> GetCaseByCaseId(int id)
    {
        return await _cases.Find(c => c._id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Cases>> GetCasesByDepartment(int department_id)
    {
        return await _cases.Find(c => c.departmentId == department_id).ToListAsync();
    }
}
