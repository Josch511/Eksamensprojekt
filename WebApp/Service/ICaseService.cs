using Core;

namespace WebApp.Service
{
    public interface ICaseService
    {
        Task<List<Cases>> GetAllCases();
    }
}
