using System;
using static Diary.Program;

namespace Diary.Windows
{
    public class MainWindow : Window
    {
        public void show()
        {
            Console.WriteLine("Текущая дата: " + currentDateTime.Day + "." + currentDateTime.Month + "." + currentDateTime.Year);

            currentDayNotes = notes.FindAll(note => (note.date.Day == currentDateTime.Day &&
            note.date.Month == currentDateTime.Month &&
            note.date.Year == currentDateTime.Year));

            if (currentDayNotes.Count != 0) {
                Console.WriteLine("На этот день есть следующие заметки:");
            }

            string arrow = "";
            for (int i = 0; i < currentDayNotes.Count; i++) {
                arrow = "   ";
                if (i == menuArrowIndex) {
                    arrow = "-> ";
                }
                Console.WriteLine(arrow + (i + 1) + ". " + currentDayNotes[i].name);
            }

            arrow = "   ";
            if (menuArrowIndex == currentDayNotes.Count) {
                arrow = "-> ";
            }
            Console.WriteLine(arrow + (currentDayNotes.Count + 1) + ". Создать заметку.");

            ConsoleKeyInfo keyInf = Console.ReadKey();

            switch (keyInf.Key) {
                case ConsoleKey.LeftArrow:
                    currentDateTime = currentDateTime.AddDays(-1);
                    break;
                case ConsoleKey.RightArrow:
                    currentDateTime = currentDateTime.AddDays(1);
                    break;
                case ConsoleKey.UpArrow:
                    menuArrowIndex--;
                    break;
                case ConsoleKey.DownArrow:
                    menuArrowIndex++;
                    break;
                case ConsoleKey.Enter:
                    if (menuArrowIndex < currentDayNotes.Count) {
                        currentWindow = noteWindow;
                    } else if(menuArrowIndex == currentDayNotes.Count) {
                        currentWindow = createNoteWindow;
                    } else {
                        currentWindow = deleteNoteWindow;
                    }
                    break;
            }

            menuArrowIndex = Math.Max(0, Math.Min(menuArrowIndex, currentDayNotes.Count));
            noteIndex = Math.Max(0, Math.Min(menuArrowIndex, currentDayNotes.Count - 1));
        }
    }
}
