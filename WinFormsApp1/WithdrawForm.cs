using FltBankCore.Helper;
using FltBankCore.Interface;
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
    public partial class WithdrawForm : Form
    {
        private readonly IServiceProvider _serviceProvide;
        private readonly IValidate _validate;
        private readonly ITransactionServices _transactionServices;
        private readonly IAccountServices _accountServices;
        public WithdrawForm(IAccountServices accountServices, IValidate validate, ITransactionServices transactionServices, IServiceProvider serviceProvide)
        {

            InitializeComponent();
            _accountServices = accountServices;
            _validate = validate;
            _transactionServices = transactionServices;
            _serviceProvide = serviceProvide;
        }

        private async void btnWithdraw_Click(object sender, EventArgs e)
        {
            try
            {
                double amount;
                Double.TryParse(txtAmount.Text, out amount);
                string accountNumber = cobWithdraw.Text;
                //string accountNumber = txtAccountNumber.Text;

                if (amount.Equals(string.Empty) || accountNumber.Equals(string.Empty))
                {
                    MessageBox.Show("Fields must not be Empty");
                }
                else
                {
                    if (_validate.isValidAmount(amount))
                    {
                        var now = await _transactionServices.Withdraw(accountNumber, amount);
                        if (now)
                        {
                            MessageBox.Show("Successful!");
                            Dashboard dashboard = _serviceProvide.GetRequiredService<Dashboard>();
                            dashboard.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Insufficient Fund!");
                            Dashboard dashboard = _serviceProvide.GetRequiredService<Dashboard>();
                            dashboard.Show();
                            this.Hide();
                        }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void WithdrawForm_Load(object sender, EventArgs e)
        {
            var accounts = await _accountServices.DisplayAccounts(DTO.CurrentUser.Id);
            cobWithdraw.Items.Clear();
            var cc = accounts.Select(x => x.AccountNumber).ToArray();
            cobWithdraw.Items.AddRange(cc);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = _serviceProvide.GetRequiredService<Dashboard>();
            dashboard.Show();
            this.Hide();
        }
    }
}
