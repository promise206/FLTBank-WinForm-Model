using FltBankCore.Interface;

namespace FltBankCore.Helper
{
    public class Utility : IUtility
    {
        public string generateNumber()
        {
            string accountNumber = "";
            Random random = new Random();
            for (int i = 1; i < 11; i++)
            {
                accountNumber += random.Next(0, 9).ToString();
            }
            return accountNumber;
        }
    }
}
