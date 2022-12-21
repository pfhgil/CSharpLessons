using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationSystem.Windows
{
    public class FilterInputWindow : Window
    {
        public Filter.Mode mode = Filter.Mode.BY_NAME;
        private string _errorStr = "";

        public override void Show()
        {
            PrintError(_errorStr, 3);

            Console.WriteLine("Введите значение фильтра (нажмите Escape, чтобы вернуться): ");
            string filterData = Console.ReadLine();

            ConsoleKey key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.Enter:
                    if (Filter.IsDataCorrect(mode, filterData))
                    {
                        Program.AdminWindow.Filter.mode = mode;
                        Program.AdminWindow.Filter.data = filterData;
                        Program.currentWindow = Program.AdminWindow;
                    }
                    else
                    {
                        _errorStr = "Введено неверное значение!";
                    }
                    break;
                case ConsoleKey.Escape:
                    Program.currentWindow = Program.FilterChooseWindow;
                    break;
            }
            
            Clear();
        }
    }
}
