using FltBankCore.Helper;
using FltBankCore.Interface;
using FltBankCore.Model;
using FltBankInfrastructure;

namespace FltBankCore.Services
{
    public class AccountServices: IAccountServices
    {
        private int _accountId;
        private readonly IReadWriteToJson _Database;
        private readonly string accountFile = "Account.json";
        private List<AccountModel> _accountModels = new();

        public AccountServices(IReadWriteToJson database)
        {
            
            _Database = database;
        }

        public async Task<bool> AddAccount( int customerId, double accountBalance, string accountNumber, string accountName, string accountType)
        {
            var accounts = await _Database.ReadJson2<AccountModel>(accountFile);

            //If the account is null, store in DTO Account

            DTO.Accounts = accounts != null ? accounts : new List<AccountModel>();
            int accountsCount = DTO.Accounts.Count();
            _accountId = accountsCount;
            _accountId++;
            AccountModel account = new AccountModel(_accountId, customerId, accountBalance, accountNumber, accountName, accountType);
            DTO.Accounts.Add(account);
            try
            {
                return await _Database.OverWriteJson<List<AccountModel>>(DTO.Accounts, accountFile);
            }
            catch (Exception)
            {
                throw;
            }
        }
        

        public async Task<List<AccountModel>> DisplayAccounts(int customerId)
        {

            var accounts = await _Database.ReadJson2<AccountModel>(accountFile);

            foreach (var account in accounts)
            {
                if (account.CustomerId.Equals(customerId))
                {
                    _accountModels.Add(account);
                }
            }

            return _accountModels;

        }

        public async Task<bool> VerifyAccountNumber(string AccountNumber)
        {
            var accounts = await _Database.ReadJson2<AccountModel>(accountFile);

            //bool has = accounts.Any(customer => customer.AccountNumber == AccountNumber);

            foreach(var account in accounts)
            {
                if (account.AccountNumber.Equals(AccountNumber))
                {
                    return true;
                    break;
                }
                
            }

            return false;
        }

        public async Task<AccountModel> LastAccount()
        {

            var LastAccount = await _Database.ReadJson<AccountModel>(accountFile);

            return LastAccount.Last();

        }
    }
}
