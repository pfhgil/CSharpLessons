namespace Editor.Windows
{
    public class FileOperationsWindow : Window
    {
        public enum WindowMode
        {
            SAVE_FILE,
            OPEN_FILE
        }

        public WindowMode mode = WindowMode.OPEN_FILE;

        public override void Show()
        {
            if(mode == WindowMode.SAVE_FILE) {
                Console.WriteLine("Введите путь, по которому хотите сохранить файл с описанием прямоугольников (ничего не вводите и нажмите Enter чтобы выйти):");
                Serializer.SerializeAndSaveRectangles(Console.ReadLine(), Program.RectanglesWindow.Rectangles); 
            } else {
                Console.WriteLine("Введите путь, по которому хотите открыть файл с описанием прямоугольников (ничего не вводите и нажмите Enter чтобы выйти):"); 
                List<Rectangle> rectangles = Serializer.LoadAndDeserializeRectangles(Console.ReadLine());
                Program.RectanglesWindow.Rectangles.Clear();
                Program.RectanglesWindow.Rectangles.AddRange(rectangles);
            }

            Console.Clear();
            Program.currentWindow = Program.MainWindow;
        }
    }
}
