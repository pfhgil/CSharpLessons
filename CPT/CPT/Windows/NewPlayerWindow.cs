using CPT.Player;

namespace CPT.Windows
{
    public class NewPlayerWindow : Window
    {
        public PlayerInfo CurrentPlayerInfo { get; } = new PlayerInfo();

        public override void Show()
        {
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            CurrentPlayerInfo.name = name;

            Console.Write("Введите путь до файла для теста: ");
            string testFilePath = Console.ReadLine();

            if (File.Exists(testFilePath)) {
                Program.MainWindow.testText = File.ReadAllText(testFilePath);
                Program.currentWindow = Program.MainWindow;                
            } else {      
                Console.WriteLine("\nВведен неверный путь!");
                Console.Write("Нажмите Enter для продолжения...");
                Console.ReadLine();
            }

            Clear();
        }
    }
}
