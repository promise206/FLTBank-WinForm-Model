using FltBankCore.Interface;
using System.Security.Cryptography;
using System.Text;

namespace FltBankCore.Helper
{
    public class HashPassword : IHashPassword
    {
        public string ComputeSha256Hash(string password)
        {
            //Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {

                //ComputeHas - return byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                //Convert byte array to string 
                StringBuilder bulder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    bulder.Append(bytes[i].ToString("x2"));
                }
                return bulder.ToString();

            }
        }

    }
}
