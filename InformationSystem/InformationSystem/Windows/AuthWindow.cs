namespace InformationSystem.Windows
{
    public class AuthWindow : Window
    {
        private string[] _info = {
            "Войти в систему",
            "Зарегистрироваться в системе",
            "Выйти"
        };

        public override void Show()
        {
            for(int i = 0; i < _info.Length; i++)
            {
                if(i == _cursorY)
                {
                    Console.Write("-> ");
                }
                Console.WriteLine(i + ") " + _info[i]);
            }

            _maxLinesNum = _info.Length;

            ConsoleKey key = Console.ReadKey(false).Key;
            CalculateCursorPos(key, _info);

            if (key == ConsoleKey.Enter)
            {
                switch (_cursorY)
                {    
                    case 0:
                        Program.UserInWindow.UserInMode = UserInWindow.Mode.LOGIN;
                        Program.currentWindow = Program.UserInWindow;
                        break;

                    case 1:
                        Program.UserInWindow.UserInMode = UserInWindow.Mode.SIGNUP;
                        Program.currentWindow = Program.UserInWindow;
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
