namespace FltBankCore.Model
{
    class Current : AccountModel
    {

        public static int minimumAmount = 0;

        public Current(int accountId, int customerId, double accountBalance, string accountNumber, string accountName, string accountType) : base(accountId, customerId, accountBalance, accountNumber, accountName, accountType)
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
