using Newtonsoft.Json;

namespace SerializationLib
{
    public class Serialization
    {
        public static void SaveObjectToFile<T>(in T obj, in string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            File.WriteAllText(filePath, JsonConvert.SerializeObject(obj));
        }
    }
}