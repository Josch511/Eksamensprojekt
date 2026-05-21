using Core;
using Interface;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Repository;
using ServerAPI.Interface;

public class CaseRepository : ICaseRepository
{
    private readonly IMongoCollection<Cases> _cases;
    private readonly IOrderItemsRepository _orderItemsRepository;

    public CaseRepository(AuthenticationRepo authRepo, IOrderItemsRepository orderItemsRepository)
    {
        _cases = authRepo.db.GetCollection<Cases>("cases");
        _orderItemsRepository = orderItemsRepository;
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

    public async Task AddCaseUpdate(int caseId, CaseUpdate caseUpdate)
    {
        var update = Builders<Cases>.Update
            .Push(c => c.caseUpdates, caseUpdate);

        var result = await _cases.UpdateOneAsync(c => c._id == caseId, update);

        if (result.MatchedCount == 0)
            throw new KeyNotFoundException($"Case {caseId} not found.");
    }

    public async Task<List<Cases>> GetCasesById(int id)
    {
        return await _cases.Find(c => c.userId == id).ToListAsync();
    }

    public async Task<Cases> GetCaseByCaseId(int id)
    {
        var currentCase = await _cases.Find(c => c._id == id).FirstOrDefaultAsync(); 
        
        currentCase.orderItem = await _orderItemsRepository.GetOrderById(currentCase.orderItemId);

        return currentCase;
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


    public async Task<List<Cases>> GetMyCasesById(int employeeId)
    {
        return await _cases.Find(c => c.assignedEmployeeId == employeeId).ToListAsync();
    }
    public async Task<bool> UpdateStatus(int caseId, string status)
    {
        var filter = Builders<Cases>.Filter.Eq(c => c._id, caseId);
        var update = Builders<Cases>.Update.Set(c => c.status, status);
        var result = await _cases.UpdateOneAsync(filter, update);
        return result.ModifiedCount > 0;
    }
    

    public async Task UpdateTime(int caseId, DateTime timeEst)
    {
        var filter = Builders<Cases>.Filter.Eq(c => c._id, caseId);
        var update = Builders<Cases>.Update.Set(c => c.eta, timeEst);
        await _cases.UpdateOneAsync(filter, update);
    }

    public async Task<List<Cases>> GetFilteredCases(int? departmentId, int? employeeId, int? typeId, string? status)
    {
        var filter = Builders<Cases>.Filter.Empty;

        if (departmentId.HasValue)
            filter &= Builders<Cases>.Filter.Eq(c => c.departmentId, departmentId.Value);

        if (employeeId.HasValue)
            filter &= Builders<Cases>.Filter.Eq(c => c.assignedEmployeeId, employeeId.Value);

        if (typeId.HasValue)
            filter &= Builders<Cases>.Filter.Eq(c => c.typeId, typeId.Value);

        if (!string.IsNullOrEmpty(status))
            filter &= Builders<Cases>.Filter.Eq(c => c.status, status);

        return await _cases.Find(filter).ToListAsync();
    }
}
