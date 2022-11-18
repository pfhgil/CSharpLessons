namespace Editor.Windows
{
    public class MainWindow : Window
    {
        private int currentChoose = 0;
        private string[] variants = {
            "Открыть файл",
            "Сохранить файл",
            "Посмотреть текущие прямоугольники",
            "Создать новый прямоугольник"
        };
        public override void Show()
        {
            Console.WriteLine("F1 - {0}, F2 - {1}, F3 - {2}, F4 - {3}, Escape - выйти\nВыберите пункт: ", 
                variants[0],
                variants[1],
                variants[2],
                variants[3]);
            for(int i = 0; i < variants.Length; i++) {
                string res = i + ". " + variants[i];
                if(currentChoose == i) {
                    res = "-> " + res;
                }
                Console.WriteLine(res);
            }

            ConsoleKeyInfo key = Console.ReadKey();
            int dif = (ConsoleKey.F4 - ConsoleKey.F1) - (ConsoleKey.F4 - key.Key);
            if (dif >= 0) {
                currentChoose = dif;
                ChangeWindow();
            } else {
                switch (key.Key) {
                    case ConsoleKey.UpArrow:
                        currentChoose--;
                        break;
                    case ConsoleKey.DownArrow:
                        currentChoose++;
                        break;
                    case ConsoleKey.Enter:
                        ChangeWindow();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(130);
                        break;
                }
            }

            currentChoose = Math.Max(0, Math.Min(currentChoose, variants.Length - 1));

            Console.Clear(); 
        }

        private void ChangeWindow()
        {
            switch (currentChoose) {
                case 0:
                    Program.FileOperationsWindow.mode = FileOperationsWindow.WindowMode.OPEN_FILE;
                    Program.currentWindow = Program.FileOperationsWindow;
                    break;
                case 1:
                    Program.FileOperationsWindow.mode = FileOperationsWindow.WindowMode.SAVE_FILE;
                    Program.currentWindow = Program.FileOperationsWindow;
                    break;
                case 2:
                    Program.RectanglesWindow.Mode = RectanglesWindow.WindowMode.SHOW_RECTANGLES;
                    Program.currentWindow = Program.RectanglesWindow;
                    break;
                case 3:
                    Program.RectanglesWindow.Mode = RectanglesWindow.WindowMode.NEW_RECTANGLE;
                    Program.currentWindow = Program.RectanglesWindow;
                    break;
            }
        }
    }
}
