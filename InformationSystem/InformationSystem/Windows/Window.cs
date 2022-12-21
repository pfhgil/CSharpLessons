using System.Drawing;

namespace InformationSystem.Windows
{
    public class Window
    {
        protected int _cursorMinX = 0;
        protected int _cursorMinY = 0;

        protected int _cursorMaxX = 0;

        protected int _cursorX = 0;
        protected int _cursorY = 0;

        protected int _maxLinesNum = 0;

        public virtual void Show()
        {

        }

        public virtual void Clear()
        {
            /*
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < _maxLinesNum; i++)
            {
                Console.WriteLine("\t\t\t\t\t\t\t\t");
            } 
            Console.SetCursorPosition(0, 0);
            */
            Console.Clear();
        }

        protected void CalculateCursorPos(ConsoleKey key, string[] infoArr)
        {
            switch(key)
            {
                case ConsoleKey.UpArrow:
                    _cursorY--;
                    break;

                case ConsoleKey.DownArrow:
                    _cursorY++;
                    break;

                case ConsoleKey.LeftArrow:
                    _cursorX--;
                    break;

                case ConsoleKey.RightArrow:
                    _cursorX++;
                    break;
            }

            if (infoArr.Length > 0)
            {
                _cursorY = Math.Clamp(_cursorY, _cursorMinY, _cursorMinY + infoArr.Length - 1);
                _cursorX = Math.Clamp(_cursorX, _cursorMinX, _cursorMaxX + infoArr[_cursorY].Length);
            }
        }

        protected void PrintError(string msg, int posY)
        {
            int _lastCurX = Console.CursorLeft;
            int _lastCurY = Console.CursorTop;
            Console.SetCursorPosition(0, posY);
            Console.WriteLine(msg);
            Console.SetCursorPosition(_lastCurX, _lastCurY);
        }
    }
}