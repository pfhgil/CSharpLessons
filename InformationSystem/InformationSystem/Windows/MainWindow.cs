namespace InformationSystem.Windows
{
    public class MainWindow : Window
    {
        private string[] _infoArr = {
            "Войти в систему",
            "Зарегистрироваться в системе",
            "Выйти"
        };

        public override void Show()
        {
            for(int i = 0; i < _infoArr.Length; i++)
            {
                if(i == _cursorY)
                {
                    Console.Write("-> ");
                }
                Console.WriteLine(i + ") " + _infoArr[i]);
            }

            _maxLinesNum = _infoArr.Length;

            ConsoleKey key = Console.ReadKey(false).Key;
            CalculateCursorPos(key, _infoArr);

            if (key == ConsoleKey.Enter)
            {
                switch (_cursorY)
                {    
                    case 0:
                        Program.AuthWindow.mode = AuthWindow.Mode.LOGIN;
                        Program.currentWindow = Program.AuthWindow;
                        break;

                    case 1:
                        Program.AuthWindow.mode = AuthWindow.Mode.SIGNUP;
                        Program.currentWindow = Program.AuthWindow;
                        break;

                    case 2:
                        Program.programActive = false;
                        break;
                }
            }

            Clear();
        }
    }
}
