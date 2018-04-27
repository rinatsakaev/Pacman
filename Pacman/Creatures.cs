using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    public class Player: ICreature
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
            throw new NotImplementedException();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
    }

    class InvisibleGhost:ICreature
    {
        public Point Coordinates { get; set; }
        public CreatureCommand Act(int deltaX, int deltaY)
        {
            throw new NotImplementedException();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            throw new NotImplementedException();
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
            return;
        }
    }

    class Food : ICreature
    {
        public Point Coordinates { get; set; }
        public CreatureCommand Act(int deltaX, int deltaY)
        {
            throw new NotImplementedException();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            throw new NotImplementedException();
        }
    }

    class SuperFood : ICreature
    {
        public Point Coordinates { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public CreatureCommand Act(int deltaX, int deltaY)
        {
            throw new NotImplementedException();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            throw new NotImplementedException();
        }
    }
    public interface ICreature
    {
        Point Coordinates { get; set; }
        CreatureCommand Act(int deltaX, int deltaY);
        bool DeadInConflict(ICreature conflictedObject);
    }

}

