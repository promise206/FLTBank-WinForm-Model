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
    public partial class Registeration : Form
    {
        private int _Id;
        private IServiceProvider serviceProvider;
        private readonly IReadWriteToJson _Database;
        private readonly ICustomerServices customerServices;
        private readonly IAccountServices accountServices;
        private readonly IValidate validate;
        private readonly IHashPassword hashPassword;
        private readonly IUtility Utility;
        private readonly string customerFile = "Customer.json";
        public Registeration(IReadWriteToJson Database, IAccountServices accountServices, IServiceProvider serviceProvider,IValidate validate, IHashPassword hashPassword, ICustomerServices customerServices, IUtility utility)
        {
            InitializeComponent();
            _Database = Database;
            this.accountServices = accountServices;
            this.serviceProvider = serviceProvider;
            this.validate = validate;
            this.hashPassword = hashPassword;
            this.customerServices = customerServices;
            Utility = utility;
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
             if (txtEmail.Text == "" || txtFirstName.Text == "" || txtLastName.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Please fill in the Blanks");
            }
            else
            {
                try
                {
                    string _accountType = string.Empty;
                    string _email = string.Empty;
                    string _password = string.Empty;
                    string _hashedPassword = string.Empty;
                    string _firstName = string.Empty;
                    string _lastName = string.Empty;
                    string _fullName = string.Empty;
                    string _accountNumber = string.Empty;
                    

                    if (rbtnCurrent.Checked)
                    {
                        _accountType = "Current";
                    }
                    else
                    {
                        _accountType = "Savings";
                    }

                    _email = txtEmail.Text.ToLower();
                    _password = txtPassword.Text;
                    if (!validate.isValidPassword(_password))
                    {
                        MessageBox.Show("Invalid Password Format, Password must Contain Special Characters, Uppercase, Number and must be up to 6 Characters.");
                    }
                    if (!validate.isValidEmail(_email))
                    {
                        MessageBox.Show("Invalid Email Format!");
                        Application.Exit();
                    }
                    

                    _hashedPassword = hashPassword.ComputeSha256Hash(txtPassword.Text);
                    _firstName = txtFirstName.Text;
                    _firstName = char.ToUpper(_firstName[0]) + _firstName.Substring(1);
                    _lastName = txtLastName.Text;
                    _lastName = char.ToUpper(_lastName[0]) + _lastName.Substring(1);
                    _accountNumber = Utility.generateNumber();
                   

                    if (!validate.isValidName(_firstName) || !validate.isValidName(_lastName))
                    {
                        MessageBox.Show("Invalid Name Format! Your Name should not start with a number");
                        Application.Exit();
                    }
                        _fullName = _firstName + " " + _lastName;

                    var now = await customerServices.DoesEmailExist(_email);
                    if (now)
                    {
                        MessageBox.Show("Email Already Exist, try Again");
                    }
                    else
                    {
                        var customers = await _Database.ReadJson<CustomerModel>(customerFile);

                        int count = customers.Count();
                        _Id = count;
                        _Id++;

                        CustomerModel customer = new CustomerModel(_Id, _fullName, _email, _hashedPassword);

                        var storeAccount = await accountServices.AddAccount(_Id, 0, _accountNumber, _fullName, _accountType);

                        var result = await customerServices.RegisterCustomer(customer);

                        if (result && storeAccount)
                        {
                            MessageBox.Show("Successful!");

                            Login login = serviceProvider.GetRequiredService<Login>();
                            login.Show();

                        }
                        else
                        {
                            MessageBox.Show("Unsuccessful!");
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = serviceProvider.GetRequiredService<Login>();
            login.Show();
            this.Hide();
        }
    }
}
