using System.IO;

namespace Tortiki.Windows
{
    public class ThankYouWindow : Window
    {
        public override void Show()
        {
            Console.WriteLine("Спасибо за заказ! (Нажмите любую клавишу)");

            string ordersFilePath = "/История заказов.txt";
            if(!File.Exists(ordersFilePath)) {
                File.Create(ordersFilePath);
            }
            File.AppendAllText(ordersFilePath, "Заказ от " + DateTime.Now + "\n\t" + Program.menuWindow.cakeOrder.ToString() + "\n\tЦена: " + Program.menuWindow.cakeOrder.GetPrice() + "\n\n");
            Program.menuWindow.cakeOrder.Clear();

            Console.ReadKey();

            Console.Clear();

            Program.currentWindow = Program.menuWindow;
        }
    }
}
