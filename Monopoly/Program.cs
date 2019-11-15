using System;

namespace Monopoly
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Board monopoly = new Board("monopoly2.csv");
            monopoly.MyController.StartGame();
        }

    }
}
