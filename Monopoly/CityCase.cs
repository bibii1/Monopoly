using System;


namespace Monopoly
{
    public class CityCase : Case
    {
        string color;
        int purshadePrice;
        int mortgagePrice;
        int[] rent;

        public CityCase(int position, string name, string color, int purshadePrice, int mortgagePrice, int[] rent)
            : base(position,name)
        {
            this.color = color;
            this.purshadePrice = purshadePrice;
            this.mortgagePrice = mortgagePrice;
            this.rent = rent;
        }

        public string Color { get => color; set => color = value; }
        public int PurshadePrice { get => purshadePrice; set => purshadePrice = value; }
        public int MortgagePrice { get => mortgagePrice; set => mortgagePrice = value; }
        public int[] Rent { get => rent; set => rent = value; }
    }
}
