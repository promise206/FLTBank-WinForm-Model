using FltBankCore.Helper;
using FltBankCore.Interface;
using FltBankUI;
using Microsoft.Extensions.DependencyInjection;

namespace WinFormsApp1
{
    public partial class Welcome : Form
    {
        private IServiceProvider serviceProvider;
        private ICustomerServices customerServices;
        private IAccountServices accountServices;
        private ITransactionServices transactionServices;
        public Welcome(ITransactionServices transactionServices,IAccountServices accountServices, ICustomerServices customerServices, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.transactionServices = transactionServices;
            this.accountServices = accountServices;
            this.customerServices = customerServices;
            this.serviceProvider = serviceProvider;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Registeration register = serviceProvider.GetRequiredService<Registeration>();
            register.Show();
            this.Hide();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Login login = serviceProvider.GetRequiredService<Login>();
            login.Show();

            this.Hide();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}