using Core;

namespace WebApp.Service
{
    public interface ICaseService
    {
        Task<List<Cases>> GetCasesById(int id);
        Task<Cases> GetCaseByCaseId(int id);
        Task AssignCase(int caseId, int employeeId);
        Task ReleaseCase(int caseId);
        Task CreateCaseComment(int caseId, string message);
        Task UpdateStatus(int caseId, string status);
        Task UpdateTime(int caseId, DateTime timeEst);
        Task<List<Cases>> GetFilteredCases(int? departmentId, int? employeeId, int? typeId, string? status);
    }
}
