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
    public class PointExtension
    {
        public static Point Add(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

    }
    class Player : ICreature
    {
        public Point Corrdinates;

        public void Move(int deltaX, int deltaY)
        {

        }
    }

    class Ghost : ICreature
    {
        public Point Corrdinates;

        public void Move(int deltaX, int deltaY)
        {

        }
    }

    internal interface ICreature
    {
        Point Corrdinates;
        void Move(int deltaX, int deltaY);
    }
}
