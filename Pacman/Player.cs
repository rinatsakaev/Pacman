using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
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
    }

    class AngryGhost : ICreature
    {
        public Point Coordinates { get; set; }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public void Move(int deltaX, int deltaY)
        {

        }
    }

    class FunkyGhost : ICreature
    {
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
        }
    }

    internal interface ICreature
    {
        Point Coordinates { get; set; }
        void Move(int deltaX, int deltaY);
        bool DeadInConflict(ICreature conflictedObject);

       
    }

}    
