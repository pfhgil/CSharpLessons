using Newtonsoft.Json;

namespace CPT.Player
{
    public class PlayerInfo
    {
        public string name = "";
        // количество символов символов в минуту (chars per minute)
        public float CPM = 0;
        // количество символов символов в секунду (chars per second)
        public float CPS = 0;

        public PlayerInfo()
        {

        }

        public PlayerInfo(PlayerInfo playerInfo)
        {
            name = playerInfo.name;
            CPM = playerInfo.CPM;
            CPS = playerInfo.CPS;
        }
    }
}
