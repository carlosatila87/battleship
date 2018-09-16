namespace Battleship.Model
{
    public class Point
    {
        public enum State
        {
            Empty = 0,
            Ship = 1,
            Bomb = 2,
            Missed = 3
        }

        public State point { get; set; } = State.Empty;

        public Point()
        {
            this.point = State.Empty;
        }
    }    
}