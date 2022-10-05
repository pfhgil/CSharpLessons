using System;
using System.Linq;

namespace Games
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(
                    "1) Угадай число\n" +
                    "2) Таблица умножения\n" +
                    "3) Вывод делителей числа\n" +
                    "4) Выход из программы\n" +
                    "в какую игру хочешь поиграть? введи пункт!"
                    );

                int chosen;
                bool parsed = int.TryParse(Console.ReadLine(), out chosen);

                if (parsed) {
                    switch (chosen) {
                        case 1:
                            GuessANumber();
                            break;
                        case 2:
                            PrintMulTable();
                            break;
                        case 3:
                            PrintAllDividers();
                            break;
                        case 4:
                            Environment.Exit(130);
                            break;
                    }
                } else {
                    Console.WriteLine("не понял! введи чисо!!!!))))");
                }
            }
        }

        private static void GuessANumber()
        {
            Random rnd = new Random();

            int num = 0;
            int rndNum = rnd.Next(0, 100);
            Console.WriteLine("рандомное число: " + rndNum);
            Console.WriteLine("программа загадает число от 0 до 100, а ты попробуй угадать!!");
            while (num != rndNum) {
                Console.WriteLine("введи свое число: ");

                bool parsed = int.TryParse(Console.ReadLine(), out num);

                if (parsed) {
                    if (num == rndNum) {
                        Console.WriteLine("красава!!!! в яблочко!!!");
                    } else if(num < rndNum) {
                        Console.WriteLine("неправильно :( попробуй ввести число побольше!!!");
                    } else {
                        Console.WriteLine("неправильно :( попробуй ввести число поменьше!!!");
                    }
                } else {
                    Console.WriteLine("вы ввели не число!!");
                }
            }
        }

        private static void PrintMulTable()
        {
            int[,] table = new int[,]
            {
                {1, 2, 3, 4, 5, 6, 7, 8, 9},
                {1, 2, 3, 4, 5, 6, 7, 8, 9}
            };

            for (int i = 0; i < table.GetLength(1); i++) {
                int a = table[0, i];
                for (int k = 0; k < table.GetLength(1); k++) {
                    int b = table[1, k];
                    Console.Write("" + a + " * " + b + " = " + a * b + "  ");
                }
                Console.Write("" + a + " * " + 10 + " = " + a * 10 + "  ");
                Console.Write("\n");
            }
        }

        private static void PrintAllDividers()
        {
            while (true) {
                Console.Write("введите целое число: ");
                int num;
                bool parsed = int.TryParse(Console.ReadLine(), out num);

                if (parsed) {
                    Console.Write("все делители для числа " + num + ": ");
                    string res = "";
                    for (int i = 1; i <= num; i++) {
                        if (num % i == 0) {
                            res += i + ", ";     
                        }
                    }
                    res = res.Remove(res.Length - 2);
                    Console.Write(res + "\n");
                    break;
                } else {
                    Console.WriteLine("не понял! введи чисо !!!!))))");
                }
            }
        }
    }
}
