using Diary.Windows;

namespace Diary
{
    public class Program
    {
        public static DateTime currentDateTime = DateTime.Now;

        public static MainWindow mainWindow = new MainWindow();
        public static NoteWindow noteWindow = new NoteWindow();
        public static CreateNoteWindow createNoteWindow = new CreateNoteWindow();
        public static DeleteNoteWindow deleteNoteWindow = new DeleteNoteWindow();

        public static Window currentWindow;

        public static List<Note> notes = new List<Note>();
        public static List<Note> currentDayNotes = new List<Note>();

        public static int menuArrowIndex = 0;
        public static int noteIndex = 0;
        public static void Main(string[] agrs)
        {
            Note test = new Note();
            test.name = "тест";
            test.description = "это тестовая заметка. тут ничего нет";

            Note test1 = new Note();
            test1.name = "Выбросить мусор";

            Note test2 = new Note();
            test2.name = "арбуз";
            test2.description = "арбузы ням ням ммм";

            notes.Add(test);
            notes.Add(test1);
            notes.Add(test2);

            currentWindow = mainWindow;

            while(true) {
                if(currentWindow != null) {
                    currentWindow.show();
                }

                Console.Clear();
            }
        }
    }
}