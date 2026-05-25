using Core;
using System.Net.Http.Json;
using WebApp.Service;


namespace WebApp.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _http.GetFromJsonAsync<User>($"user/{id}");
        }
    }
}