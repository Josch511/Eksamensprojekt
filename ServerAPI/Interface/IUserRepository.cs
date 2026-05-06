using Core;

namespace Interface
{
    public interface IUserRepository
    {
        Task<User> LoginUser(User customer);
        Task<User> GetByEmailAsync(string email);
    }
}
