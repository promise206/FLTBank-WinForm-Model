namespace FltBankInfrastructure
{
    public interface IReadWriteToJson
    {
        Task<List<T>> ReadJson<T>(string jsonFile);
        Task<bool> WriteJson<T>(T model, string jsonFile);
        Task<bool> OverWriteJson<T>(T model, string jsonFile);
        Task<List<T>> ReadJson2<T>(string jsonFile);
    }
}