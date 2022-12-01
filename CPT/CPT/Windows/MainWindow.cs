using System.Diagnostics;
using System.Text;
using CPT.Player;

namespace CPT.Windows
{
    public class MainWindow : Window
    {
        private StringBuilder _correctText = new StringBuilder();

        public string testText = "";

        private int _cursorX = 0, _cursorY = 0;

        private bool _playerStarted = false;

        private int _secondsToWrite = 5;
        private int _timeSpendInMS = 0;

        private int _maxLinesNum = 1;

        private int _totalInputChars = 0;

        private Thread _timerThread;

        private Stopwatch _stopwatch = new Stopwatch();

        public MainWindow()
        {
            _timerThread = new Thread(() =>
            {         
                while (true) {
                    if (_playerStarted && Program.currentWindow == this) {
                        try {
                            _stopwatch.Start();
                            Thread.Sleep(1);
                            _stopwatch.Stop();

                            _timeSpendInMS = (int) (_stopwatch.Elapsed.TotalMilliseconds);
                            int secondsLeft = _secondsToWrite - _timeSpendInMS / 1000;           

                            Console.ForegroundColor = ConsoleColor.White;    
                            Console.SetCursorPosition(0, _maxLinesNum + 1);
                            Console.WriteLine(TimeSpan.FromSeconds(secondsLeft));

                            if(secondsLeft <= 0) {
                                StopInput();
                            }
                        } catch(Exception e) {

                        }
                    }
                }
            });

            _timerThread.Start();
        }

        public override void Show()
        {
            _maxLinesNum = 1;
            Console.SetCursorPosition(0, 0);

            for(int i = 0; i < testText.Length; i++) {
                if (i < _correctText.Length && testText[i] == _correctText[i]) {
                    Console.ForegroundColor = ConsoleColor.Green;
                } else {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write(testText[i]);

                if((i + 1) % Console.WindowWidth == 0) {
                    _maxLinesNum++;
                }
            }

            Console.WriteLine("\n-------------------");
            _maxLinesNum++;

            Console.SetCursorPosition(_cursorX, _cursorY);    

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch(keyInfo.Key) {
                case ConsoleKey n when n != ConsoleKey.Escape &&
                n != ConsoleKey.Backspace && 
                n != ConsoleKey.NoName &&
                n != ConsoleKey.Enter:
                    char c = keyInfo.KeyChar;

                    if (_cursorX < testText.Length && c == testText[_totalInputChars]) {
                        // активация таймера по вводу первого символа =)
                        if(_cursorX == 0) {
                            _playerStarted = true;
                        }

                        _cursorX++;
                        _totalInputChars++;

                        if (_cursorX >= Console.WindowWidth) {
                            _cursorY++;
                            _cursorX = 0;
                        } 

                        _correctText.Append(c);

                        if (_totalInputChars == testText.Length) {
                            StopInput();
                        }
                    }
                    break;
            }

            Clear();
        }

        private void StopInput()
        {          
            Program.NewPlayerWindow.CurrentPlayerInfo.CPS = _correctText.Length / (_timeSpendInMS / 1000.0f);
            Program.NewPlayerWindow.CurrentPlayerInfo.CPM = Program.NewPlayerWindow.CurrentPlayerInfo.CPS * 60.0f;

            Program.PlayersStats.PlayersInfos.Add(new PlayerInfo(Program.NewPlayerWindow.CurrentPlayerInfo));
            Program.PlayersStats.Save();

            Clear();
            Console.SetCursorPosition(0, 0);

            _timerThread.Interrupt();

            _correctText = new StringBuilder();

            _timeSpendInMS = 0;

            _cursorX = _cursorY = 0;

            _totalInputChars = 0;

            _playerStarted = false;

            _stopwatch.Reset();

            Program.currentWindow = Program.RecordsTable;
        }

        public override void Clear()
        {
            for(int i = 0; i < _maxLinesNum + 2; i++) {
                Console.WriteLine("                                                                                                             ");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
