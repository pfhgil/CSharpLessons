namespace InformationSystem.InformationSystem
{
    public class InformationSystem
    {
        private static InformationSystem _instance;



        public static InformationSystem GetInstance()
        {
            return _instance == null ? _instance = new InformationSystem() : _instance;
        }
    }
}
