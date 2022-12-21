using Editor;
using InformationSystem.Main;
using System.Text;

namespace InformationSystem.Windows
{
    public class AuthWindow : Window
    {
        public enum Mode
        {
            LOGIN,
            SIGNUP
        }

        public Mode mode = Mode.SIGNUP;

        public User CurrentUser { get; } = new User();

        private UserAuthData _tmpUserAuthData = new UserAuthData();

        private string _errorStr = "";

        public override void Show()
        {
            PrintError(_errorStr, 5);

            string info = "";
            if (mode == Mode.LOGIN)
            {
                info = "Введите логин и пароль (Нажмите Escape, чтобы выйти):";  
            }
            else
            {
                info = "Введите логин, пароль и повторите пароль (Нажмите Escape, чтобы выйти):";
            }

            ConsoleKey key = ConsoleEditor.EditUsersAuthData(info, new List<UserAuthData>() { _tmpUserAuthData }, mode);

            switch (key)
            {
                case ConsoleKey.Escape:
                    Program.currentWindow = Program.MainWindow;
                    _tmpUserAuthData.Reset();
                    ConsoleEditor.Reset();
                    _errorStr = "";               
                    break;

                case ConsoleKey.Enter:
                    bool correct = _tmpUserAuthData.IsCorrect(mode, out _errorStr);

                    if(correct)
                    {
                        CurrentUser.name = new StringBuilder(_tmpUserAuthData.name.ToString());
                        CurrentUser.password = new StringBuilder(_tmpUserAuthData.password.ToString());

                        var foundUser = Scheme.GetInstance().AllUsers.Find(u => u.name.ToString() == _tmpUserAuthData.name.ToString());

                        if (mode == Mode.SIGNUP)
                        {
                            if(foundUser != null)
                            {
                                _errorStr = "Такой пользователь уже существует!";
                                break;
                            }

                            if (Scheme.GetInstance().AllUsers.Count > 0)
                            {
                                CurrentUser.ID = Scheme.GetInstance().AllUsers[Scheme.GetInstance().AllUsers.Count - 1].ID + 1;
                            }

                            Scheme.GetInstance().AllUsers.Add(CurrentUser);
                        }
                        else if(mode == Mode.LOGIN)
                        {
                            if (foundUser == null)
                            {
                                _errorStr = "Такого пользователя не существует!";
                                break;
                            }

                            if(foundUser.password.ToString() != _tmpUserAuthData.password.ToString())
                            {
                                _errorStr = "Введен неверный пароль!";
                                break;
                            }
                        }

                        if(foundUser != null)
                        {
                            CurrentUser.role = foundUser.role;
                        }           

                        Scheme.Save();

                        if (CurrentUser.role == User.Role.Admin)
                        {
                            Program.currentWindow = Program.AdminWindow;
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 6);
                            Console.WriteLine("Вы простой " + CurrentUser.role.ToString() + " и не можете в " + Program.AdminWindow + "! Кликните на любую клавишу...");
                            Console.ReadKey();
                            Program.programActive = false;
                            break;
                        }
                        _tmpUserAuthData.Reset();
                        ConsoleEditor.Reset();
                    } 
                    break;
            }

            Clear();
        }
    }
}
