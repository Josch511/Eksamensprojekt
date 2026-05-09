using Core;
using MongoDB.Driver;
using ServerAPI.Interface;

namespace Repository;

public class DepartmentsRepository : IDepartmentsRepository
{
    private readonly IMongoCollection<Departments> _departments;

    public DepartmentsRepository(AuthenticationRepo authRepo)
    {
        _departments = authRepo.db.GetCollection<Departments>("departments");
    }

    public async Task<List<Departments>> GetAllDepartments()
    {
        return await _departments.Find(_ => true).ToListAsync();
    }

    public async Task<Departments?> GetDepartmentById(int id)
    {
        return await _departments.Find(d => d._id == id).FirstOrDefaultAsync();
    }
}



