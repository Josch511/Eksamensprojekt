using Core;

namespace WebApp.Service
{
    public interface ICaseService
    {
        Task<List<Cases>> GetAllCases();
        Task<List<Cases>> GetCasesById(int id);
        Task<List<Cases>> GetCasesByDepartment(int departmentId);
        Task<Cases> GetCaseByCaseId(int id);
        Task AssignCase(int caseId, int employeeId);
        Task<List<Cases>> GetCasesByAssignedEmployee(int employeeId);
        Task<bool> ReleaseCase(int caseId);
    }
}
