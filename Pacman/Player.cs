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
    }

    class Ghost : ICreature
    {
        public Point Corrdinates { get; set; }

        public void Move(int deltaX, int deltaY)
        {
            Corrdinates.Add(deltaX, deltaY);
        }
    }

    internal interface ICreature
    {
        Point Corrdinates { get; set; }
        void Move(int deltaX, int deltaY);
    }
}
