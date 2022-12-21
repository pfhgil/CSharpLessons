using InformationSystem.Main;

namespace InformationSystem
{
    public class Filter
    {
        public enum Mode
        {
            BY_ID,
            BY_NAME,
            BY_PASSWORD,
            BY_ROLE
        }

        public Mode mode = Mode.BY_NAME;
        public string data = "";

        public static bool IsDataCorrect(Mode mode, String data)
        {
            if(mode == Mode.BY_ID)
            {
                return int.TryParse(data, out int n);
            }

            if(mode == Mode.BY_ROLE)
            {
                return Enum.TryParse(data, out User.Role role);
            }

            return true;
        }
    }
}
