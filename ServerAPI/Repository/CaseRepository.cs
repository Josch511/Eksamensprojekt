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
        await _cases.InsertOneAsync(newcase);
    }

    public async Task<List<Cases>> GetAllCases()
    {
        return await _cases.Find(_ => true).ToListAsync();
    }
}