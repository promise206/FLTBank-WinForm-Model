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
    public partial class Login : Form
    {
        private IServiceProvider serviceProvider;
        private readonly ICustomerServices customerServices;
        private readonly IHashPassword _hashPassword;
        public Login(IHashPassword hashPassword, IServiceProvider serviceProvider,ICustomerServices customerServices)
        {
            InitializeComponent();
            this._hashPassword = hashPassword;
            this.serviceProvider = serviceProvider;
            this.customerServices = customerServices;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string Email = string.Empty;
            string Password = string.Empty;
            Email = txtEmail.Text;
            Password = txtPassword.Text;
            string HashedPassword = string.Empty;

            if(txtEmail.Text == string.Empty || txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Please Fill in the Blanks!");
            }
            else
            {
                HashedPassword = _hashPassword.ComputeSha256Hash(Password);
                var verifyCustomer = await customerServices.VerifyLoginDetails(Email,HashedPassword);

                if (!verifyCustomer)
                {
                    MessageBox.Show("Invalid Login Details!");

                }
                else
                {
                    var now = await customerServices.Login(Email, HashedPassword);

                    DTO.CurrentUser = now;

                    Dashboard dashboard = serviceProvider.GetRequiredService<Dashboard>();
                    dashboard.Show();
                    this.Hide();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Registeration register = serviceProvider.GetRequiredService<Registeration>();
            register.Show();
            this.Hide();
        }
    }
}
