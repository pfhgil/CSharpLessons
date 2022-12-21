using System.Drawing;
using System.Text;
using InformationSystem.Main;
using InformationSystem.Windows;

namespace Editor
{
    public class ConsoleEditor
    {
        private static int cursorPosX, cursorPosY, currentPosInList;

        public static void Reset()
        {
            cursorPosX = cursorPosY = currentPosInList = 0;
        }

        // =)
        public static ConsoleKey EditUsersAuthData(string addictionInfo, List<UserAuthData> usersAuthData, AuthWindow.Mode mode)
        {        
            if (usersAuthData.Count == 0) return ConsoleKey.Escape;  

            int nNum = addictionInfo.Split('\n').Length;
            Console.WriteLine(addictionInfo);

            List<string> splitted = new List<string>();
            usersAuthData.ForEach(u => splitted.AddRange(u.ToStringArray().ToList()));
            for(int i = 0; i < splitted.Count; i++)
            {
                if (i == 1 || i == 2)
                {
                    foreach (char c in splitted[i])
                    {
                        Console.Write("*");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(splitted[i]);
                }
            }

            Console.SetCursorPosition(cursorPosX, nNum + cursorPosY);

            StringBuilder currentString = new StringBuilder(splitted[cursorPosY]);
            int lastCursorPosY = cursorPosY;
           
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key) {
                case ConsoleKey.UpArrow:
                    cursorPosY--;
                    break;
                case ConsoleKey.DownArrow:
                    cursorPosY++;
                    break;
                case ConsoleKey.LeftArrow:
                    cursorPosX--;
                    break;
                case ConsoleKey.RightArrow:
                    cursorPosX++;
                    break;
                case ConsoleKey.Backspace:
                    if (currentString.Length == 0) break;
                    currentString.Remove(cursorPosX, 1);
                    cursorPosX--;
                    break;
                case ConsoleKey n when n != ConsoleKey.Escape && n != ConsoleKey.Backspace && n != ConsoleKey.NoName && n != ConsoleKey.Enter:
                    if (cursorPosX < currentString.Length - 1) {
                        currentString.Insert(cursorPosX + 1, key.KeyChar);   
                    } else  {
                        currentString.Append(key.KeyChar);
                    }
                    cursorPosX++;
                    break;
            }

            splitted[lastCursorPosY] = currentString.ToString();

            cursorPosY = Math.Max(0, Math.Min(cursorPosY, splitted.Count - 1));
            cursorPosX = Math.Max(0, Math.Min(cursorPosX, splitted[cursorPosY].Length - 1));

            int linesNum = 2;
            if(mode == AuthWindow.Mode.SIGNUP)
            {
                linesNum = 3;
            }
            currentPosInList = cursorPosY / linesNum;
            string result = "";
            int posInSplitted = currentPosInList * linesNum;
            for (int i = posInSplitted; i < posInSplitted + linesNum; i++) {
                result += splitted[i] + "\n";
            }
            if (usersAuthData.Count != 0) {
                usersAuthData[currentPosInList].Set(UserAuthData.Parse(result));
            }

            return key.Key;
        }

        // =)
        public static ConsoleKey EditUsers(string addictionInfo, List<User> users)
        {
            if (users.Count == 0) return ConsoleKey.Escape;

            int nNum = addictionInfo.Split('\n').Length;
            Console.WriteLine(addictionInfo);

            List<string> splitted = new List<string>();
            users.ForEach(r => splitted.AddRange(r.ToStringArray().ToList()));
            splitted.ForEach(s => Console.WriteLine(s));

            Console.SetCursorPosition(cursorPosX, nNum + cursorPosY);

            StringBuilder currentString = new StringBuilder(splitted[cursorPosY]);
            int lastCursorPosY = cursorPosY;

            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    cursorPosY--;
                    break;
                case ConsoleKey.DownArrow:
                    cursorPosY++;
                    break;
                case ConsoleKey.LeftArrow:
                    cursorPosX--;
                    break;
                case ConsoleKey.RightArrow:
                    cursorPosX++;
                    break;
                case ConsoleKey.Backspace:
                    if (currentString.Length == 0) break;
                    currentString.Remove(cursorPosX, 1);
                    cursorPosX--;
                    break;
                case ConsoleKey n when n != ConsoleKey.Escape && n != ConsoleKey.Backspace && n != ConsoleKey.NoName && n != ConsoleKey.Enter:
                    if (cursorPosX < currentString.Length - 1)
                    {
                        currentString.Insert(cursorPosX + 1, key.KeyChar);
                    }
                    else
                    {
                        currentString.Append(key.KeyChar);
                    }
                    cursorPosX++;
                    break;
            }

            splitted[lastCursorPosY] = currentString.ToString();

            cursorPosY = Math.Max(0, Math.Min(cursorPosY, splitted.Count - 1));
            cursorPosX = Math.Max(0, Math.Min(cursorPosX, splitted[cursorPosY].Length - 1));

            int linesNum = 4;
            currentPosInList = cursorPosY / linesNum;
            string result = "";
            int posInSplitted = currentPosInList * linesNum;
            for (int i = posInSplitted; i < posInSplitted + linesNum; i++)
            {
                result += splitted[i] + "\n";
            }
            if (users.Count != 0)
            {
                users[currentPosInList].Set(User.Parse(result));
            }

            return key.Key;
        }
    }
}
