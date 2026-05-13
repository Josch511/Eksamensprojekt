using Core;
using System.Net.Http.Json;
using WebApp.Service;


namespace WebApp.Service
{
    public class CaseService : ICaseService
    {
        private readonly HttpClient _http;

        public CaseService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Cases>> GetAllCases()
        {
            var cases = await _http.GetFromJsonAsync<List<Cases>>
                ($"cases");

            return cases ?? new List<Cases>();
        }

        public async Task<List<Cases>> GetCasesByDepartment(int departmentId)
        {
            var cases = await _http.GetFromJsonAsync<List<Cases>>
                ($"cases/department/{departmentId}");

            return cases ?? new List<Cases>();
        }

        public async Task<List<Cases>> GetCasesById(int id)
        {
            var cases = await _http.GetFromJsonAsync<List<Cases>>($"cases/{id}");

            return cases ?? new List<Cases>();
        }
    }
}
