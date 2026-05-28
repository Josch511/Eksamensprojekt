using Core;

namespace ServerAPI.Interface
{
    public interface ICaseRepository
    {
        Task SaveCase(Cases newcase);
        Task<bool> AddCaseUpdate(int caseId, CaseUpdate caseUpdate);
        Task<List<Cases>> GetCasesById(int id);
        Task<Cases> GetCaseByCaseId(int id);
        Task<List<Cases>> GetMyCasesById(int employeeId);
        Task<bool> AssignCase(int caseId, int employeeId);
        Task<bool> ReleaseCase(int caseId);
        Task<bool> UpdateStatus(int caseId, string status);
        Task UpdateTime(int caseId, DateTime update);
        Task<List<Cases>> GetFilteredCases(int? departmentId, int? employeeId, int? typeId, string? status);
    }
}
