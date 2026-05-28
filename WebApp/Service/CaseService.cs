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


        public async Task<List<Cases>> GetCasesById(int id)
        {
            var response = await _http.GetAsync($"cases/{id}");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Ingen sager fundet", null, response.StatusCode);

            return await response.Content.ReadFromJsonAsync<List<Cases>>() ?? new List<Cases>();
        }

        public async Task<Cases> GetCaseByCaseId(int id)
        {
            var response = await _http.GetAsync($"cases/single/{id}");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Sag med id {id} blev ikke fundet", null, response.StatusCode);

            return await response.Content.ReadFromJsonAsync<Cases>() ?? new Cases();
        }

        public async Task AssignCase(int caseId, int employeeId)
        {
            await _http.PutAsync
            (
                $"cases/{caseId}/assign/{employeeId}",null
            );
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

        public async Task UpdateTime(int caseId, DateTime timeEst)
        {
            await _http.PutAsJsonAsync($"cases/{caseId}/time", timeEst);
        }

        public async Task<List<Cases>> GetFilteredCases(int? departmentId, int? employeeId, int? typeId, string? status)
        {
            var query = "cases/filter?";
            if (departmentId.HasValue) query += $"departmentId={departmentId}&";
            if (employeeId.HasValue) query += $"employeeId={employeeId}&";
            if (typeId.HasValue) query += $"typeId={typeId}&";
            if (!string.IsNullOrEmpty(status)) query += $"status={status}";

            var cases = await _http.GetFromJsonAsync<List<Cases>>(query);
            return cases ?? new List<Cases>();
        }
    }
} 
