using FltBankCore.Interface;
using System.Text.RegularExpressions;

namespace FltBankCore.Helper
{
    public class Validate : IValidate
    {
        public bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return true;
            else
                return false;
        }

        public bool isValidPassword(string inputPassword)
        {

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum6Chars = new Regex(@".{6,}");

            if (hasNumber.IsMatch(inputPassword) && hasUpperChar.IsMatch(inputPassword) && hasMinimum6Chars.IsMatch(inputPassword))
                return true;
            else
                return false;
        }
        public bool isValidName(string inputName)
        {
            if (inputName != string.Empty)
            {
                if (char.IsNumber(inputName[0]))
                    return false;
                else
                    return true;
            }
            else
            {
                return false;
            }
        }

        public bool isValidAccountNumber(string accountNumber)
        {
            bool isValid = true;
            if (accountNumber != string.Empty && accountNumber.Length == 10)
            {
                foreach (char c in accountNumber)
                {
                    if (!char.IsNumber(c))
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            else
            {
                isValid = false;
            }

            return isValid;
        }

        public bool isValidAmount(double amount)
        {
            bool isvalid = true;
            if (amount >= 0)
            {
                string amounts = amount.ToString();
                foreach (char c in amounts.Where(x => !char.IsNumber(x)))
                {
                    isvalid = false;
                    break;
                }
            }
            else
            {
                isvalid = false;
            }

            return isvalid;
        }

    }
}
