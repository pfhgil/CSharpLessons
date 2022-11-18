using Editor.Windows;

namespace Editor
{
    public class Program
    {
        public static MainWindow MainWindow { get; } = new MainWindow();
        public static FileOperationsWindow FileOperationsWindow { get; } = new FileOperationsWindow();
        public static RectanglesWindow RectanglesWindow { get; } = new RectanglesWindow();

        public static Window currentWindow = MainWindow;
        public static void Main()
        {
            while(true) {
                currentWindow.Show();
            }
        }
    }
}