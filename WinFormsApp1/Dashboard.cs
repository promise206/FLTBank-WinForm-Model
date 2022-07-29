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
using WinFormsApp1;

namespace FltBankUI
{
    public partial class Dashboard : Form
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IReadWriteToJson _Database;
        private readonly IAccountServices _accountServices;
        private readonly string customerFile = "Customer.json";
        private readonly string accountFile = "Account.json";
        private readonly string transactionFile = "Transaction.json";
        public Dashboard(IAccountServices accountServices, IReadWriteToJson Database, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _accountServices = accountServices;
            _Database = Database;
            this.serviceProvider = serviceProvider;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            WithdrawForm withdraw = serviceProvider.GetRequiredService<WithdrawForm>();
            withdraw.Show();
            this.Hide();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            lstView.Clear();
            lstView.Columns.Add("FULL NAME", 250);
            lstView.Columns.Add("ACCOUNT NUMBER", 250);
            lstView.Columns.Add("ACCOUNT TYPE", 250);
            lstView.Columns.Add("AMOUNT BAL.", 250);

            var accounts = await _Database.ReadJson2<AccountModel>(accountFile);

            foreach (var account in accounts)
            {
                if (account.CustomerId.Equals(DTO.CurrentUser.Id))
                {
                    ListViewItem item = new ListViewItem(DTO.CurrentUser.FullName);
                    item.SubItems.Add(account.AccountNumber);
                    item.SubItems.Add(account.AccountType);
                    item.SubItems.Add(account.AccountBalance.ToString());
                    item.Tag = DTO.CurrentUser.FullName;
                    this.lstView.Items.Add(item);
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            DepositForm deposit = serviceProvider.GetRequiredService<DepositForm>();
            deposit.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TransferForm transfer = serviceProvider.GetRequiredService<TransferForm>();
            transfer.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Welcome welcome = serviceProvider.GetRequiredService<Welcome>();
            welcome.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CreateAccountForm form1 = serviceProvider.GetRequiredService<CreateAccountForm>();
            form1.Show();
            this.Hide();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            lstView.Clear();
            lstView.Columns.Add("DATE", 250);
            lstView.Columns.Add("DESCRIPTION", 250);
            lstView.Columns.Add("AMOUNT", 250);
            lstView.Columns.Add("BALANCE", 250);

            var transactions = await _Database.ReadJson<TransactionModel>(transactionFile);

            foreach (var transaction in transactions)
            {
                if (transaction.AccountNumber.Equals(cobAccounts.Text))
                {
                    ListViewItem item = new ListViewItem(transaction.Date);
                    item.SubItems.Add(transaction.Description);
                    item.SubItems.Add(transaction.Amount.ToString());
                    item.SubItems.Add(transaction.AccountBalance.ToString());
                    item.Tag = transaction.Date;
                    this.lstView.Items.Add(item);
                }
            }
        }

        private async void Dashboard_Load_1(object sender, EventArgs e)
        {
            var accounts = await _accountServices.DisplayAccounts(DTO.CurrentUser.Id);
            cobAccounts.Items.Clear();
            var cc = accounts.Select(x => x.AccountNumber).ToArray();
            cobAccounts.Items.AddRange(cc);
        }
    }
}
