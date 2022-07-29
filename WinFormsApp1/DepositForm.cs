using FltBankCore.Helper;
using FltBankCore.Interface;
using FltBankCore.Model;
using FltBankInfrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FltBankUI
{
    public partial class DepositForm : Form
    {
        private readonly IServiceProvider _serviceProvide;
        private readonly IValidate _validate;
        private readonly ITransactionServices _transactionServices;
        private readonly IAccountServices _accountServices;
        private readonly IReadWriteToJson _Database;
        private readonly string accountFile = "Account.json";
        public DepositForm(IReadWriteToJson Database, IAccountServices accountServices, IValidate validate, ITransactionServices transactionServices, IServiceProvider serviceProvide)
        {

            InitializeComponent();
            _Database = Database;
            _accountServices = accountServices;
            _validate = validate;
            _transactionServices = transactionServices;
            _serviceProvide = serviceProvide;
        }

        private async void btnDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                double amount;
                Double.TryParse(txtAmount.Text, out amount);
                string accountNumber = cobDeposit.Text;
                //string accountNumber = txtAccountNumber.Text;


                var accounts = await _Database.ReadJson2<AccountModel>(accountFile);

                bool has = false;

                foreach (var account in accounts)
                {
                    if (account.AccountNumber.Equals(accountNumber))
                    {
                         has = true;
                        break;
                    }

                }


                if (!has)
                {
                    MessageBox.Show("Invalid Reciever Account Number!");

                }

                if (amount.Equals(string.Empty) || amount.Equals(string.Empty))
                {
                    MessageBox.Show("Fields must not be Empty");
                }
                else
                {
                    if (_validate.isValidAmount(amount))
                    {
                        _transactionServices.Deposit(accountNumber, amount);
                        MessageBox.Show("Successful!");
                        Dashboard dashboard = _serviceProvide.GetRequiredService<Dashboard>();
                        dashboard.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Amount / Account Number!");
                    }
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private async void DepositForm_Load(object sender, EventArgs e)
        {
            var accounts = await _accountServices.DisplayAccounts(DTO.CurrentUser.Id);
            cobDeposit.Items.Clear();
            var cc = accounts.Select(x => x.AccountNumber).ToArray();
            cobDeposit.Items.AddRange(cc);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = _serviceProvide.GetRequiredService<Dashboard>();
            dashboard.Show();
            this.Hide();
        }
    }
}
