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

        public CreatureCommand Act(int deltaX, int deltaY)
        {
            var command = new CreatureCommand();
            switch (Level.KeyPressed)
            {
                case Keys.Up:
                    if (deltaY - 1 >= 0)
                        command.DeltaY--;
                    break;
                case Keys.Down:
                    if (deltaY + 1 < Level.MapHeight)
                        command.DeltaY++;
                    break;
                case Keys.Left:
                    if (deltaX - 1 >= 0)
                        command.DeltaX--;
                    break;
                case Keys.Right:
                    if (deltaX + 1 < Level.MapWidth)
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

        public CreatureCommand Act(int deltaX, int deltaY)
        {

        }
    }

    class FunkyGhost : ICreature
    {
        public Point Coordinates { get; set; }
        public CreatureCommand Act(int deltaX, int deltaY)
        {
            return new CreatureCommand
            {
                DeltaX = deltaX,
                DeltaY = deltaY
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Player;
        }
    }

    class InvisibleGhost : ICreature
    {
        public Point Coordinates { get; set; }
        public CreatureCommand Act(int deltaX, int deltaY)
        {

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

        public CreatureCommand Act(int deltaX, int deltaY)
        {
            return new CreatureCommand();
        }
    }

    class Food : ICreature
    {
        public Point Coordinates { get; set; }
        public CreatureCommand Act(int deltaX, int deltaY)
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

        public CreatureCommand Act(int deltaX, int deltaY)
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
        CreatureCommand Act(int deltaX, int deltaY);
        bool DeadInConflict(ICreature conflictedObject);
    }
    public class CreatureCommand
    {
        public int DeltaX;
        public int DeltaY;
        public ICreature TransformTo;
    }
}