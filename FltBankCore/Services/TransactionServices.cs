using FltBankCore.Interface;
using FltBankCore.Model;
using FltBankInfrastructure;

namespace FltBankCore.Services
{
    public class TransactionServices : ITransactionServices
    {
        private readonly IServiceProvider ServiceProvider;
        private int _transactionId { get; set; }
        private readonly IReadWriteToJson _Database;
        private readonly string accountFile = "Account.json";
        private readonly string transactionFile = "Transaction.json";

        public TransactionServices(IServiceProvider serviceProvider, IReadWriteToJson database)
        {
            
            ServiceProvider = serviceProvider;
            _Database = database;
        }
        public async void Deposit(string accountNumber, double amount)
        {
            var transactions = await _Database.ReadJson<TransactionModel>(transactionFile);

            int transactionCount = transactions.Count;
            _transactionId = transactionCount;
            try
            {
                string date = DateTime.Now.ToString("MM/dd/yyyy");
                double finalBalance = 0;
                string accountType = "";
                string accountName = "";
                int accountId = 0;
                int customerId = 0;
                double accountBalance = 0;
                string description;


                var accounts = await _Database.ReadJson2<AccountModel>(accountFile);

                foreach (var account in accounts)
                {
                    if (account.AccountNumber.Equals(accountNumber))
                    {
                        accountType = account.AccountType;
                        accountName = account.AccountName;
                        accountId = account.AccountId;
                        customerId = account.CustomerId;
                        accountBalance = account.AccountBalance;
                    }
                }

                Console.WriteLine($"------ Deposit Into {accountName} Account ------");

                Savings savings = new Savings(accountId, customerId, accountBalance, accountNumber, accountName, accountType);
                Current current = new Current(accountId, customerId, accountBalance, accountNumber, accountName, accountType);

                if (accountType == "Savings")
                {

                    savings.deposit(amount);

                    description = $"Credited By {savings.AccountName} ";

                    finalBalance = accountBalance + amount;

                    _transactionId++;
                    //Store Transaction Details
                    TransactionModel transaction = new TransactionModel(_transactionId, savings.AccountId, customerId, amount, date, description, finalBalance, savings.AccountNumber);
                    var storeAccount = await AddTransaction(transaction);

                    //Update Account Balance in The Accounts List


                    var Accounts = await _Database.ReadJson2<AccountModel>(accountFile);

                    foreach (var account in accounts.Where(x=> x.AccountNumber == accountNumber))
                    {
                        account.AccountBalance = finalBalance;
                    }
                    var writeToJson = await _Database.OverWriteJson<List<AccountModel>>(accounts, accountFile);

                    //Success Message
                }
                else if (accountType == "Current")
                {

                    current.deposit(amount);

                    description = $"Credited By {current.AccountName} ";

                    finalBalance = accountBalance + amount;
                    


                    _transactionId++;
                    //Store Stransaction Details
                    TransactionModel transaction = new TransactionModel(_transactionId, current.AccountId, customerId, amount, date, description, finalBalance, current.AccountNumber);
                    var storeAccount = await AddTransaction(transaction);

                    var Accounts = await _Database.ReadJson2<AccountModel>(accountFile);

                    //Update Account Balance in The Accounts List

                    foreach (var account in accounts.Where(x => x.AccountNumber == accountNumber))
                    {
                        account.AccountBalance = finalBalance;
                    }

                    var writeToJson = await _Database.OverWriteJson<List<AccountModel>>(accounts, accountFile);
                    //Success Message

                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Withdraw(string accountNumber, double amount)
        {
            var transactions = await _Database.ReadJson<TransactionModel>(transactionFile);

            int transactionCount = transactions.Count;
            _transactionId = transactionCount;
            try
            {
                string date = DateTime.Now.ToString("MM/dd/yyyy");
                double finalBalance = 0;
                string accountType = "";
                int accountId = 0;
                int customerId = 0;
                string accountName = "";
                double accountBalance = 0;
                string description;

                var accounts = await _Database.ReadJson2<AccountModel>(accountFile);

                foreach (var account in accounts)
                {
                    if (account.AccountNumber.Equals(accountNumber))
                    {
                        accountType = account.AccountType;
                        accountName = account.AccountName;
                        accountId = account.AccountId;
                        customerId = account.CustomerId;
                        accountBalance = account.AccountBalance;
                    }
                }

                Console.WriteLine($"------ Debit From {accountName} ------");

                if (accountType == "Savings" && accountBalance - amount < Savings.MinimumBalance)
                {
                    
                    return false;
                }

                Savings savings = new Savings(accountId, customerId, accountBalance, accountNumber, accountName, accountType);
                Current current = new Current(accountId, customerId, accountBalance, accountNumber, accountName, accountType);

                if (accountType == "Savings")
                {

                    savings.withdraw(amount);

                    description = $"Debited From {savings.AccountName} ";

                    finalBalance = accountBalance - amount;

                    _transactionId++;
                    //Store Transaction Details
                    TransactionModel transaction = new TransactionModel(_transactionId, current.AccountId, customerId, amount, date, description, finalBalance, current.AccountNumber);
                    var storeAccount = await AddTransaction(transaction);

                    //Update Account Balance in The Accounts List
                    var Accounts = await _Database.ReadJson2<AccountModel>(accountFile);

                    foreach (var account in accounts.Where(x => x.AccountNumber == accountNumber))
                    {
                        account.AccountBalance = finalBalance;
                    }

                    
                    var writeToJson = await _Database.OverWriteJson<List<AccountModel>>(accounts, accountFile);

                    if (writeToJson)
                    {
                        return true;
                    }
                    
                }
                else if (accountType == "Current")
                {

                    current.withdraw(amount);

                    description = $"Debited From {current.AccountName} ";

                    finalBalance = accountBalance - amount;


                    _transactionId++;
                    //Store Stransaction Details
                    TransactionModel transaction = new TransactionModel(_transactionId, current.AccountId, customerId, amount, date, description, finalBalance, current.AccountNumber);
                    var storeAccount = AddTransaction(transaction);

                    var Accounts = await _Database.ReadJson2<AccountModel>(accountFile);

                    //Update Account Balance in The Accounts List
                    foreach (var account in accounts.Where(x => x.AccountNumber == accountNumber))
                    {
                        account.AccountBalance = finalBalance;
                    }

                    var writeToJson = await _Database.OverWriteJson<List<AccountModel>>(accounts, accountFile);
                    //Success Message
                    if (writeToJson)
                    {
                        return true;
                    }
                }

                return false;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<bool> Transfer(string senderAccountNumber, string recieverAccountNumber, double amount)
        {
            //Withdraw from sender.
            var now = await Withdraw(senderAccountNumber, amount);

            if (now)
            {
                //Deposit Reciever.
                Deposit(recieverAccountNumber, amount);

                return true;
            }
            else
            {
                return false;
            }
            
        }

        public async Task<bool> AddTransaction(TransactionModel transaction)
        {
            try
            {
                return await _Database.WriteJson<TransactionModel>(transaction, transactionFile);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
