using System;

namespace Battleship.Model
{
    public class Coordinate
    {
        public enum VerticalAxis { A = 1, B, C, D, E, F, G, H, I, J}

        public int HorizontalPoint { get; }
        public VerticalAxis VerticalPoint { get; }        

        public Coordinate(string coord)
        {
            try
            {
                VerticalPoint = (VerticalAxis)Enum.Parse(typeof(VerticalAxis), coord.Substring(0, 1).ToUpper()); ;
                HorizontalPoint = Convert.ToInt32(coord.Substring(1));
            }
            catch
            {
                throw new FormatException("Invalid Format");
            }
        }

        public static bool IsValid(string coord)
        {
            try
            {
                var verticalCoord = (VerticalAxis)Enum.Parse(typeof(VerticalAxis), coord.Substring(0, 1).ToUpper());
                var horizontalCoord = Convert.ToInt32(coord.Substring(1));
                if (Convert.ToInt32(verticalCoord) < 0 || Convert.ToInt32(verticalCoord) > 10 || horizontalCoord < 0 || horizontalCoord > 10)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}