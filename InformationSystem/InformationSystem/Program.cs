using InformationSystem.Main;
using InformationSystem.Windows;

namespace InformationSystem
{
    internal class Program
    {
        public static MainWindow MainWindow { get; } = new MainWindow();
        public static AuthWindow AuthWindow { get; } = new AuthWindow();
        public static AdminWindow AdminWindow { get; } = new AdminWindow();

        public static UserEditWindow UserEditWindow { get; } = new UserEditWindow();

        public static FilterChooseWindow FilterChooseWindow { get; } = new FilterChooseWindow();

        public static FilterInputWindow FilterInputWindow { get; } = new FilterInputWindow();

        public static Window currentWindow = MainWindow;

        public static bool programActive = true;

        static void Main(string[] args)
        {
            Scheme.Load();

            while(programActive)
            {
                currentWindow.Show();
            }
        }
    }
}