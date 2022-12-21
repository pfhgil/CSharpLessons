using InformationSystem.Main;

namespace InformationSystem.Windows
{
    public class AdminWindow : Window
    {
        public Filter Filter { get; } = new Filter();

        public override void Show()
        {
            Console.WriteLine("Информация о вас: \n" +
                "Псевдоним: " + Program.AuthWindow.CurrentUser.name.ToString() + "\n" +
                "Роль: " + Program.AuthWindow.CurrentUser.role.ToString());

            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Для создания пользователя нажмите F1,\n" +
                "для удаления пользователя нажмите F2 (вы не можете удалить сами себя!),\n" +
                "для редактирования пользователя нажмите F3,\n" +
                "для фильтрации пользователей нажмите F4\n" +
                "для выхода в главное меню нажмите Escape.");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Весь список пользователей:");

            List<User> allUsers = Scheme.GetInstance().AllUsers;
           
            if(Filter.data != "")
            {
                switch(Filter.mode)
                {
                    case Filter.Mode.BY_ID:
                        int idToFilter = 0;
                        if (int.TryParse(Filter.data, out idToFilter))
                        {
                            User foundUser = allUsers.Find(u => u.ID == idToFilter);
                            allUsers = foundUser == null ? new List<User>() : new List<User> { foundUser };
                        }
                        break;

                    case Filter.Mode.BY_NAME:
                        allUsers = allUsers.FindAll(u => u.name.ToString().ToLower().Contains(Filter.data.ToLower()));
                        break;

                    case Filter.Mode.BY_PASSWORD:
                        allUsers = allUsers.FindAll(u => u.password.ToString().ToLower().Contains(Filter.data.ToLower()));
                        break;

                    case Filter.Mode.BY_ROLE:
                        User.Role roleToFilter = 0;
                        if (Enum.TryParse(Filter.data, out roleToFilter))
                        {
                            allUsers = allUsers.FindAll(u => u.role == roleToFilter);
                        }
                        break;
                }           
            }          

            string[] userInfoArr = new string[allUsers.Count];
            for (int i = 0; i < allUsers.Count; i++)
            {
                userInfoArr[i] = "Псевдоним: " + allUsers[i].name + ", роль: " + allUsers[i].role.ToString() + ", ID: " + allUsers[i].ID;

                if (i == _cursorY)
                {
                    Console.Write("-> ");
                }

                Console.WriteLine(i + ") " + userInfoArr[i]);
            }

            _maxLinesNum = allUsers.Count; 

            ConsoleKey key = Console.ReadKey(false).Key;
            CalculateCursorPos(key, userInfoArr);

            switch(key)
            {
                case ConsoleKey.F1:
                    User newUser = new User();
                    newUser.ID = Scheme.GetInstance().AllUsers[Scheme.GetInstance().AllUsers.Count - 1].ID + 1;
                    Program.UserEditWindow.EditableUser = newUser;
                    Program.UserEditWindow.mode = UserEditWindow.Mode.NEW_USER;
                    Program.currentWindow = Program.UserEditWindow;
                    break;
                case ConsoleKey.F2:
                    if (allUsers.Count > 0 && allUsers[_cursorY].name.ToString() != Program.AuthWindow.CurrentUser.name.ToString())
                    {
                        allUsers.RemoveAt(_cursorY);
                        Scheme.Save();
                    }
                    break;
                case ConsoleKey.F3:
                    if (allUsers.Count > 0)
                    {
                        Program.UserEditWindow.EditableUser = allUsers[_cursorY];
                        Program.UserEditWindow.mode = UserEditWindow.Mode.EDIT_USER;
                        Program.currentWindow = Program.UserEditWindow;
                    }
                    break;
                case ConsoleKey.F4:
                    Program.currentWindow = Program.FilterChooseWindow;
                    break;
                case ConsoleKey.Escape:
                    Program.currentWindow = Program.MainWindow;
                    break;
            }

            Clear();
        }
    }
}
