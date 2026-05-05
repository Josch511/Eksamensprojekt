using Core;
using Interface;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Repository;

public class UserCustomerRepository : IUserCustomerRepository
{
    private readonly IMongoCollection<UserCustomer> _users;

    public UserCustomerRepository(AuthenticationRepo authRepo)
    {
        _users = authRepo.db.GetCollection<UserCustomer>("userCustomer");
    }

    public async Task<UserCustomer> LoginUser(UserCustomer user)
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

    public async Task<UserCustomer?> GetByEmailAsync(string email)
    {
        return await _users.Find(u => u.email == email).FirstOrDefaultAsync();

    }
}