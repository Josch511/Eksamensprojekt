using Core;

namespace Interface
{
    public interface IUserRepository
    {
        Task<User?> LoginUser(User customer);
        Task<User?> GetByEmailAsync(string email);
        Task<List<User>> GetCustomers();
        Task<List<User>> GetEmployees();
        Task<User> GetUserById(int id);
    }
}
