using Newtonsoft.Json;

namespace InformationSystem.Main
{
    public class Scheme
    {
        private static Scheme _instance;

        public string name = "DefaultScheme";

        public List<User> AllUsers { get; } = new List<User>();

        public static void Save()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + GetInstance().name + "\\" + GetInstance().name + ".json";
            if(!File.Exists(path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.Create(path).Close();
            }   

            string jsonString = JsonConvert.SerializeObject(GetInstance(), Formatting.Indented);
            File.WriteAllText(path, jsonString);
        }

        public static void Load()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + GetInstance().name + "\\" + GetInstance().name + ".json";
            if (File.Exists(path))
            {
                _instance = JsonConvert.DeserializeObject<Scheme>(File.ReadAllText(path));
            }
        }

        public static Scheme GetInstance()
        {
            return _instance == null ? _instance = new Scheme() : _instance;
        }
    }
}
