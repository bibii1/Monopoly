using System;
namespace Monopoly
{
    public class Dice
    {
        int value;

        public Dice()
        {
            Random r = new Random();
            this.value = r.Next(1, 6);
        }

        public int Value { get => value; set => this.value = value; }

        public void RollDice()
        {
            Random r = new Random();
            this.value = r.Next(1, 6);
        }
    }
}
