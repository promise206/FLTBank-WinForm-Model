namespace FltBankCore.Interface
{
    public interface IHashPassword
    {
        string ComputeSha256Hash(string password);
    }
}