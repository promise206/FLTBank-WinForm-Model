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
    public partial class CreateAccountForm : Form
    {
        private readonly IAccountServices _accountServices;
        private readonly IUtility _utility;
        private readonly IServiceProvider _serviceProvider;
        public CreateAccountForm(IServiceProvider serviceProvider, IUtility utility, IAccountServices accountServices)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _utility = utility;
            _accountServices = accountServices;
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string accountType = string.Empty;
            string _accountNumber = _utility.generateNumber();
            if (rbtnCurrent.Checked)
            {
                accountType = "Current";

            }else if (rbtnSavings.Checked)
            {
                accountType = "Savings";
            }
            else
            {
                MessageBox.Show("You Forgot to Select an account, Try Again!");
            }

            /*int customerId, double accountBalance, string accountNumber, string accountName, string accountType*/
            var storeAccount = await _accountServices.AddAccount(DTO.CurrentUser.Id,0,_accountNumber,DTO.CurrentUser.FullName,accountType);

            if (storeAccount)
            {
                MessageBox.Show("Account Created Successfully!");
                Dashboard dashboard = _serviceProvider.GetRequiredService<Dashboard>();
                dashboard.Show();
                this.Hide();
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = _serviceProvider.GetRequiredService<Dashboard>();
            dashboard.Show();
            this.Hide();
        }
    }
}
