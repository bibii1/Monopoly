using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Monopoly
{
    public class GameController
    {
        private Board myBoard;

        public GameController(Board myBoard)
        {
            this.myBoard = myBoard;
        }

        public void StartGame()
        {
            List<string> numbers = new List<string>();
            numbers.Add("1");
            numbers.Add("2");
            numbers.Add("3");
            numbers.Add("4");
            Console.WriteLine("How many player will play today ?");
            int choix = 0;
            DeplacementFleches(choix, numbers, "How many player will play today ?");
            for (int i = 0; i < choix; i++)
            {
                Console.WriteLine("Enter player " + i + " name.");
                myBoard.Players.Add(new Player(Console.ReadLine()));
            }
            Console.WriteLine("Now it's time to play !");
            System.Threading.Thread.Sleep(2000);
            playTurn();
        }

        public bool GameIsOver()
        {
            if (myBoard.Players.Count == 1)
                return true;
            return false;
        }

        public void playTurn()
        {
            if(!GameIsOver())
            {
                MovePLayer(myBoard.Players[myBoard.ActivePlayer]);
                LandOn(myBoard.Players[myBoard.ActivePlayer]);
            }
        }

        public void MovePLayer(Player activePlayer)
        {
            myBoard.Dices[0].RollDice();
            myBoard.Dices[1].RollDice();
            activePlayer.Position += myBoard.Dices[0].Value + myBoard.Dices[0].Value;
            if (activePlayer.Position > 39)
                activePlayer.Position -= 39;
        }

        public void LandOn(Player activePlayer)
        {
            if(myBoard.GameBoard[activePlayer.Position] is CityCase)
            {
                LandOnCity(activePlayer,(CityCase)myBoard.GameBoard[activePlayer.Position]);
            }
            else
            {
                LandOnAction(activePlayer, (ActionCase)myBoard.GameBoard[activePlayer.Position]);
            }
        }

        public void LandOnCity(Player activePlayer,CityCase activeCase)
        {
            List<string> answers = new List<string>();
            answers.Add("Yes");
            answers.Add("No");
            int choix = 0;
            if(activeCase.Owner == null)
            {
                Console.WriteLine("This property haven't been bought yet. Do you want to buy it ?");
                DeplacementFleches(choix, answers, "This property haven't been bought yet. Do you want to buy it ?");
                switch(choix)
                {
                    case 1:
                        if(activePlayer.Balance>=activeCase.PurchasePrice)
                        {
                            activeCase.Owner = activePlayer;
                            activePlayer.BuyCase(activeCase);
                            Console.WriteLine("Congratulation, you're the new owner of " + activeCase.Name);
                            System.Threading.Thread.Sleep(2000);
                            Console.Clear();
                            NextAction();
                        }
                        else
                        {
                            Console.WriteLine("Balance is too low to purchase this case");
                            System.Threading.Thread.Sleep(2000);
                            Console.Clear();
                            NextAction();
                        }
                        break;

                    case 2:
                        Console.Clear();
                        NextAction();
                        break;
                }
            }
            else
            {
                if (activeCase.Owner.Name.Equals(activePlayer.Name))
                {
                    Console.WriteLine("You landed on your property.");
                    System.Threading.Thread.Sleep(2000);
                    NextAction();
                }
                else
                {
                    Console.WriteLine("You landed on "+ activeCase.Owner.Name +"property.\n You owe him "+activeCase.ActiveRent);
                    System.Threading.Thread.Sleep(2000);
                    activePlayer.Balance -= activeCase.ActiveRent;
                    activeCase.Owner.Balance += activeCase.ActiveRent;
                    Console.Clear();
                    NextAction();
                }
            }
        }

        public void LandOnAction(Player activePlayer,ActionCase activeCase)
        {

        }

        public void NextAction()
        {
            List<string> actions = new List<string>();
            actions.Add("Buy houses where it's possible");
            actions.Add("Show all player state");
            actions.Add("End turn");
            int choix = 0;
            Console.WriteLine("What do you want to do ?");
            DeplacementFleches(choix, actions, "What do you want to do ?");
            switch(choix)
            {
                case 1:
                    break;

                case 2:
                    break;

                case 3:
                    myBoard.ActivePlayer++;
                    playTurn();
                    break;
            }
        }

        #region
        static void Texte(int indiceTexte, List<string> maliste)
        {
            for (int i = 1; i < maliste.Count + 1; i++)
            {
                if (i == indiceTexte)
                {
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(maliste[i - 1]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                    Console.WriteLine(maliste[i - 1]);
            }
        }

        /// <summary>
        /// Saute un nombre de ligne pris en entree
        /// </summary>
        /// <param name="nombredeligne">Nombredeligne.</param>
        static void SautDeLigne(int nombredeligne)
        {
            for (int i = 0; i < nombredeligne; i++)
                Console.WriteLine();
        }

        /// <summary>
        /// Permet de se deplacer dans le menu
        /// </summary>
        /// <returns>The fleches.</returns>
        /// <param name="choix">Choix.</param>
        /// <param name="maListe">Ma liste.</param>
        /// <param name="phrase">Phrase.</param>
        static int DeplacementFleches(int choix, List<string> maListe, string phrase)
        {
            ConsoleKeyInfo cki = new ConsoleKeyInfo(); ;
            while (cki.Key != ConsoleKey.Enter)
            {
                if (cki.Key == ConsoleKey.UpArrow && choix == 1)
                {
                    choix = maListe.Count + 1;
                    Console.Clear();
                    Console.WriteLine(phrase);
                    SautDeLigne(1);
                    Texte(choix, maListe);
                }
                if (cki.Key == ConsoleKey.DownArrow && choix == maListe.Count)
                {
                    choix = 0;
                    Console.Clear();
                    Console.WriteLine(phrase);
                    SautDeLigne(1);
                    Texte(choix, maListe);
                }
                if (cki.Key == ConsoleKey.UpArrow && choix > 1)
                {
                    choix--;
                    Console.Clear();
                    Console.WriteLine(phrase);
                    SautDeLigne(1);
                    Texte(choix, maListe);

                }
                if (cki.Key == ConsoleKey.DownArrow && choix < maListe.Count)
                {

                    Console.Clear();
                    choix++;
                    Console.WriteLine(phrase);
                    SautDeLigne(1);
                    Texte(choix, maListe);
                }
                cki = Console.ReadKey();
            }
            Console.Clear();
            return choix;
        }
        #endregion
    }
}
