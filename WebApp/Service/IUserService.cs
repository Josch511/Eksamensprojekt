using Core;

namespace WebApp.Service
{
    public interface IUserService
    {
        Task<User> LoginUser(string email, string password);
        Task<User> GetUserById(int id);
    }
}
