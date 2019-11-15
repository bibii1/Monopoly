using System;
using System.Collections.Generic;

namespace Monopoly
{
    public class Player
    {
        string name;
        int position;
        Case[][] properties;
        int balance;
        int playerNumber;
        static int val = 1;
        bool rolledADouble;
        int doubles;
        bool isLocked;

        public Player(string name)
        {
            this.name = name;
            this.position = 0;
            this.properties = new Case[10][];
            this.balance = 1500;
            this.properties[0] = new Case[2];
            this.properties[1] = new Case[4];
            this.properties[2] = new Case[2];
            this.properties[3] = new Case[3];
            this.properties[4] = new Case[3];
            this.properties[5] = new Case[3];
            this.properties[6] = new Case[3];
            this.properties[7] = new Case[3];
            this.properties[8] = new Case[3];
            this.properties[9] = new Case[2];
            playerNumber = val;
            val++;
            doubles = 0;
            rolledADouble = false;
            isLocked = false;
        }

        public string Name { get => name; set => name = value; }
        public int Position { get => position; set => position = value; }
        public Case[][] Properties { get => properties; set => properties = value; }
        public int Balance { get => balance; set => balance = value; }
        public int PlayerNumber { get => playerNumber; set => playerNumber = value; }
        public bool RolledADouble { get => rolledADouble; set => rolledADouble = value; }
        public int Doubles { get => doubles; set => doubles = value; }
        public bool IsLocked { get => isLocked; set => isLocked = value; }

        public void BuyCase(CityCase newCase)
        {
            int count = 0;
            List<string> colors = new List<string>();
            colors.Add("blanc");
            colors.Add("noir");
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
                case "blanc":
                    while (properties[0][count] != null)
                        count++;
                    properties[0][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    break;

                case "noir":
                    while (properties[1][count] != null)
                        count++;
                    properties[1][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    break;

                case "Marron":
                    while (properties[2][count] != null)
                        count++;
                    properties[2][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    break;

                case "Bleu clair":
                    while (properties[3][count] != null)
                        count++;
                    properties[3][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    break;

                case "Rose":
                    while (properties[4][count] != null)
                        count++;
                    properties[4][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    break;

                case "Orange":
                    while (properties[5][count] != null)
                        count++;
                    properties[5][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    break;

                case "Rouge":
                    while (properties[6][count] != null)
                        count++;
                    properties[6][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    break;

                case "Jaune":
                    while (properties[7][count] != null)
                        count++;
                    properties[7][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    break;

                case "Vert":
                    while (properties[8][count] != null)
                        count++;
                    properties[8][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    break;

                case "Bleu fonce":
                    while (properties[9][count] != null)
                        count++;
                    properties[9][count] = newCase;
                    this.Balance -= newCase.PurchasePrice;
                    break;
            }
        }
    }
}
