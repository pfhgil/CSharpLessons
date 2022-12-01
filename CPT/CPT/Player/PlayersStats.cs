using Newtonsoft.Json;

namespace CPT.Player
{
    public class PlayersStats
    {
        private string _path = "D:\\PlayersStats.txt";

        public List<PlayerInfo> PlayersInfos { get; } = new List<PlayerInfo>();

        public void Save()
        {
            if (!File.Exists(_path)) {
                File.Create(_path).Close();
            }
            File.WriteAllText(_path, JsonConvert.SerializeObject(this));
        }

        public void Load()
        {
            if (File.Exists(_path)) {
                Set(JsonConvert.DeserializeObject<PlayersStats>(File.ReadAllText(_path)));
            }
        }

        public void Set(PlayersStats playersStats)
        {
            PlayersInfos.Clear();

            if (playersStats != null) {
                PlayersInfos.AddRange(playersStats.PlayersInfos);
            }
        }
    }
}
