using System;
namespace Monopoly
{
    public class GameView
    {
        private Board myBoard;
        public GameView(Board myBoard)
        {
            this.myBoard = myBoard;
        }

        public void showState()
        {
            Console.WriteLine(myBoard.State);
        }
    }
}
