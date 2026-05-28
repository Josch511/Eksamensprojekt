using Core;
using System.Net.Http.Json;

namespace WebApp.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _http;

        public DepartmentService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Departments>> GetAllDepartments()
        {
            var response = await _http.GetAsync("departments/getall");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error, null, response.StatusCode);
            }

            return await response.Content.ReadFromJsonAsync<List<Departments>>() ?? new List<Departments>();
        }

        public async Task<Departments?> GetDepartmentById(int id)
        {
            var response = await _http.GetAsync($"departments/getById/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error, null, response.StatusCode);
            }

            return await response.Content.ReadFromJsonAsync<Departments>();
        }
    }
}