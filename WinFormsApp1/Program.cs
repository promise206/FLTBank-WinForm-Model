using FltBankCore.Helper;
using FltBankCore.Interface;
using FltBankCore.Services;
using FltBankInfrastructure;
using FltBankUI;
using Microsoft.Extensions.DependencyInjection;
namespace WinFormsApp1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);  
            
            var services = new ServiceCollection();
            ConfigureServices(services);

            using(ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
            var form1 = serviceProvider.GetRequiredService<Welcome>();
                Application.Run(form1);
            }

        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddScoped<ICustomerServices, CustomerServices>();
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<IReadWriteToJson, ReadWriteToJson>();
            services.AddScoped<IHashPassword, HashPassword>();
            services.AddScoped<IValidate, Validate>();
            services.AddScoped<ITransactionServices, TransactionServices>();
            services.AddScoped<IUtility, Utility>();
            services.AddScoped<Login>();
            services.AddScoped<Welcome>();
            services.AddScoped<Registeration>();
            services.AddScoped<Dashboard>();
            services.AddScoped<DepositForm>();
            services.AddScoped<CreateAccountForm>();
            services.AddScoped<WithdrawForm>();
            services.AddScoped<TransferForm>();
        }
    }
}