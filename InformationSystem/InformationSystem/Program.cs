using InformationSystem.Windows;

namespace InformationSystem
{
    internal class Program
    {
        public static AuthWindow AuthWindow { get; } = new AuthWindow();
        public static UserInWindow UserInWindow { get; } = new UserInWindow();

        public static Window currentWindow = AuthWindow;

        public static bool programActive = true;

        static void Main(string[] args)
        {
            while(programActive)
            {
                currentWindow.Show();
            }
        }
    }
}