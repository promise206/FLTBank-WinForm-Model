namespace FltBankCore.Model
{
    public class AccountModel
    {

        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public double AccountBalance { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }

        public AccountModel(int accountId, int customerId, double accountBalance, string accountNumber, string accountName, string accountType)
        {
            AccountId = accountId;
            CustomerId = customerId;
            AccountBalance = accountBalance;
            AccountNumber = accountNumber;
            AccountName = accountName;
            AccountType = accountType;
        }
    }
}
