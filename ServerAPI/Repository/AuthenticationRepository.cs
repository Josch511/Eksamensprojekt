using Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Repository;

public class AuthenticationRepo
{
    public readonly IMongoDatabase db;

    public AuthenticationRepo()
    {
        var client = new MongoClient("mongodb+srv://jona:hihi@project.uc9xnf6.mongodb.net/");
        db = client.GetDatabase("eksamensprojekt");
    }
}