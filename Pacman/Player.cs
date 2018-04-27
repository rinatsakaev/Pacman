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

        public void Move(int deltaX, int deltaY)
        {

        }
    }

    class Ghost : ICreature
    {
        public Point Coordinates { get; set; }

        public void Move(int deltaX, int deltaY)
        {

        }
    }

    class Wall : ICreature
    {
        public Point Coordinates { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Move(int deltaX, int deltaY)
        {
            return;
        }
    }

    internal interface ICreature
    {
        Point Coordinates { get; set; }
        void Move(int deltaX, int deltaY);
    }
}
