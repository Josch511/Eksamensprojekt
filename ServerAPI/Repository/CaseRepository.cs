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

    public async Task<List<Cases>> GetCasesByDepartment(int departmentId)
    {
        return await _cases.Find(c => c.departmentId == departmentId).ToListAsync();
    }

    public async Task<bool> AssignCase(int caseId, int employeeId)
    {
        var filter = Builders<Cases>.Filter.And(
            Builders<Cases>.Filter.Eq(c => c._id, caseId),
            Builders<Cases>.Filter.Or(
                Builders<Cases>.Filter.Eq(c => c.assignedEmployeeId, null),
                Builders<Cases>.Filter.Eq(c => c.assignedEmployeeId, 0)
            )
        );

        var update = Builders<Cases>.Update
            .Set(c => c.assignedEmployeeId, employeeId);

        var result = await _cases.UpdateOneAsync(filter, update);

        return result.ModifiedCount > 0;
    }

    public async Task<bool> ReleaseCase(int caseId)
    {
        var filter = Builders<Cases>.Filter.Eq(c => c._id, caseId);
        var update = Builders<Cases>.Update.Set(c => c.assignedEmployeeId, null);
        var result = await _cases.UpdateOneAsync(filter, update);
        return result.ModifiedCount > 0;
    }
}
