using InformationSystem.Windows;
using System.Text;

namespace InformationSystem.Main
{
    public class UserAuthData
    {
        public StringBuilder name = new StringBuilder();
        public StringBuilder password = new StringBuilder();
        public StringBuilder verifyPassword = new StringBuilder();

        public void Reset()
        {
            name = new StringBuilder();
            password = new StringBuilder();
            verifyPassword = new StringBuilder();
        }

        public void Set(UserAuthData userAuthData)
        {
            name =new StringBuilder(userAuthData.name.ToString());
            password = new StringBuilder(userAuthData.password.ToString());
            verifyPassword = new StringBuilder(userAuthData.verifyPassword.ToString());
        }

        public override string ToString()
        {
            return name.ToString() + "\n" + password.ToString() + "\n" + verifyPassword.ToString() + "\n";
        }

        public string[] ToStringArray()
        {
            return new string[] {
                name.ToString(),
                password.ToString(),
                verifyPassword.ToString()
            };
        }

        public static UserAuthData Parse(string str)
        {
            UserAuthData tmp = new UserAuthData();
            try
            {
                string[] parameters = str.Split('\n');

                tmp.name = new StringBuilder(parameters[0]);
                tmp.password = new StringBuilder(parameters[1]);
                tmp.verifyPassword = new StringBuilder(parameters[2]);
            }
            catch (Exception e) { }

            return tmp;
        }

        public bool IsCorrect(AuthWindow.Mode mode, out string errorStr)
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

            if(mode == AuthWindow.Mode.SIGNUP)
            {
                if(verifyPassword.ToString() != password.ToString())
                {
                    errorStr = "Пароли не совпадают!";
                    return false;
                }
            }

            errorStr = "";
            return true;
        }
    }
}
