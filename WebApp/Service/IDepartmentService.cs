using Core;

namespace WebApp.Service
{
    public interface IDepartmentService
    {
        Task<List<Departments>> GetAllDepartments();
        Task<Departments?> GetDepartmentById(int id);
    }
}