using Editor;
using InformationSystem.Main;
using System.Diagnostics.Metrics;
using static InformationSystem.Windows.AuthWindow;

namespace InformationSystem.Windows
{
    public class UserEditWindow : Window
    {
        public enum Mode
        {
            NEW_USER,
            EDIT_USER
        }

        private User _editableUser;
        public User EditableUser
        {
            get
            {
                return _editableUser;
            }
            set 
            {
                _editableUser = value;
                _editableUserCopy.Set(value);
            }
        }
        private User _editableUserCopy = new User();

        public Mode mode = Mode.NEW_USER;

        private string _errorStr = "";

        public override void Show()
        {
            PrintError(_errorStr, 8);

            string info = "";
            if(mode == Mode.NEW_USER)
            {
                info = "Создание пользователя\n";
            }
            else
            {
                info = "Изменение пользователя\n";
            }
            info += "1 - ая строка - ID, 2 - ая строка - имя, 2 - ая строка - пароль, 3 - ая строка - роль (0 - Common, 1 - Admin)\nНажмите Escape, чтобы выйти и Enter, чтобы применить изменения";
            ConsoleKey key = ConsoleEditor.EditUsers(info, new List<User>() { _editableUserCopy });

            switch(key)
            {
                case ConsoleKey.Enter:
                    bool correct = _editableUserCopy.IsCorrect(out _errorStr);

                    if (correct)
                    {
                        var foundUser = Scheme.GetInstance().AllUsers.Find(u => u.ID == _editableUserCopy.ID && u != _editableUser);

                        if (foundUser != null)
                        {
                            _errorStr = "Пользователь с таким ID уже существует!";
                            break;
                        }

                        foundUser = Scheme.GetInstance().AllUsers.Find(u => u.name.ToString() == _editableUserCopy.name.ToString() && u != _editableUser);

                        if (foundUser != null)
                        {
                            _errorStr = "Пользователь с таким именем уже существует!";
                            break;
                        }             

                        if (mode == Mode.EDIT_USER)
                        {
                            _editableUser.Set(_editableUserCopy);
                        }
                        else
                        {
                            Scheme.GetInstance().AllUsers.Add(new User(_editableUserCopy));
                        }

                        _editableUserCopy.Reset();
                        Program.currentWindow = Program.AdminWindow;

                        Scheme.Save();
                    }
                    break;
                case ConsoleKey.Escape:
                    _editableUserCopy.Reset();
                    Program.currentWindow = Program.AdminWindow;
                    break;
            }

            Clear();
        }
    }
}
