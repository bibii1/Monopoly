using System;
using System.Collections.Generic;

namespace Monopoly
{
    public class Player : PlayerObserver
    {
        string name;
        int position;
        CityCase[][] properties;
        int balance;
        int playerNumber;
        static int val = 1;
        bool rolledADouble;
        int doubles;
        bool isLocked;
        int turnsLocked;

        public Player(string name)
        {
            this.name = name;
            this.position = 0;
            this.properties = new CityCase[10][];
            this.balance = 1500;
            this.properties[0] = new CityCase[2];
            this.properties[1] = new CityCase[4];
            this.properties[2] = new CityCase[2];
            this.properties[3] = new CityCase[3];
            this.properties[4] = new CityCase[3];
            this.properties[5] = new CityCase[3];
            this.properties[6] = new CityCase[3];
            this.properties[7] = new CityCase[3];
            this.properties[8] = new CityCase[3];
            this.properties[9] = new CityCase[2];
            playerNumber = val;
            val++;
            doubles = 0;
            rolledADouble = false;
            isLocked = false;
            turnsLocked = 0;
        }

        public string Name { get => name; set => name = value; }
        public int Position { get => position; set => position = value; }
        public CityCase[][] Properties { get => properties; set => properties = value; }
        public int Balance { get => balance; set => balance = value; }
        public int PlayerNumber { get => playerNumber; set => playerNumber = value; }
        public bool RolledADouble { get => rolledADouble; set => rolledADouble = value; }
        public int Doubles { get => doubles; set => doubles = value; }
        public bool IsLocked { get => isLocked; set => isLocked = value; }
        public int TurnsLocked { get => turnsLocked; set => turnsLocked = value; }

        public void BuyCase(CityCase newCase)
        {
            int count = 0;
            List<string> colors = new List<string>();
            colors.Add("Blanc");
            colors.Add("Noir");
            colors.Add("Marron");
            colors.Add("Bleu clair");
            colors.Add("Rose");
            colors.Add("Orange");
            colors.Add("Rouge");
            colors.Add("Jaune");
            colors.Add("Vert");
            colors.Add("Bleu fonce");
            switch (newCase.Color)
            {
                case "Blanc":
                    while (properties[0][count] != null)
                        count++;
                    properties[0][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    if (count == properties[0].Length - 1)
                        newCase.ActiveRent *= 2;
                    break;

                case "Noir":
                    while (properties[1][count] != null)
                        count++;
                    properties[1][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    if (count == properties[1].Length - 1)
                        newCase.ActiveRent *= 2;
                    break;

                case "Marron":
                    while (properties[2][count] != null)
                        count++;
                    properties[2][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    if (count == properties[2].Length - 1)
                        newCase.ActiveRent *= 2;
                    break;

                case "Bleu clair":
                    while (properties[3][count] != null)
                        count++;
                    properties[3][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    if (count == properties[3].Length - 1)
                        newCase.ActiveRent *= 2;
                    break;

                case "Rose":
                    while (properties[4][count] != null)
                        count++;
                    properties[4][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    if (count == properties[4].Length - 1)
                        newCase.ActiveRent *= 2;
                    break;

                case "Orange":
                    while (properties[5][count] != null)
                        count++;
                    properties[5][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    if (count == properties[5].Length - 1)
                        newCase.ActiveRent *= 2;
                    break;

                case "Rouge":
                    while (properties[6][count] != null)
                        count++;
                    properties[6][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    if (count == properties[6].Length - 1)
                        newCase.ActiveRent *= 2;
                    break;

                case "Jaune":
                    while (properties[7][count] != null)
                        count++;
                    properties[7][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    if (count == properties[7].Length - 1)
                        newCase.ActiveRent *= 2;
                    break;

                case "Vert":
                    while (properties[8][count] != null)
                        count++;
                    properties[8][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    if (count == properties[8].Length - 1)
                        newCase.ActiveRent *= 2;
                    break;

                case "Bleu fonce":
                    while (properties[9][count] != null)
                        count++;
                    properties[9][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    if (count == properties[9].Length - 1)
                        newCase.ActiveRent *= 2;
                    break;
            }
        }

        public override string ToString()
        {
            return name + " balance is " + balance + " and is on position " + position;
        }

        public void hasLost()
        {
            foreach(CityCase[] c1 in properties)
                foreach(CityCase c in c1)
                {
                    if(c!=null)
                    {
                        c.ActiveRent /= 2;
                        c.Owner = null;
                    }
                }
        }

        public void Update()
        {
            Console.WriteLine("Your new balance is " + balance);
        }
    }
}
