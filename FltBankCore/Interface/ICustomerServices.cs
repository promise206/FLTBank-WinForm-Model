using FltBankCore.Model;

namespace FltBankCore.Interface
{
    public interface ICustomerServices
    {
        Task<CustomerModel> Login(string email, string password);
        Task<bool> RegisterCustomer(CustomerModel customer);
        Task<bool> VerifyLoginDetails(string email, string password);
        Task<bool> DoesEmailExist(string email);
    }
}