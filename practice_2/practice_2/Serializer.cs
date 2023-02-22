using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace practice_2
{
    public class Serializer
    {
        public static void Serialize<T>(string path, T obj)
        {
            if (!File.Exists(path))
            {        
                File.Create(path).Close();
            }

            File.WriteAllText(path, JsonConvert.SerializeObject(obj, Formatting.Indented));
        }

        public static T Deserialize<T>(string path)
        {
            if (!File.Exists(path)) return default;

            return (T) JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
    }
}
