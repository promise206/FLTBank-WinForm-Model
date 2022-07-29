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
    public partial class TransferForm : Form
    {
        private readonly IServiceProvider _serviceProvide;
        private readonly IAccountServices _accountServices;
        private readonly IReadWriteToJson _Database;
        private readonly IValidate _validate;
        private readonly ITransactionServices _transactionServices;
        private readonly string accountFile = "Account.json";
        public TransferForm(IAccountServices accountServices, IValidate validate, ITransactionServices transactionServices, IServiceProvider serviceProvide)
        {

            InitializeComponent();
            _accountServices = accountServices;
            _validate = validate;
            _transactionServices = transactionServices;
            _serviceProvide = serviceProvide;
        }

        private async void btnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                double amount;

                Double.TryParse(txtAmount.Text, out amount);

                //string SenderAccountNumber = txtSender.Text;
                string SenderAccountNumber = cobSender.Text;

                string RecieverAccountNumber = txtReciever.Text;

                var verifyAccount = await _accountServices.VerifyAccountNumber(RecieverAccountNumber);


                if (!verifyAccount)
                {
                    MessageBox.Show("Invalid Reciever Account Number!");
                    
                }

                if (amount.Equals(string.Empty) || amount.Equals(string.Empty))
                {
                    MessageBox.Show("Fields must not be Empty!");
                }
                else
                {
                    if (_validate.isValidAmount(amount))
                    {
                        _transactionServices.Transfer(SenderAccountNumber,RecieverAccountNumber,amount);
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

        private async void TransferForm_Load(object sender, EventArgs e)
        {
            var accounts = await _accountServices.DisplayAccounts(DTO.CurrentUser.Id);
            cobSender.Items.Clear();
            var cc = accounts.Select(x => x.AccountNumber).ToArray();
            cobSender.Items.AddRange(cc);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = _serviceProvide.GetRequiredService<Dashboard>();
            dashboard.Show();
            this.Hide();
        }
    }
}
