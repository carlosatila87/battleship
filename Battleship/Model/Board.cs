namespace Battleship.Model
{
    public class Board
    {
        private static Board _instance;

        protected Board()
        {
            Points = (new Point[10, 10]);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Points[i, j] = new Point();
                }
            }
        }

        public static Board GetBoard()
        {
            if (_instance == null)
            {
                _instance = new Board();
            }

            return _instance;
        }

        public Point[,] Points { get; set; }

        public static class Messages
        {
            public const string FinishedGame = "You Won!";
            public const string Continue = "Keep trying";
        }

        public static Board NewBoard()
        {            
            _instance = new Board();
            return _instance;
        }        

        public static string GetGameStatus()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (_instance.Points[i, j].point == Point.State.Ship)
                        return Messages.Continue;
                }
            }
            return Messages.FinishedGame;
        }        
    }
}