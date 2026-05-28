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

        public async Task<User> LoginUser(string email, string password)
        {
            var user = new { email, password };
            var response = await _http.PostAsJsonAsync("/user/login", user);
            
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error, null, response.StatusCode);
            }
            
            var loggedInUser = await response.Content.ReadFromJsonAsync<User>();
            
            return loggedInUser;
        }
        public async Task<User> GetUserById(int id)
        {
            return await _http.GetFromJsonAsync<User>($"user/{id}");
        }
    }
}