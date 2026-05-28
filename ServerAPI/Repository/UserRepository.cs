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
        var existingUser = await GetByEmailAsync(user.email);

        if (existingUser == null)
        {
            return null;
        }
        
        if (user.password != existingUser.password)
        {
            return null;
        }

        return existingUser;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _users.Find(u => u.email == email).FirstOrDefaultAsync();
    }

    public async Task<List<User>> GetCustomers()
    {
        return await _users
            .Find(u => u.role == "customer")
            .ToListAsync();
    }

    public async Task<List<User>> GetEmployees()
    {
        return await _users
            .Find(u => u.role == "employee")
            .ToListAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        return await _users.Find(u => u._id == id).FirstOrDefaultAsync();
    }
}