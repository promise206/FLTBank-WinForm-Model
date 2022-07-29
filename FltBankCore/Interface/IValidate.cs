namespace FltBankCore.Interface
{
    public interface IValidate
    {
        bool isValidAccountNumber(string accountNumber);
        bool isValidAmount(double amount);
        bool isValidEmail(string inputEmail);
        bool isValidName(string inputName);
        bool isValidPassword(string inputPassword);
    }
}