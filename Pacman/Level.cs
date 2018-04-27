using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class Level
    {
        public Level(string name, Point start, ICreature[,] map)
        {
            Name = name;
            Start = start;
            Map = map;
        }
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);
        public bool IsCompleted;
        public readonly string Name;
        public readonly Point Start;
        public static ICreature[,] Map;
        public Player Player;

        public void MakeStep(int deltaX, int deltaY)
        {
             Player.Move(deltaX, deltaY);
        }

        public bool CanMoveTo(int x, int y)
        {
            if (!(x <= MapWidth && x >= 0 && y <= MapHeight && y >= 0))
                return false;
            if (Map[x, y] is Wall)
                return false;
            return true;
        }
    }
}
