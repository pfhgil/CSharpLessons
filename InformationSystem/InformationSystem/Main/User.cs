using InformationSystem.Windows;
using System.Text;

namespace InformationSystem.Main
{
    public class User
    {
        public enum Role
        {
            Common,
            Admin
        }

        public int ID = 0;
        public StringBuilder name = new StringBuilder("");
        public StringBuilder password = new StringBuilder("");
        public Role role = Role.Admin;

        public User() { }
        public User(User user)
        {
            Set(user);
        }

        public void Reset()
        {
            ID = 0;
            name = new StringBuilder();
            password = new StringBuilder();
            role= Role.Admin;
        }

        public void Set(User user)
        {
            ID = user.ID;
            name = new StringBuilder(user.name.ToString());
            password = new StringBuilder(user.password.ToString());
            role = user.role;
        }

        public override string ToString()
        {
            return name.ToString() + "\n" + password.ToString() + "\n" + role.ToString() + "\n";
        }

        public string[] ToStringArray()
        {
            return new string[] {
                ID.ToString(),
                name.ToString(),
                password.ToString(),
                ((int) role).ToString()
            };
        }

        public static User Parse(string str)
        {
            User tmp = new User();
            try
            {
                string[] parameters = str.Split('\n');

                int.TryParse(parameters[0], out tmp.ID);
                tmp.name = new StringBuilder(parameters[1]);
                tmp.password = new StringBuilder(parameters[2]);
                Enum.TryParse(parameters[3], out tmp.role);
            }
            catch (Exception e) { }

            return tmp;
        }

        public bool IsCorrect(out string errorStr)
        {
            if (name.ToString() == "")
            {
                errorStr = "Введите корректное имя!";
                return false;
            }

            if (password.ToString() == "")
            {
                errorStr = "Введите корректный пароль!";
                return false;
            }

            if(!Enum.IsDefined(typeof(Role), role))
            {
                errorStr = "Введена неверная роль!";
                return false;
            }

            errorStr = "";
            return true;
        }
    }
}
