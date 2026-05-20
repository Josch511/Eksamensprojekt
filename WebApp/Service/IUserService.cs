using Core;

namespace WebApp.Service
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
    }
}
