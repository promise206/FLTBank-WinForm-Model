using Newtonsoft.Json;

namespace FltBankInfrastructure
{
    public class ReadWriteToJson : IReadWriteToJson
    {
        private readonly string database = Path.Combine(Environment.CurrentDirectory, @"Database\");

        public async Task<bool> WriteJson<T>(T model, string jsonFile)
        {
            try
            {
                string json = JsonConvert.SerializeObject(model) + Environment.NewLine;
                await File.AppendAllTextAsync(database + jsonFile, json);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<T>> ReadJson<T>(string jsonFile)
        {
            var readText = await File.ReadAllTextAsync(database + jsonFile);

            var objects = new List<T>();

            var serializer = new JsonSerializer();

            using (var stringReader = new StringReader(readText))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                jsonReader.SupportMultipleContent = true;

                while (jsonReader.Read())
                {
                    T json = serializer.Deserialize<T>(jsonReader);

                    objects.Add(json);
                }
            }
            return objects;
        }

        public async Task<bool> OverWriteJson<T>(T model, string jsonFile)
        {
            //
            //WriteAllTextAsync
            try
            {
                string json = JsonConvert.SerializeObject(model) + Environment.NewLine;
                await File.WriteAllTextAsync(database + jsonFile, json);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<T>> ReadJson2<T>(string jsonFile)
        {
            var readText = await File.ReadAllTextAsync(database + jsonFile);
            return JsonConvert.DeserializeObject<List<T>>(readText);
        }
    }
}
