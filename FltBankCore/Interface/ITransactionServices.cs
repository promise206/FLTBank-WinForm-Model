using FltBankCore.Model;

namespace FltBankCore.Interface
{
    public interface ITransactionServices
    {
        void Deposit(string accountNumber, double amount);
        Task<bool> Withdraw(string accountNumber, double amount);
        Task<bool> Transfer(string senderAccountNumber, string recieverAccountNumber, double amount);
    }
}