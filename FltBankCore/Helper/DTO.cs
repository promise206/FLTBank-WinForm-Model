
using FltBankCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FltBankCore.Helper
{
    public class DTO
    {
        public static CustomerModel CurrentUser;
        public static List<AccountModel> Accounts;
        public static List<TransactionModel> Transactions;
        public static List<CustomerModel> Customers;

    }
}
