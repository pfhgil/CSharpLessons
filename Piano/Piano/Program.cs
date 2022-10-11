namespace Piano
{
    class Program
    {
        private static int[][] allHz = new int[][]
        {
            new int[] { 65, 69, 73, 77, 82, 87, 92, 98, 103, 110, 116, 123 },
            new int[] { 130, 138, 146, 155, 164, 174, 185, 196, 207, 220, 233, 246 },
            new int[] { 261, 277, 293, 311, 329, 349, 370, 392, 415, 440, 466, 493 },
            new int[] { 523, 554, 587, 622, 659, 698, 740, 784, 830, 880, 932, 987 },
            new int[] { 1047, 1109, 1175, 1245, 1319, 1397, 1480, 1568, 1661, 1760, 1865, 1976 },
            new int[] { 2093, 2217, 2349, 2489, 2637, 2794, 2960, 3136, 3322, 3520, 3729, 3951 },
            new int[] { 4186, 4435, 4699, 4978, 5274, 5588, 5920, 6272, 6645, 7040, 7459, 7902 }
        };

        private static int currentOctave = 0;

        static void Main(string[] args)
        {
            while (true) {
                Console.Write(
                "привет! это пианино! нажимай клавиши F1-F7 для, чтобы изменить октаву!\n" +
                "нажимай клавиши 0-9 для того, чтобы играть на пианино (издавать звуки)!\n" +
                "текущая октава: " + (currentOctave + 1) + "!\n"
                );
                PlaySound(GetKey());
                Console.Clear();
            }
        }

        private static ConsoleKeyInfo GetKey() { return Console.ReadKey(true); }

        private static int[] GetOctave(ConsoleKeyInfo inf)
        {
            int octaveNum = Math.Abs((int)ConsoleKey.F1 - (int)inf.Key);
            if (octaveNum >= 0 && octaveNum <= 6) {
                currentOctave = octaveNum;
            }

            return allHz[currentOctave];
        }

        private static void PlaySound(ConsoleKeyInfo inf)
        {
            int[] octaveHz = GetOctave(inf);

            const int duration = 100;

            switch (inf.Key) {
                case ConsoleKey.D:
                    Console.Beep(octaveHz[0], duration);
                    break;
                case ConsoleKey.R:
                    Console.Beep(octaveHz[1], duration);
                    break;
                case ConsoleKey.F:
                    Console.Beep(octaveHz[2], duration);
                    break;
                case ConsoleKey.T:
                    Console.Beep(octaveHz[3], duration);
                    break;
                case ConsoleKey.G:
                    Console.Beep(octaveHz[4], duration);
                    break;
                case ConsoleKey.Y:
                    Console.Beep(octaveHz[5], duration);
                    break;
                case ConsoleKey.H:
                    Console.Beep(octaveHz[6], duration);
                    break;
                case ConsoleKey.J:
                    Console.Beep(octaveHz[7], duration);
                    break;
                case ConsoleKey.I:
                    Console.Beep(octaveHz[8], duration);
                    break;
                case ConsoleKey.K:
                    Console.Beep(octaveHz[9], duration);
                    break;
                case ConsoleKey.O:
                    Console.Beep(octaveHz[10], duration);
                    break;
                case ConsoleKey.L:
                    Console.Beep(octaveHz[11], duration);
                    break;
            }
        }
    }
}
