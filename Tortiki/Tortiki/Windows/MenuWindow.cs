using Tortiki.Cake;

namespace Tortiki.Windows
{
    public class MenuWindow : Window
    {
        private int currentChoose = 0;
        private CakeOption[] cakeOptions = new CakeOption[] {
            CakeOption.FORM,
            CakeOption.SIZE,
            CakeOption.TASTE,
            CakeOption.AMOUNT,
            CakeOption.GLAZE,
            CakeOption.DECOR
        };

        public readonly CakeOrder cakeOrder = new CakeOrder();

        public override void Show()
        {
            Console.WriteLine("Заказ тортов в ТОРТИКИ. Торты на ваш выбор!\nВыберите параметры торта.\n-----------------");

            for(int i = 0; i < cakeOptions.Length + 2; i++) {
                if(i == currentChoose) {
                    Console.Write("-> ");
                }
                if (i < cakeOptions.Length) {
                    Console.WriteLine(i + ") " + Utils.CakeOptionToRussian(cakeOptions[i]));
                } else if(i == cakeOptions.Length) {
                    Console.WriteLine(i + ") Заказать!");
                } else { 
                    Console.WriteLine(i + ") Сбросить параметры тортика!\n");
                }
            }

            Console.WriteLine("Цена вопроса: " + cakeOrder.GetPrice() + "\nВаш тортик: " + cakeOrder.ToString());

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch(keyInfo.Key) {
                case ConsoleKey.UpArrow:
                    currentChoose--;
                    break;
                case ConsoleKey.DownArrow:
                    currentChoose++;
                    break;
                case ConsoleKey.Enter:
                    if (currentChoose < cakeOptions.Length) {
                        Program.cakeOptionWindow.cakeOption = cakeOptions[currentChoose];
                        Program.currentWindow = Program.cakeOptionWindow;
                    } else if(currentChoose == cakeOptions.Length) {
                        if (cakeOrder.GetPrice() != 0) {
                            Program.currentWindow = Program.thankYouWindow;
                        } 
                    } else {
                        cakeOrder.Clear();
                    }
                    break;
            }

            currentChoose = Math.Max(Math.Min(currentChoose, cakeOptions.Length + 1), 0);

            Console.Clear();
        }
    }
}
