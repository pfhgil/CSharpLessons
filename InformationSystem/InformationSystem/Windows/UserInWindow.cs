using InformationSystem.InformationSystem;

namespace InformationSystem.Windows
{
    public class UserInWindow : Window
    {
        public enum Mode
        {
            LOGIN,
            SIGNUP
        }

        private string[] _info =
        {
            "Логин: ",
            "Пароль: "
        };

        private Mode _mode = Mode.SIGNUP;
        public Mode UserInMode
        {
            get { return _mode; }
            set
            {
                _mode = value;

                if(_mode == Mode.LOGIN)
                {
                    _info = new string[]
                    {
                        "Логин: ",
                        "Пароль: "
                    };
                }
                else
                {
                    _info = new string[]
                    {
                        "Логин: ",
                        "Пароль: ",
                        "Повторите пароль: "
                    };
                }
            }
        }

        private User _currentUser = new User();

        public override void Show()
        {
            for (int i = 0; i < _info.Length; i++)
            {
                Console.WriteLine(_info[i]);
            }

            // какаха
            Console.SetCursorPosition(_info[0].Length, 0);
            Console.Write(_currentUser.name.ToString());
            Console.SetCursorPosition(_info[1].Length, 1);
            Console.Write(_currentUser.password.ToString());

            _maxLinesNum = _info.Length;

            Console.SetCursorPosition(_cursorX, _cursorY);

            ConsoleKeyInfo keyInfo = Console.ReadKey(false);
            CalculateCursorPos(keyInfo.Key, _info);

            _cursorMinX = _info[_cursorY].Length;
            // какаха
            if (_cursorY == 0)
            {
                _cursorX = Math.Clamp(_cursorX, _cursorMinX, _cursorMinX + _currentUser.name.Capacity + 1);
                _currentUser.name.Insert(_cursorX - _cursorMinX, keyInfo.KeyChar);  
            } 
            else if(_cursorY == 1) 
            {
                _cursorX = Math.Clamp(_cursorX, _cursorMinX, _cursorMinX + _currentUser.password.Capacity + 1);
                _currentUser.password.Insert(_cursorX - _cursorMinX, keyInfo.KeyChar);        
            }
            else if(_cursorY == 3 && _mode == Mode.SIGNUP)
            {
                //_cursorX = Math.Clamp(_cursorX, _cursorMinX, _cursorMinX + _currentUser.password.Length + 1);
                _currentUser.name.Insert(_cursorX, keyInfo.KeyChar);
            }

            Clear();
        }
    }
}
