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
        
        public async Task<Cases> GetCaseByCaseId(int id)
        {
            var cases = await _http.GetFromJsonAsync<Cases>($"cases/single/{id}");
            
            return cases ?? new Cases();
        }

        public async Task AssignCase(int caseId, int employeeId)
        {
            await _http.PutAsync
            (
                $"cases/{caseId}/assign/{employeeId}",null
            );
        }

        public async Task<List<Cases>> GetCasesByAssignedEmployee(int employeeId)
        {
            var cases = await _http.GetFromJsonAsync<List<Cases>>
                ($"cases/my/{employeeId}");
            return cases ?? new List<Cases>();
        }

        public async Task<bool> ReleaseCase(int caseId)
        {
            var response = await _http.PutAsJsonAsync($"cases/{caseId}/release", new { });
            return response.IsSuccessStatusCode;
        }

        public async Task CreateCaseComment(int caseId, string message)
        {
            await _http.PostAsJsonAsync($"cases/{caseId}/updates", message);
        }

        public async Task<bool> UpdateStatus(int caseId, string status)
        {
            var response = await _http.PutAsJsonAsync($"cases/{caseId}/status", status);
            return response.IsSuccessStatusCode;
        }
    }
} 
