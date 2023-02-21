using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button[,] playField;

        private List<Button> freeCells = new List<Button>();

        private bool botCanMove = false;

        private Random random = new Random();

        private string playerSide = "●";
        private string botSide = "X";

        private bool blockControls = false;

        public MainWindow()
        {
            InitializeComponent();

            playField = new Button[3, 3];
            playField[0, 0] = (Button) this.FindName("F00");
            playField[0, 1] = (Button)this.FindName("F01");
            playField[0, 2] = (Button)this.FindName("F02");
            playField[1, 0] = (Button)this.FindName("F10");
            playField[1, 1] = (Button)this.FindName("F11");
            playField[1, 2] = (Button)this.FindName("F12");
            playField[2, 0] = (Button)this.FindName("F20");
            playField[2, 1] = (Button)this.FindName("F21");
            playField[2, 2] = (Button)this.FindName("F22");

            AddCells();

            ((Label)this.FindName("SideLabel")).Content = "Вы играете за: " + playerSide;
        }

        private void AddCells()
        {
            freeCells.Add(playField[0, 0]);
            freeCells.Add(playField[0, 1]);
            freeCells.Add(playField[0, 2]);
            freeCells.Add(playField[1, 0]);
            freeCells.Add(playField[1, 1]);
            freeCells.Add(playField[1, 2]);
            freeCells.Add(playField[2, 0]);
            freeCells.Add(playField[2, 1]);
            freeCells.Add(playField[2, 2]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button) sender;

            if(button.Name == "RestartButton" && blockControls)
            {
                freeCells.Clear();

                string tmp = playerSide;
                playerSide = botSide;
                botSide = tmp;

                for(int i = 0; i < playField.GetLength(0); i++)
                {
                    for(int k = 0; k < playField.GetLength(1); k++)
                    {
                        playField[i, k].Content = "";
                        playField[i, k].IsEnabled = true;
                    }
                }

                blockControls = false;

                ((Label)this.FindName("SideLabel")).Content = "Вы играете за: " + playerSide;

                AddCells();
            }

            if (blockControls || button.Name == "RestartButton") return;

            if(button.Content == "")
            {
                button.Content = playerSide;
                RemoveFreeCell(button);
                botCanMove = true;
            } 
            else
            {
                botCanMove = false;
            }

            CheckSomeoneWon();

            Bot_FindDangerousFields();
            Bot_Attack();

            if (!blockControls)
            {
                CheckSomeoneWon();
            }
        }

        private void CheckSomeoneWon()
        {
            bool playerWon = false;
            bool botWon = false;

            CheckSideForWinning(playerSide, ref playerWon);

            // проверка на то, что ботяра победил
            CheckSideForWinning(botSide, ref botWon);

            if (playerWon)
            {
                MessageBox.Show("Победа!");
                BlockControls();
            }
            else if(botWon)
            {
                MessageBox.Show("Бот победил =(!");
                BlockControls();
            }
            else if(!playerWon && !botWon && freeCells.Count == 0)
            {
                MessageBox.Show("Ничья!");
                BlockControls();
            }

            //CheckWinCombination()
        }

        private void BlockControls()
        {
            blockControls = true;
            for (int i = 0; i < playField.GetLength(0); i++)
            {
                for (int k = 0; k < playField.GetLength(1); k++)
                {
                    playField[i, k].IsEnabled = false;
                }
            }
        }

        private void CheckSideForWinning(string side, ref bool won)
        {
            CheckWinCombination((0, 0), (0, 2), (0, 1), side, ref won);
            CheckWinCombination((1, 0), (1, 2), (1, 1), side, ref won);
            CheckWinCombination((2, 0), (2, 2), (2, 1), side, ref won);

            CheckWinCombination((0, 0), (0, 1), (0, 2), side, ref won);
            CheckWinCombination((1, 0), (1, 1), (1, 2), side, ref won);
            CheckWinCombination((2, 0), (2, 1), (2, 2), side, ref won);

            CheckWinCombination((0, 1), (0, 2), (0, 0), side, ref won);
            CheckWinCombination((1, 1), (1, 2), (1, 0), side, ref won);
            CheckWinCombination((2, 1), (2, 2), (2, 0), side, ref won);



            CheckWinCombination((0, 0), (2, 0), (1, 0), side, ref won);
            CheckWinCombination((0, 1), (2, 1), (1, 1), side, ref won);
            CheckWinCombination((0, 2), (2, 2), (1, 2), side, ref won);

            CheckWinCombination((0, 0), (1, 0), (2, 0), side, ref won);
            CheckWinCombination((0, 1), (1, 1), (2, 1), side, ref won);
            CheckWinCombination((0, 2), (1, 2), (2, 2), side, ref won);

            CheckWinCombination((1, 0), (2, 0), (0, 0), side, ref won);
            CheckWinCombination((1, 1), (2, 1), (0, 1), side, ref won);
            CheckWinCombination((1, 2), (2, 2), (0, 2), side, ref won);



            CheckWinCombination((0, 0), (2, 2), (1, 1), side, ref won);
            CheckWinCombination((0, 2), (2, 0), (1, 1), side, ref won);

            CheckWinCombination((0, 0), (1, 1), (2, 2), side, ref won);
            CheckWinCombination((1, 1), (2, 2), (0, 0), side, ref won);

            CheckWinCombination((0, 2), (1, 1), (2, 0), side, ref won);
            CheckWinCombination((1, 1), (2, 0), (0, 2), side, ref won);
        }

        private void Bot_FindDangerousFields()
        {
            if (!BotCanMove()) return;

            CheckCombinationAndPut((0, 0), (0, 2), (0, 1));
            CheckCombinationAndPut((1, 0), (1, 2), (1, 1));
            CheckCombinationAndPut((2, 0), (2, 2), (2, 1));

            CheckCombinationAndPut((0, 0), (0, 1), (0, 2));
            CheckCombinationAndPut((1, 0), (1, 1), (1, 2));
            CheckCombinationAndPut((2, 0), (2, 1), (2, 2));

            CheckCombinationAndPut((0, 1), (0, 2), (0, 0));
            CheckCombinationAndPut((1, 1), (1, 2), (1, 0));
            CheckCombinationAndPut((2, 1), (2, 2), (2, 0));



            CheckCombinationAndPut((0, 0), (2, 0), (1, 0));
            CheckCombinationAndPut((0, 1), (2, 1), (1, 1));
            CheckCombinationAndPut((0, 2), (2, 2), (1, 2));

            CheckCombinationAndPut((0, 0), (1, 0), (2, 0));
            CheckCombinationAndPut((0, 1), (1, 1), (2, 1));
            CheckCombinationAndPut((0, 2), (1, 2), (2, 2));

            CheckCombinationAndPut((1, 0), (2, 0), (0, 0));
            CheckCombinationAndPut((1, 1), (2, 1), (0, 1));
            CheckCombinationAndPut((1, 2), (2, 2), (0, 2));



            CheckCombinationAndPut((0, 0), (2, 2), (1, 1));
            CheckCombinationAndPut((0, 2), (2, 0), (1, 1));

            CheckCombinationAndPut((0, 0), (1, 1), (2, 2));
            CheckCombinationAndPut((1, 1), (2, 2), (0, 0));

            CheckCombinationAndPut((0, 2), (1, 1), (2, 0));
            CheckCombinationAndPut((1, 1), (2, 0), (0, 2));

        }

        private void Bot_Attack()
        {
            if (!BotCanMove()) return;

            int rnd = random.Next(0, freeCells.Count);
            Button foundButton = freeCells[rnd];

            if (foundButton != null)
            {
                foundButton.Content = botSide;

                RemoveFreeCell(foundButton);
            }

            botCanMove = false;
        }

        private void CheckCombinationAndPut((int, int) pointOne, (int, int) pointTwo, (int, int) pointToPut)
        {
            if (!BotCanMove()) return;

            (int, int) foundCell = (-1, -1);
            if (playField[pointOne.Item1, pointOne.Item2].Content == playerSide && playField[pointTwo.Item1, pointTwo.Item2].Content == playerSide && playField[pointToPut.Item1, pointToPut.Item2].Content == "")
            {
                foundCell = (pointToPut.Item1, pointToPut.Item2);
                playField[foundCell.Item1, foundCell.Item2].Content = botSide;

                RemoveFreeCell(playField[foundCell.Item1, foundCell.Item2]);

                botCanMove = false;
            }
        }

        private void RemoveFreeCell(Button button)
        {
            if (freeCells.Contains(button))
            {
                freeCells.Remove(button);
            }
        }

        private bool BotCanMove()
        {
            return botCanMove && freeCells.Count > 0;
        }

        private void CheckWinCombination((int, int) pointOne, (int, int) pointTwo, (int, int) pointThree, string s, ref bool def)
        {
            bool res = playField[pointOne.Item1, pointOne.Item2].Content == s && playField[pointTwo.Item1, pointTwo.Item2].Content == s && playField[pointThree.Item1, pointThree.Item2].Content == s;
            def = res ? res : def;

           //essageBox.Show(def.ToString());
        }

        private void F10_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
