using Core;
using MongoDB.Driver;

namespace Repository;

public class AuthenticationRepo
{
    public readonly IMongoDatabase db;

    public AuthenticationRepo(IConfiguration configuration)
    {
        var connectionString = configuration["Mongo:ConnectionString"];
        var client = new MongoClient(connectionString);
        db = client.GetDatabase("eksamensprojekt");
    }
}