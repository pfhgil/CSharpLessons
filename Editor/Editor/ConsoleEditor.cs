using System.Text;

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
        public static ConsoleKey EditRectanglesParams(string addictionInfo, List<Rectangle> rectangles)
        {        
            if (rectangles.Count == 0) return ConsoleKey.Escape;  

            int nNum = addictionInfo.Split('\n').Length;
            Console.WriteLine(addictionInfo);

            List<string> splitted = new List<string>();
            rectangles.ForEach(r => splitted.AddRange(r.ToStringArray().ToList()));
            splitted.ForEach(s => Console.WriteLine(s));

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

            int linesNum = 5;
            currentPosInList = cursorPosY / linesNum;
            string result = "";
            int posInSplitted = currentPosInList * linesNum;
            for (int i = posInSplitted; i < posInSplitted + linesNum; i++) {
                result += splitted[i] + "\n";
            }
            if (rectangles.Count != 0) {
                rectangles[currentPosInList].Set(Rectangle.Parse(result));
            }

            return key.Key;
        }
    }
}
