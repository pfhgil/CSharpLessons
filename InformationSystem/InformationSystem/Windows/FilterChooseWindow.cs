namespace InformationSystem.Windows
{
    public class FilterChooseWindow : Window
    {
        private string[] _infoArr =
        {
            "По ID",
            "По имени",
            "По паролю",
            "По роли",
            "Сбросить фильтр"
        };
        public override void Show()
        {
            Console.WriteLine("Выберите фильтр: ");

            for (int i = 0; i < _infoArr.Length; i++)
            {
                if (i == _cursorY)
                {
                    Console.Write("-> ");
                }

                Console.WriteLine(i + ") " + _infoArr[i]);
            }

            ConsoleKey key = Console.ReadKey(false).Key;
            CalculateCursorPos(key, _infoArr);

            switch(key)
            {
                case ConsoleKey.Enter:
                    switch(_cursorY)
                    { 
                        case 0:
                            Program.FilterInputWindow.mode = Program.AdminWindow.Filter.mode = Filter.Mode.BY_ID;
                            Program.currentWindow = Program.FilterInputWindow;
                            break;
                        case 1:
                            Program.FilterInputWindow.mode = Program.AdminWindow.Filter.mode = Filter.Mode.BY_NAME;
                            Program.currentWindow = Program.FilterInputWindow;
                            break;
                        case 2:
                            Program.FilterInputWindow.mode = Program.AdminWindow.Filter.mode = Filter.Mode.BY_PASSWORD;
                            Program.currentWindow = Program.FilterInputWindow;
                            break;
                        case 3:
                            Program.FilterInputWindow.mode = Program.AdminWindow.Filter.mode = Filter.Mode.BY_ROLE;
                            Program.currentWindow = Program.FilterInputWindow;
                            break;
                        case 4:
                            Program.currentWindow = Program.AdminWindow;
                            break;
                    }

                    Program.AdminWindow.Filter.data = "";
                    break;
                case ConsoleKey.Escape:
                    Program.currentWindow = Program.AdminWindow;
                    break;
            }

            Clear();
        }
    }
}
