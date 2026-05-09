using Core;

namespace ServerAPI.Interface;

public interface IDepartmentsRepository
{
    Task<List<Departments>> GetAllDepartments();
    Task<Departments?> GetDepartmentById(int id);
    
}