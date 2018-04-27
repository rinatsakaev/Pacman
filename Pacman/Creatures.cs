using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    public class Player : ICreature
    {
        public Point Coordinates { get; set; }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is AngryGhost)
                return true;
            return false;
        }

        public CreatureCommand Act(int currentX, int currentY)
        {
            var command = new CreatureCommand();
            switch (Level.KeyPressed)
            {
                case Keys.Up:
                    if (currentY - 1 >= 0)
                        command.DeltaY--;
                    break;
                case Keys.Down:
                    if (currentY + 1 < Level.MapHeight)
                        command.DeltaY++;
                    break;
                case Keys.Left:
                    if (currentX - 1 >= 0)
                        command.DeltaX--;
                    break;
                case Keys.Right:
                    if (currentX + 1 < Level.MapWidth)
                        command.DeltaX++;
                    break;
            }
            return command;
        }
    }


    class AngryGhost : ICreature
    {
        public Point Coordinates { get; set; }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public CreatureCommand Act(int currentX, int currentY)
        {
            var newX = 0;
            var newY = 0;
            var playerCoordinates = GetPlayerCoordinates();
            if (playerCoordinates == null)
                return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
            if (playerCoordinates[0] < currentX && CanMove(currentX, currentY, -1, 0))
            {
                newX = -1;
                newY = 0;
            }
            if (playerCoordinates[0] > currentX && CanMove(currentX, currentY, 1, 0))
            {
                newX = 1;
                newY = 0;
            }
            if (playerCoordinates[1] < currentY && CanMove(currentX, currentY, 0, -1))
            {
                newX = 0;
                newY = -1;
            }
            if (playerCoordinates[1] > currentY && CanMove(currentX, currentY, 0, 1))
            {
                newX = 0;
                newY = 1;
            }
            return new CreatureCommand { DeltaX = newX, DeltaY = newY };
        }
        private bool CanMove(int x, int y, int deltaX, int deltaY)
        {
            if (x + deltaX >= 0 && y + deltaY >= 0 && x + deltaX < Level.MapWidth && y + deltaY < Level.MapHeight)
                if (Level.Map[x + deltaX, y + deltaY] is Wall)
                    return false;
            return true;
        }
        private int[] GetPlayerCoordinates()
        {
            for (int x = 0; x < Level.MapWidth; x++)
                for (int y = 0; y < Level.MapHeight; y++)
                {
                    if (Level.Map[x, y] is Player)
                        return new int[] { x, y };
                }
            return null;
        }
    }

    class FunkyGhost : ICreature
    {
        public Point Coordinates { get; set; }
        public CreatureCommand Act(int currentX, int currentY)
        {
            var newX = 0;
            var newY = 0;
            var playerCoordinates = GetPlayerCoordinates();
            if (playerCoordinates == null)
                return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
            if (playerCoordinates[0] > currentX && CanMove(currentX, currentY, -1, 0))
            {
                newX = -1;
                newY = 0;
            }
            if (playerCoordinates[0] < currentX && CanMove(currentX, currentY, 1, 0))
            {
                newX = 1;
                newY = 0;
            }
            if (playerCoordinates[1] > currentY && CanMove(currentX, currentY, 0, -1))
            {
                newX = 0;
                newY = -1;
            }
            if (playerCoordinates[1] < currentY && CanMove(currentX, currentY, 0, 1))
            {
                newX = 0;
                newY = 1;
            }
            return new CreatureCommand { DeltaX = newX, DeltaY = newY };
        }
        private bool CanMove(int x, int y, int deltaX, int deltaY)
        {
            if (x + deltaX >= 0 && y + deltaY >= 0 && x + deltaX < Level.MapWidth && y + deltaY < Level.MapHeight)
                if (Level.Map[x + deltaX, y + deltaY] is Wall)
                    return false;
            return true;
        }
        private int[] GetPlayerCoordinates()
        {
            for (int x = 0; x < Level.MapWidth; x++)
                for (int y = 0; y < Level.MapHeight; y++)
                {
                    if (Level.Map[x, y] is Player)
                        return new int[] { x, y };
                }
            return null;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Player;
        }
    }

    class InvisibleGhost : ICreature
    {
        private SinglyLinkedList<Point> pathToBase;
        public Point Coordinates { get; set; }
        public CreatureCommand Act(int currentX, int currentY)
        {
            var queue = new Queue<SinglyLinkedList<Point>>();
            var visited = new HashSet<Point>();
            pathToBase = null;
            queue.Enqueue(new SinglyLinkedList<Point>(new Point(currentX, currentY)));
            while (queue.Count != 0)
            {
                var list = queue.Dequeue();
                if (list.Value == Level.GhostBase)
                    pathToBase = list;

                foreach (var nextPoint in Rectangle(list.Value.X, list.Value.Y))
                {
                    if (Level.Map[nextPoint.X, nextPoint.Y] is Wall)
                        continue;
                    if (visited.Contains(nextPoint))
                        continue;
                    visited.Add(nextPoint);
                    queue.Enqueue(new SinglyLinkedList<Point>(nextPoint, list));
                }
            }

            var path = pathToBase.ToList().ToDirections();
            return new CreatureCommand
            {
                DeltaX = path.First().Item1,
                DeltaY = path.First().Item2
            };
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
            var bounds = new Rectangle(0, 0, Level.MapWidth, Level.MapHeight);
            return bounds.Contains(point);
        }
        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

    }

    class Wall : ICreature
    {
        public Point Coordinates { get; set; }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public CreatureCommand Act(int currentX, int currentY)
        {
            return new CreatureCommand();
        }
    }

    class Food : ICreature
    {
        public Point Coordinates { get; set; }
        public CreatureCommand Act(int currentX, int currentY)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Player;
        }
    }

    class SuperFood : ICreature
    {
        public Point Coordinates { get; set; }

        public CreatureCommand Act(int currentX, int currentY)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Player;
        }
    }
    public interface ICreature
    {
        Point Coordinates { get; set; }
        CreatureCommand Act(int currentX, int currentY);
        bool DeadInConflict(ICreature conflictedObject);
    }
    public class CreatureCommand
    {
        public int DeltaX;
        public int DeltaY;
        public ICreature TransformTo;
    }

}