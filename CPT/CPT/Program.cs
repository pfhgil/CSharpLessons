using CPT.Player;
using CPT.Windows;

namespace CPT
{
    public class Program
    {
        public static NewPlayerWindow NewPlayerWindow { get; } = new NewPlayerWindow();
        public static MainWindow MainWindow { get; } = new MainWindow();

        public static RecordsTable RecordsTable { get; } = new RecordsTable();

        public static Window currentWindow = NewPlayerWindow;

        public static PlayersStats PlayersStats { get; } = new PlayersStats();

        public static bool shouldExit = false;

        static void Main(string[] args)
        {
            PlayersStats.Load();

            while(!shouldExit) {
                currentWindow.Show();
            }
        }
    }
}