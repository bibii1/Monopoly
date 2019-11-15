using System;


namespace Monopoly
{
    public class CityCase : Case
    {
        string color;
        int purchasePrice;
        int mortgagePrice;
        int[] rent;
        int activeRent;
        Player owner;

        public CityCase(string name, string color, int purshadePrice, int mortgagePrice, int[] rent)
            : base(name)
        {
            this.color = color;
            this.purchasePrice = purshadePrice;
            this.mortgagePrice = mortgagePrice;
            this.rent = rent;
            this.activeRent = rent[0];
        }

        public string Color { get => color; set => color = value; }
        public int PurchasePrice { get => purchasePrice; set => purchasePrice = value; }
        public int MortgagePrice { get => mortgagePrice; set => mortgagePrice = value; }
        public int[] Rent { get => rent; set => rent = value; }
        public Player Owner { get => owner; set => owner = value; }
        public int ActiveRent { get => activeRent; set => activeRent = value; }
    }
}
