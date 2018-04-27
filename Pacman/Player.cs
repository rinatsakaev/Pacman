using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
<<<<<<< HEAD
    class Player : ICreature
    {
        public Point Coordinates { get; set; }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is AngryGhost)
                return true;
            return false;
        }

        public void Move(int deltaX, int deltaY)
        {

        }
=======
    public static class PointExtension
    {
        public static Point Add(this Point a, int x, int y)
        {
            return new Point(a.X + x, a.Y + y);
        }
>>>>>>> 23faebb2832eecea18e86ca0818ac5a3f8e9cfd2
    }

    class AngryGhost : ICreature
    {
<<<<<<< HEAD
        public Point Coordinates { get; set; }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
=======
        public Point Corrdinates { get; set; }
>>>>>>> 23faebb2832eecea18e86ca0818ac5a3f8e9cfd2

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

    class FunkyGhost : ICreature
    {
<<<<<<< HEAD
        public Point Coordinates { get; set; }
        public void Move(int deltaX, int deltaY)
        {
            throw new NotImplementedException();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
    }

    class Wall : ICreature
    {
        public Point Coordinates { get; set; }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public void Move(int deltaX, int deltaY)
        {
            return;
=======
        public Point Corrdinates { get; set; }

        public void Move(int deltaX, int deltaY)
        {
            Corrdinates.Add(deltaX, deltaY);
        }

        public bool DeadInConflict(ICreature conflictedObj) => false;

        public CreatureCommand Act(int x, int y)
        {
            
>>>>>>> 23faebb2832eecea18e86ca0818ac5a3f8e9cfd2
        }
    }

    public interface ICreature
    {
<<<<<<< HEAD
        Point Coordinates { get; set; }
        void Move(int deltaX, int deltaY);
        bool DeadInConflict(ICreature conflictedObject);

       
=======
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
>>>>>>> 23faebb2832eecea18e86ca0818ac5a3f8e9cfd2
    }

}    
