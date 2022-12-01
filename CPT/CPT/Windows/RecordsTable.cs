using CPT.Player;

namespace CPT.Windows
{
    public class RecordsTable : Window
    {
        public override void Show()
        {
            Console.WriteLine("Имя\t\tСимволы в минуту\t\tСимволы в секунду");
            foreach (PlayerInfo playerInfo in Program.PlayersStats.PlayersInfos) {
                Console.WriteLine(playerInfo.name + "\t\t" + playerInfo.CPM + "\t\t\t" + playerInfo.CPS);
            }
            Console.WriteLine("-------------\nНажмите Enter для выхода...");

            Console.ReadLine();

            Clear();

            Program.currentWindow = Program.NewPlayerWindow;
        }
    }
}
