using Core;
using Interface;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Repository;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(AuthenticationRepo authRepo)
    {
        _users = authRepo.db.GetCollection<User>("user");
    }

    public async Task<User> LoginUser(User user)
    {
        try
        {
            var existingUser = await GetByEmailAsync(user.email);
            if (existingUser == null)
                throw new UnauthorizedAccessException("Email eller password forkert");

            return existingUser;
        }
        catch (UnauthorizedAccessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("Noget gik galt på serveren", ex);
        }
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _users.Find(u => u.email == email).FirstOrDefaultAsync();
    }
}