using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Editor.Windows
{
    public class RectanglesWindow : Window
    {
        public enum WindowMode
        {
            SHOW_RECTANGLES,
            NEW_RECTANGLE
        }

        private WindowMode mode = WindowMode.NEW_RECTANGLE;

        public WindowMode Mode
        {
            get { return mode; }
            set {
                ConsoleEditor.Reset();
                mode = value;
            }
        }

        public List<Rectangle> Rectangles { get; } = new List<Rectangle>();
        private Rectangle tmpRectangle = new Rectangle();

        public override void Show()
        {
            string info = "";
            ConsoleKey key;
            if(mode == WindowMode.NEW_RECTANGLE) {
                info = "Нажмите Escape для перехода в главное меню и Enter для создания прямоугольника\n" +
                    "Создание прямоугольника:\n" +
                    "-------------------------------\n";

                key = ConsoleEditor.EditRectanglesParams(info, new List<Rectangle>() { tmpRectangle });
            } else {
                info = "Нажмите Escape для перехода в главное меню\n" +
                    "Редактирование прямоугольников:\n" +
                    "-------------------------------\n";

                key = ConsoleEditor.EditRectanglesParams(info, Rectangles);
            }

            switch(key) {
                case ConsoleKey.Escape:
                    Program.currentWindow = Program.MainWindow;
                    break;
                case ConsoleKey.Enter:
                    if (mode == WindowMode.NEW_RECTANGLE) {
                        Rectangles.Add(new Rectangle(tmpRectangle));
                        tmpRectangle.Reset();
                        Program.currentWindow = Program.MainWindow;
                    }
                    break;
            }

            Console.Clear();
        }
    }
}
