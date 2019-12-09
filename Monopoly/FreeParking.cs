using System;
namespace Monopoly
{
    public class FreeParking : ActionCase
    {
        int totalTaxes;

        public FreeParking(string name,string action) : base(name,action)
        {
            totalTaxes = 0;
        }

        public int TotalTaxes { get => totalTaxes; set => totalTaxes = value; }
    }
}
