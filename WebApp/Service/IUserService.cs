using Core;

namespace WebApp.Service
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<List<User>> GetEmployeesByDepartment(int departmentId);
    }
}
