using Tortiki.Windows;

namespace Tortiki
{
    internal class Program
    {
        public static readonly MenuWindow menuWindow = new MenuWindow();
        public static readonly CakeOptionWindow cakeOptionWindow = new CakeOptionWindow();
        public static readonly ThankYouWindow thankYouWindow = new ThankYouWindow();

        public static Window currentWindow;
        static void Main(string[] args)
        {
            currentWindow = menuWindow;

            while(true) {
                currentWindow.Show();
            }
        }
    }
}