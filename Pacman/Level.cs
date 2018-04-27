using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public bool IsCompleted;
        public readonly string Name;
        public readonly Point Start;
        public static ICreature[,] Map;
        public static Point GhostBase;
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);
        public Player Player;
        public static Keys KeyPressed;

        public void MoveAll()
        {
            MovePlayer(0, 0);
            MoveGhosts();
            Map = RefreshMap();
        }

        public void MovePlayer(int deltaX, int deltaY)
        {
            Player.Act(deltaX, deltaY);
            if (Player.DeadInConflict(Map[Player.Coordinates.X, Player.Coordinates.Y]))
                IsCompleted = true;
        }

        private ICreature[,] RefreshMap()
        {
   
            var aliveCandidates = Map.ToList();
            foreach (var candidate in aliveCandidates)
                foreach (var rival in aliveCandidates)
                    if (rival != candidate &&
                        rival.Coordinates.X == candidate.Coordinates.X &&
                        rival.Coordinates.Y == candidate.Coordinates.Y &&
                candidate.DeadInConflict(rival))
                        aliveCandidates.Remove(candidate);

            return GetMapFromList(aliveCandidates);
        }

        private ICreature[,] GetMapFromList(List<ICreature> aliveCandidates)
        {
            var map = new ICreature[MapWidth,MapHeight];
            foreach (var creature in aliveCandidates)
            {
                map[creature.Coordinates.X, creature.Coordinates.Y] = creature;
            }

            return map;
        }

        public void MoveGhosts()
        {
            for (var x = 0; x < MapWidth; x++)
                for (var y = 0; y < MapHeight; y++)
                {
                    var currentCell = Map[x, y];
                    if (currentCell is AngryGhost)
                        MoveAngryGhost(currentCell);
                    if (currentCell is FunkyGhost)
                        MoveFunkyGhost(currentCell);
                    if (currentCell is InvisibleGhost)
                        MoveInvisibleGhost(currentCell);
                }
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

    public static class ArrayExtensions
    {
        public static List<ICreature> ToList(this ICreature[,] map)
        {
            var res = new List<ICreature>();
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    res.Add(map[x, y]);
            return res;
        }
    }
}
