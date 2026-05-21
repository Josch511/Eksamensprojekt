using Core;

namespace WebApp.Service
{
    public interface ICaseService
    {
        Task<List<Cases>> GetCasesById(int id);
        Task<Cases> GetCaseByCaseId(int id);
        Task AssignCase(int caseId, int employeeId);
        Task<bool> ReleaseCase(int caseId);
        Task CreateCaseComment(int caseId, string message);
        Task<bool> UpdateStatus(int caseId, string status);
        Task UpdateTime(int caseId, DateTime timeEst);
        Task<List<Cases>> GetFilteredCases(int? departmentId, int? employeeId, int? typeId, string? status);
    }
}
