using Core;

namespace Interface
{
    public interface IUserCustomerRepository
    {
        Task<UserCustomer> LoginUser(UserCustomer customer);
        Task<UserCustomer> GetByEmailAsync(string email);
    }
}
