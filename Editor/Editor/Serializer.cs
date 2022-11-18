using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Editor
{
    public class Serializer
    {
        public static void SerializeAndSaveRectangles(string path, List<Rectangle> rectangles)
        {
            //if (Directory.GetParent(path).Exists) return;
            if (path == "") return;
            string extension = Path.GetExtension(path);

            if(!File.Exists(path)) {
                File.Create(path).Close();
            }

            switch (extension) {
                case ".txt":
                    string toSave = "";
                    rectangles.ForEach(r => toSave += r.ToString());
                    File.WriteAllText(path, toSave);
                    break;
                case ".json":
                    string jsonString = JsonConvert.SerializeObject(rectangles);
                    File.WriteAllText(path, jsonString);
                    break;
                case ".xml":
                    using (var fs = new FileStream(path, FileMode.OpenOrCreate)) {
                        new XmlSerializer(typeof(List<Rectangle>)).Serialize(fs, rectangles);
                    }
                    break;
            }

        }

        public static List<Rectangle> LoadAndDeserializeRectangles(string path)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            if (path == "") return null;

            string extension = Path.GetExtension(path);

            try {
                string fileText = File.ReadAllText(path);
                switch (extension) {
                    case ".txt":
                        string[] splitted = fileText.Split('\n');
                        string toParse = "";
                        for (int i = 4; i < splitted.Length; i += 5) {
                            toParse = splitted[i - 4] + "\n" + splitted[i - 3] + "\n" + splitted[i - 2] + "\n" + splitted[i - 1] + "\n" + splitted[i];
                            rectangles.Add(Rectangle.Parse(toParse));
                        }
                        break;
                    case ".json":
                        rectangles.AddRange(JsonConvert.DeserializeObject<List<Rectangle>>(fileText));
                        break;
                    case ".xml":
                        using (var fs = new FileStream(path, FileMode.Open)) {
                            rectangles.AddRange((IEnumerable<Rectangle>)new XmlSerializer(typeof(List<Rectangle>)).Deserialize(fs));
                        }
                        break;
                }
            } catch(Exception e) {
                return null;
            }

            return rectangles;
        }
    }
}
