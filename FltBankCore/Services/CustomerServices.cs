using FltBankCore.Interface;
using FltBankCore.Model;
using FltBankInfrastructure;

namespace FltBankCore.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IReadWriteToJson _Database;
        private readonly string customerFile = "Customer.json";
        private readonly string accountFile = "Account.json";
        private readonly IHashPassword hashedPassword;

        public CustomerServices(IReadWriteToJson database, IHashPassword hashPassword)
        {
            _Database = database;
            hashedPassword = hashPassword;
        }

        public async Task<bool> RegisterCustomer(CustomerModel customer)
        {
            try
            {
                return await _Database.WriteJson<CustomerModel>(customer, customerFile);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CustomerModel> Login(string email, string password)
        {

            var customers = await _Database.ReadJson<CustomerModel>(customerFile);

            foreach (var customer in customers)
            {
                if (customer.Email.Equals(email) && customer.Password.Equals(password))
                {
                    return customer;
                    break;
                }
            }

            return null;
        }

        public async Task<bool> VerifyLoginDetails(string email, string password)
        {
            var customers = await _Database.ReadJson<CustomerModel>(customerFile);

            foreach (var customer in customers)
            {
                
                if (customer.Email.Equals(email) && customer.Password.Equals(password))
                {
                    return true;
                    break;
                }

            }

            return false;
        }

        public async Task<bool> DoesEmailExist(string email)
        {
            var customers = await _Database.ReadJson<CustomerModel>(customerFile);

            //bool has = accounts.Any(customer => customer.AccountNumber == AccountNumber);

            foreach (var customer in customers)
            {

                if (customer.Email.Equals(email))
                {
                    return true;
                    break;
                }

            }

            return false;
        }


    }
}
