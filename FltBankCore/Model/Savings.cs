namespace FltBankCore.Model
{
    class Savings : AccountModel
    {

        public static int MinimumBalance = 1000;
        public Savings(int accountId,int customerId, double accountBalance, string accountNumber, string accountName, string accountType) : base(accountId, customerId,  accountBalance, accountNumber, accountName, accountType)
        {
        }

        public bool deposit(double amount)
        {
            this.AccountBalance = AccountBalance + amount;
            return true;
        }

        public bool withdraw(double amount)
        {
            this.AccountBalance = AccountBalance - amount;
            return true;
        }
    }
}
