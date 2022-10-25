using static Diary.Program;

namespace Diary.Windows
{
    public class NoteWindow: Window
    {
        public void show()
        {
            Console.WriteLine(
                        "Информация о заметке.\n" +
                        "Дата: " + currentDayNotes[noteIndex].date.ToString() + "\n" +
                        "Имя: " + currentDayNotes[noteIndex].name + "\n" +
                        "Описание: " + currentDayNotes[noteIndex].description + "\n\n" +
                        "Нажмите Escape, чтобы вернуться обратно. Нажмите Delete или F6 чтобы удалить заметку.");

            ConsoleKeyInfo keyInf = Console.ReadKey();

            switch (keyInf.Key) {
                case ConsoleKey.Escape:
                    currentWindow = mainWindow;
                    break;

                case ConsoleKey.Delete:
                case ConsoleKey.F6:
                    currentWindow = deleteNoteWindow; 
                    break;
            }
        }
    }
}
