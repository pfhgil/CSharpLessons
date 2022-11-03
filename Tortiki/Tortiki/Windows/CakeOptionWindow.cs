using Tortiki.Cake;
using static Tortiki.Utils;

namespace Tortiki.Windows
{
    public class CakeOptionWindow : Window
    {
        public CakeOption cakeOption = CakeOption.FORM;

        private List<CakeOptionValue> optionsValues = new List<CakeOptionValue>();
        private int currentChoose = 0;

        public CakeOptionWindow()
        {
            var cakeOptionValues = Enum.GetValues(typeof(CakeOptionValue));

            foreach(CakeOptionValue cakeOptionValue in cakeOptionValues) {
                optionsValues.Add(cakeOptionValue);
            }
        }

        public override void Show()
        {
            int j = 0;
            int min = 0, max = 0;
            switch(cakeOption) {
                case CakeOption.FORM:
                    min = 0;
                    max = 3;
                    break;
                case CakeOption.SIZE:
                    min = 4;
                    max = 6;
                    break;
                case CakeOption.TASTE:
                    min = 7;
                    max = 11;
                    break;
                case CakeOption.AMOUNT:
                    min = 12;
                    max = 15;
                    break;
                case CakeOption.GLAZE:
                    min = 16;
                    max = 20;
                    break;
                case CakeOption.DECOR:
                    min = 21;
                    max = 23;
                    break;
            };

            for (int i = min; i <= max; i++) {
                if (j == currentChoose) {
                    Console.Write("-> ");
                }
               
                Console.WriteLine(j + ") " + 
                    CakeOptionValueToRussian(optionsValues.ElementAt(i)) + 
                    " - " + 
                    GetCakeOptionValuePrice(optionsValues.ElementAt(i)));

                j++;
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key) {
                case ConsoleKey.UpArrow:
                    currentChoose--;
                    break;
                case ConsoleKey.DownArrow:
                    currentChoose++;
                    break;
                case ConsoleKey.Escape:
                    Program.currentWindow = Program.menuWindow;
                    break;
                case ConsoleKey.Enter:
                    switch (cakeOption) {
                        case CakeOption.FORM:
                            Program.menuWindow.cakeOrder.form = optionsValues[min + currentChoose];
                            break;
                        case CakeOption.SIZE:
                            Program.menuWindow.cakeOrder.size = optionsValues[min + currentChoose];
                            break;
                        case CakeOption.TASTE:
                            Program.menuWindow.cakeOrder.taste = optionsValues[min + currentChoose];
                            break;
                        case CakeOption.AMOUNT:
                            Program.menuWindow.cakeOrder.cakesNum = optionsValues[min + currentChoose];
                            break;
                        case CakeOption.GLAZE:
                            Program.menuWindow.cakeOrder.glaze = optionsValues[min + currentChoose];
                            break;
                        case CakeOption.DECOR:
                            Program.menuWindow.cakeOrder.decor = optionsValues[min + currentChoose];
                            break;
                    };
                    Program.currentWindow = Program.menuWindow;

                    break;
            }

            currentChoose = Math.Max(Math.Min(currentChoose, j - 1), 0);

            Console.Clear();
        }
    }
}
