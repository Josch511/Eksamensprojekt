using Core;

namespace ServerAPI.Interface
{
    public interface ICaseRepository
    {
        Task<List<Cases>> GetAllCases();
        Task CreateCase(Cases newcase);
        Task<List<Cases>> GetCasesById(int id);
        Task<Cases> GetCaseByCaseId(int id);

    }
}
