using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Model
{
    public static class StateTrackerMessages
    {
        public const string YouHit = "You hit a ship";
        public const string YouMissed = "You missed";
        public const string RepeatedShot = "Repeated shot";
    }

    public static class StateTrackerOrientation
    {
        public const string Vertical = "VERTICAL";
        public const string Horizontal = "HORIZONTAL";
    }

    public class StateTrackerBattleship
    {
        public int Size { get; set; }
        public string Coordinate { get; set; }
        public string Orientation { get; set; }
    }

    public class StateTrackerCoordinate
    {
        public string Coordinate { get; set; }
    }

    public class StateTrackerAttackReturn
    {
        public string LastAttack { get; set; }
        public string State { get; set; }
        public string GameStatus { get; set; }
    }    
}
