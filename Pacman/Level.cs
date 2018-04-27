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
        private SinglyLinkedList<Point> pathToBase;

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

        private void MoveInvisibleGhost(ICreature ghost)
        {
            var queue = new Queue<SinglyLinkedList<Point>>();
            var visited = new HashSet<Point>();
            pathToBase = null;
            queue.Enqueue(new SinglyLinkedList<Point>(ghost.Coordinates));
            while (queue.Count != 0)
            {
                var list = queue.Dequeue();
                if (list.Value == GhostBase)
                    pathToBase = list;

                foreach (var nextPoint in Rectangle(list.Value.X, list.Value.Y))
                {
                    if (Map[nextPoint.X, nextPoint.Y] is Wall)
                        continue;
                    if (visited.Contains(nextPoint))
                        continue;
                    visited.Add(nextPoint);
                    queue.Enqueue(new SinglyLinkedList<Point>(nextPoint, list));
                }
            }

            var path = pathToBase.ToList().ToDirections();
            ghost.Act(path.First().Item1, path.First().Item2);

        }
       

        public static IEnumerable<Point> Rectangle(int xstart, int ystart)
        {
            for (var dx = -1; dx <= 1; dx++)
            for (var dy = -1; dy <= 1; dy++)
            {
                if (!InBounds(new Point(xstart + dx, ystart + dy)))
                    continue;
                if (!(dx != 0 && dy != 0 || dx == 0 && dy == 0))
                    yield return new Point { X = xstart + dx, Y = ystart + dy };
            }
        }

        public static bool InBounds(Point point)
        {
            var bounds = new Rectangle(0, 0, Map.GetLength(0), Map.GetLength(1));
            return bounds.Contains(point);
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
        public static Tuple<int, int> GetMoveDirection(Point from, Point to)
        {
            if (from.X > to.X) return new Tuple<int, int>(-1,0);
            if (from.X < to.X) return new Tuple<int, int>(1, 0);
            if (from.Y > to.Y) return new Tuple<int, int> (0, 1);
            if (from.Y < to.Y) return new Tuple<int, int>(0, -1);
            throw new ArgumentException();
        }

        public static Tuple<int, int>[] ToDirections(this List<Point> source)
        {
            var moves = new List<Tuple<int, int>>();
            if (source == null)
                return new Tuple<int, int>[0];
            for (var i = source.Count - 1; i > 0; i--)
            {
                moves.Add(GetMoveDirection(source[i], source[i - 1]));
            }
            return moves.ToArray();
        }
    }
}
