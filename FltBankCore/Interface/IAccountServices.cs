using FltBankCore.Model;

namespace FltBankCore.Interface
{
    public interface IAccountServices
    {
        Task<bool> AddAccount( int customerId, double accountBalance, string accountNumber, string accountName, string accountType);
        Task<List<AccountModel>> DisplayAccounts(int customerId);
        Task<bool> VerifyAccountNumber(string AccountNumber);
        Task<AccountModel> LastAccount();
    }
}
