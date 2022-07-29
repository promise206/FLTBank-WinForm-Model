namespace FltBankCore.Model
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public CustomerModel(int id, string AccountName, string Email, string password)
        {
            this.Id = id;
            this.FullName = AccountName;
            this.Email = Email;
            this.Password = password;
        }
    }
    }
