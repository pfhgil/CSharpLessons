using static Diary.Program;

namespace Diary.Windows
{
    public class CreateNoteWindow: Window
    {

        private Note note = new Note();
        public void show()
        {
            Console.WriteLine(
                "Создние заметки. Дата: " + currentDateTime.ToString() + "\n\nИмя заметки: ");
            note.date = currentDateTime;
            note.name = Console.ReadLine();
            Console.WriteLine("Описание: ");
            note.description = Console.ReadLine();

            Console.WriteLine("Нажмите Enter для создания заметки или Escape для отмены создания заметки.");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch(keyInfo.Key) {
                case ConsoleKey.Escape:    
                    currentWindow = mainWindow;
                    break;
                case ConsoleKey.Enter:
                    notes.Add(new Note(note));
                    currentWindow = mainWindow;
                    break;
            }

            note.reset();
        }
    }
}
