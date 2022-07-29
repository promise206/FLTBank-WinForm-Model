namespace FltBankCore.Model
{
    public class TransactionModel
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public double Amount { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public double AccountBalance { get; set; }
        public string AccountNumber { get; set; }

        public TransactionModel(int transactionId, int accountId, int customerId, double amount, string date, string description, double accountBalance, string accountNumber)
        {
            TransactionId = transactionId;
            AccountId = accountId;
            CustomerId = customerId;
            Amount = amount;
            Date = date;
            Description = description;
            AccountBalance = accountBalance;
            AccountNumber = accountNumber;
        }

    }
}
