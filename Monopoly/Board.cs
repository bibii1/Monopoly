using System;
using System.IO;
using System.Collections.Generic;

namespace Monopoly
{
    public interface PlayerObserver
    {
        void Update();
    }

    public interface Subject
    {
        void Attach(PlayerObserver observer);
        void Detach(PlayerObserver observer);
        void Notify(PlayerObserver observer);
    }

    class Board : Subject
    {
        Case[] gameBoard;
        List<Player> players;
        Dice[] dices;
        int activePlayer;
        string state;
        GameView myView;
        GameController myController;
        List<PlayerObserver> observers = new List<PlayerObserver>();


        public Board(string filename)
        {
            gameBoard = new Case[40];
            StreamReader sr = new StreamReader(filename);
            string stock = sr.ReadLine();
            string[] stock2 = new string[11];
            int[] rents = new int[6];
            for (int i = 0; i < 40; i++)
            {
                stock = sr.ReadLine();
                stock2 = stock.Split(';');
                if (stock2[1]=="Ville")
                {
                    for (int j = 0; j < 6; j++)
                    {
                        rents[j] = int.Parse(stock2[j + 5]);
                    }
                    gameBoard[i] = new CityCase(stock2[0], stock2[2], int.Parse(stock2[3]), int.Parse(stock2[4]), rents);
                }
                else
                {
                    if (!stock2[0].Equals("Parc Gratuit"))
                        gameBoard[i] = new ActionCase(stock2[0], stock2[2]);
                    else
                        gameBoard[i] = new FreeParking(stock2[0], stock2[2]);
                }
            }
            this.dices = new Dice[2];
            this.dices[0] = new Dice();
            this.dices[1] = new Dice();
            this.activePlayer = 0;
            this.state = "";
            this.myView = new GameView(this);
            this.myController = new GameController(this);
            this.players = new List<Player>();
        }

        public Case[] GameBoard { get => gameBoard; set => gameBoard = value; }
        public List<Player> Players { get => players; set => players = value; }
        public int ActivePlayer { get => activePlayer; set => activePlayer = value; }
        public Dice[] Dices { get => dices; set => dices = value; }
        public string State { get => state; set => state = value; }
        public GameView MyView { get => myView; set => myView = value; }
        public GameController MyController { get => myController; set => myController = value; }
        public List<PlayerObserver> Observers { get => observers; set => observers = value; }

        public void Attach(PlayerObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(PlayerObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(PlayerObserver observer)
        {
            observer.Update();
        }
    }
}
