using Core;

namespace ServerAPI.Interface
{
    public interface ICaseRepository
    {
        Task<List<Cases>> GetAllCases();
        Task CreateCase(Cases newcase);
        Task AddCaseUpdate(int caseId, CaseUpdate caseUpdate);
        Task<List<Cases>> GetCasesById(int id);
        Task<Cases> GetCaseByCaseId(int id);
        Task<List<Cases>> GetCasesByDepartment(int department_id);
        Task<List<Cases>> GetMyCasesById(int employeeId);
        Task<bool> AssignCase(int caseId, int employeeId);
        Task<bool> ReleaseCase(int caseId);
        Task<bool> UpdateStatus(int caseId, string status);
    }
}
