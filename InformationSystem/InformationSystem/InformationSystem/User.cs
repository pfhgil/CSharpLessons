using System.Text;

namespace InformationSystem.InformationSystem
{
    public class User
    {
        public StringBuilder name = new StringBuilder("");
        public StringBuilder password = new StringBuilder("");
        public string role = Roles.COMMON_USER;
    }
}
