using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    public static class PointExtension
    {
        public static Point Add(this Point a, int x, int y)
        {
            return new Point(a.X + x, a.Y + y);
        }
    }
    class Player : ICreature
    {
        public Point Corrdinates { get; set; }

        public void Move(int deltaX, int deltaY)
        {
            Corrdinates.Add(deltaX, deltaY);
        }

        public bool DeadInConflict(ICreature conflictedObj)
        {
            return conflictedObj is Ghost;
        }

        public CreatureCommand Act(int x, int y)
        {
            var command = new CreatureCommand();
            switch (Level.KeyPressed)
            {
                case Keys.Up:
                    if (y - 1 >= 0)
                        command.DeltaY--;
                    break;
                case Keys.Down:
                    if (y + 1 < Level.MapHeight)
                        command.DeltaY++;
                    break;
                case Keys.Left:
                    if (x - 1 >= 0)
                        command.DeltaX--;
                    break;
                case Keys.Right:
                    if (x + 1 < Level.MapWidth)
                        command.DeltaX++;
                    break;
            }
            return command;
        }
    }

    class Ghost : ICreature
    {
        public Point Corrdinates { get; set; }

        public void Move(int deltaX, int deltaY)
        {
            Corrdinates.Add(deltaX, deltaY);
        }

        public bool DeadInConflict(ICreature conflictedObj) => false;

        public CreatureCommand Act(int x, int y)
        {
            
        }
    }

    public interface ICreature
    {
        Point Corrdinates { get; set; }
        void Move(int deltaX, int deltaY);
        bool DeadInConflict(ICreature conflictedObj);
        CreatureCommand Act(int x, int y);
    }

    public class CreatureCommand
    {
        public int DeltaX;
        public int DeltaY;
        public ICreature TransformTo;
    }
}
