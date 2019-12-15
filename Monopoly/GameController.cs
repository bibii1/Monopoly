using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Monopoly
{
    class GameController
    {
        private Board myBoard;

        public GameController(Board myBoard)
        {
            this.myBoard = myBoard;
        }

        public void StartGame()
        {
            Console.WriteLine("\n\n ALl choices will be made with arrows from keyboard \n\n");
            System.Threading.Thread.Sleep(2000);
            List<string> numbers = new List<string>();
            numbers.Add("2");
            numbers.Add("3");
            numbers.Add("4");
            Console.WriteLine("How many player will play today ?");
            int choix = 0;
            Texte(0, numbers);
            choix = DeplacementFleches(choix, numbers, "How many player will play today ?")+1;
            for (int i = 0; i < choix; i++)
            {
                Console.WriteLine("Enter player " + i + " name.");
                myBoard.Players.Add(new Player(Console.ReadLine()));
                myBoard.Attach(myBoard.Players[i]);
            }
            Console.WriteLine("Now it's time to play !");
            System.Threading.Thread.Sleep(2000);
            myBoard.State = "It's " + myBoard.Players[0].Name + "'s turn.";
            playTurn();
        }

        public bool GameIsOver()
        {
            if (myBoard.Players.Count == 1)
                return true;
            return false;
        }

        public void playerHasLost()
        {
            for (int i = 0; i < myBoard.Players.Count; i++)
            {
                if (myBoard.Players[i].Balance < 0)
                {
                    Console.WriteLine(myBoard.Players[i] + " has a negative balance and is now eliminated");
                    myBoard.Players[i].hasLost();
                    myBoard.Players.Remove(myBoard.Players[i]);
                    break;
                }
            }
        }

        public void playTurn()
        {
            if(!GameIsOver())
            {
                Console.Clear();
                myBoard.MyView.showState();
                MovePLayer(myBoard.Players[myBoard.ActivePlayer]);
                LandOn(myBoard.Players[myBoard.ActivePlayer]);
            }
            else
            {
                Console.WriteLine("Congratulations " + myBoard.Players[0].Name + " you have won the game");
            }
        }

        public void LockedTurn(Player activePlayer)
        {
            Console.WriteLine("It's " + activePlayer.Name + " " + (activePlayer.TurnsLocked + 1) + " turn in prison");
            activePlayer.TurnsLocked++;
            if (myBoard.Dices[0].Value == myBoard.Dices[1].Value)
            {
                Console.WriteLine("It's a double !");
                Console.WriteLine(activePlayer.Name + "get out of prison");
                Console.WriteLine(activePlayer.Name + " rolled on a " + (myBoard.Dices[0].Value + myBoard.Dices[1].Value));
                activePlayer.IsLocked = false;
                activePlayer.TurnsLocked = 0;
                activePlayer.Position += myBoard.Dices[0].Value + myBoard.Dices[1].Value;
                if (activePlayer.Position > 39)
                {
                    activePlayer.Position -= 39;
                    Console.WriteLine("You received 200 for passing by start case");
                    activePlayer.Balance += 200;
                }
            }
            else if (activePlayer.TurnsLocked == 3)
            {
                Console.WriteLine(activePlayer.Name + "get out of prison");
                Console.WriteLine(activePlayer.Name + " rolled on a " + (myBoard.Dices[0].Value + myBoard.Dices[1].Value));
                activePlayer.IsLocked = false;
                activePlayer.TurnsLocked = 0;
                activePlayer.Position += myBoard.Dices[0].Value + myBoard.Dices[1].Value;
                if (activePlayer.Position > 39)
                {
                    activePlayer.Position -= 39;
                    Console.WriteLine("You received 200 for passing by start case");
                    activePlayer.Balance += 200;
                }
            }
            else
            {
                Console.WriteLine("You didn't rolled a double, so you stay in prison");
                activePlayer.TurnsLocked++;
            }
        }

        public void NormalTurn(Player activePlayer)
        {
            if (myBoard.Dices[0].Value == myBoard.Dices[1].Value)
            {
                activePlayer.RolledADouble = true;
                activePlayer.Doubles++;
                Console.WriteLine("It's a double !");
                if (activePlayer.Doubles == 3)
                {
                    Console.WriteLine("3 consecutives doubles. Go to prison");
                    activePlayer.IsLocked = true;
                    activePlayer.Position = 10;
                }
            }
            Console.WriteLine(activePlayer.Name + " rolled on a " + (myBoard.Dices[0].Value + myBoard.Dices[1].Value));
            activePlayer.Position += myBoard.Dices[0].Value + myBoard.Dices[1].Value;
            if (activePlayer.Position > 39)
            {
                activePlayer.Position -= 39;
                Console.WriteLine("You received 200 for passing by start case");
                activePlayer.Balance += 200;
            }
        }

        public void MovePLayer(Player activePlayer)
        {
            myBoard.Dices[0].RollDice();
            System.Threading.Thread.Sleep(10);
            myBoard.Dices[1].RollDice();
            if (activePlayer.IsLocked)
                LockedTurn(activePlayer);
            else
                NormalTurn(activePlayer);
        }

        public void LandOn(Player activePlayer)
        {
            if (!activePlayer.IsLocked)
            {
                if (myBoard.GameBoard[activePlayer.Position] is CityCase)
                    LandOnCity(activePlayer, (CityCase)myBoard.GameBoard[activePlayer.Position]);
                else
                    LandOnAction(activePlayer, (ActionCase)myBoard.GameBoard[activePlayer.Position]);
            }
            else
            {
                NextAction();
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
                Console.WriteLine("You landed on "+activeCase.Name+", do you want to buy it ?"
                                   + "\nYour balance is " + activePlayer.Balance + ". Price is " + activeCase.PurchasePrice);
                Texte(0, answers);
                choix = DeplacementFleches(choix, answers, ("You landed on " + activeCase.Name + ", do you want to buy it ?"
                                   + "\nYour balance is " + activePlayer.Balance + ". Price is " + activeCase.PurchasePrice));
                switch(choix)
                {
                    case 1:
                        if(activePlayer.Balance >= activeCase.PurchasePrice)
                        {
                            activeCase.Owner = activePlayer;
                            activePlayer.BuyCase(activeCase);
                            Console.WriteLine("Congratulations, you're the new owner of " + activeCase.Name);
                            myBoard.Notify(myBoard.Players[myBoard.ActivePlayer]);
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
            Console.WriteLine("You landed on " + activeCase.Name);
            switch(activeCase.Action)
            {
                case "Vous remportez 100M":
                    Console.WriteLine(activeCase.Action);
                    ChangeBalance(100, activePlayer);
                    break;

                case "Payer 200M":
                    Console.WriteLine(activeCase.Action);
                    ChangeBalance(-200, activePlayer);
                    break;

                case "Payer 50M":
                    Console.WriteLine(activeCase.Action);
                    ChangeBalance(-50, activePlayer);
                    break;

                case "Rien":
                    Console.WriteLine("C'est une simple visite");
                    break;

                case "Payer 100M":
                    Console.WriteLine(activeCase.Action);
                    ChangeBalance(-100, activePlayer);
                    break;

                case "Vous remportez 70M":
                    Console.WriteLine(activeCase.Action);
                    ChangeBalance(70, activePlayer);
                    break;

                case "Allez en prison":
                    Console.WriteLine(activeCase.Action);
                    activePlayer.Position = 10;
                    activePlayer.IsLocked = true;
                    break;

                case "Vous remportez 50M":
                    Console.WriteLine(activeCase.Action);
                    ChangeBalance(50, activePlayer);
                    break;

                case "Payer 30M":
                    Console.WriteLine(activeCase.Action);
                    ChangeBalance(-30, activePlayer);
                    break;

                case "Recevez 400M":
                    Console.WriteLine(activeCase.Action);
                    ChangeBalance(400, activePlayer);
                    break;

                case "Vous remportez le total des taxes accumulees":
                    Console.WriteLine(activeCase.Action+" soit "+ ((FreeParking)myBoard.GameBoard[20]).TotalTaxes);
                    ChangeBalance(((FreeParking)myBoard.GameBoard[20]).TotalTaxes, activePlayer);
                    ((FreeParking)myBoard.GameBoard[20]).TotalTaxes = 0;
                    break;
            }
            NextAction();
        }

        public void ChangeBalance(int val, Player activePlayer)
        {
            activePlayer.Balance += val;
            if(val<0)
                ((FreeParking)myBoard.GameBoard[20]).TotalTaxes += val;
            myBoard.Notify(myBoard.Players[myBoard.ActivePlayer]);
        }

        public void NextAction()
        {
            playerHasLost();
            List<string> actions = new List<string>();
            actions.Add("Show all player stats");
            actions.Add("End turn");
            int choix = 0;
            if (myBoard.Players[myBoard.ActivePlayer].IsLocked)
            {
                myBoard.Players[myBoard.ActivePlayer].Doubles = 0;
                myBoard.ActivePlayer++;
                if (myBoard.ActivePlayer >= myBoard.Players.Count)
                    myBoard.ActivePlayer = 0;
                myBoard.State = "It's " + myBoard.Players[myBoard.ActivePlayer].Name + "'s turn.";
                playTurn();
            }
            else
            {
                Console.WriteLine("What do you want to do ?");
                Texte(0, actions);
                choix = DeplacementFleches(choix, actions, "What do you want to do ?");
            }
            switch (choix)
            {
                case 1:
                    Console.Clear();
                    foreach (Player p in myBoard.Players)
                        Console.WriteLine(p); 
                    Console.WriteLine();
                    NextAction();
                    break;

                case 2:
                    if (myBoard.Players[myBoard.ActivePlayer].RolledADouble)
                        myBoard.Players[myBoard.ActivePlayer].RolledADouble = false;
                    else
                        myBoard.ActivePlayer++;
                    if (myBoard.ActivePlayer >= myBoard.Players.Count)
                        myBoard.ActivePlayer = 0;
                    myBoard.State = "It's " + myBoard.Players[myBoard.ActivePlayer].Name + "'s turn.";
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
