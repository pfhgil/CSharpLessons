using static Diary.Program;

namespace Diary.Windows
{
    public class DeleteNoteWindow: Window
    {
        public void show()
        {
            Console.WriteLine(
                "Вы уверены, что хотите удалить заметку \"" + currentDayNotes[noteIndex].name + "\"?\n" +
                "Нажмите Enter для того, чтобы удалить заметку и Escape чтобы отменить удаление."
                );

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.Key) {
                case ConsoleKey.Escape:
                    currentWindow = mainWindow;
                    break;
                case ConsoleKey.Enter:
                    notes.RemoveAt(noteIndex);
                    currentWindow = mainWindow;
                    break;
            }
        }
    }
}
