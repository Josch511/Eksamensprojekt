using Core;

namespace ServerAPI.Interface
{
    public interface ICaseRepository
    {
        Task<List<Cases>> GetAllCases();
    }
}
