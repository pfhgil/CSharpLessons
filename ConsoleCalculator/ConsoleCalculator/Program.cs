
namespace ConsoleCalculatorGovnoEdition
{
    public class Calculator
    {
        public static void Main(string[] args)
        {
            while(true) {
                Console.WriteLine("1. Сложить 2 числа\n" +
                    "2. Вычесть первое из второго\n" +
                    "3. Перемножить два числа\n" +
                    "4. Разделить первое на второе\n" +
                    "5. Возвести в степень N первое число\n" +
                    "6. Найти квадратный корень из числа\n" +
                    "7. Найти 1 процент от числа\n" +
                    "8. Найти факториал из числа\n" +
                    "9. Выйти из программы\n\n" +
                    "введите действие: ");        
                try {
                    int chosen = Convert.ToInt16(Console.ReadLine());
                    Console.WriteLine("магическим образом получается результат: " + solve(chosen) + "\n");
                } catch(Exception e) {
                    Console.WriteLine("ну ты глупый пользователь, тебе сказали число ввести. давай по новой");
                }
            }
        }

        private static float solve(int chosenVar)
        {
            float res = 0.0f;

            float a = 0.0f, b = 0.0f;

            switch(chosenVar) {    
                case 6: case 7: case 8:
                    try {
                        Console.WriteLine("введите первое число: ");
                        a = float.Parse(Console.ReadLine());
                    } catch (Exception e) {
                        Console.WriteLine("вы болван!!! первое число не число!!");
                    }
                    break;

                case 1: case 2: case 3: case 4: case 5:
                    try {
                        Console.WriteLine("введите первое число: ");
                        a = float.Parse(Console.ReadLine());
                        Console.WriteLine("введите второе число: ");
                        b = float.Parse(Console.ReadLine());
                    } catch (Exception e) {
                        Console.WriteLine("вы болван!!! второе число не число!!");
                    }
                    break;
            }

            if(chosenVar == 1) {
                res = a + b;
            } else if(chosenVar == 2) {
                res = a - b;
            } else if(chosenVar == 3) {
                res = a * b;
            } else if(chosenVar == 4) {
                res = a / b;
            } else if(chosenVar == 5) {
                res = (float) Math.Pow((double) a, (double) b);
            } else if(chosenVar == 6) {
                res = (float) Math.Sqrt((double) a);
            } else if(chosenVar == 7) {
                res = a / 100.0f;
            } else if(chosenVar == 8) {
                Console.WriteLine("факториал предполагает округление первого введённого числа!");
                res = 1.0f;
                for(int i = 1; i <= Math.Round(a); i++) {
                    res *= i;
                }
            } else if(chosenVar == 9) {
                System.Environment.Exit(130);
            }

            return res;
        }
    }
}