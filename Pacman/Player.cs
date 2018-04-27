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
    public class Point
    {

    }
    class Player : ICreature
    {
        public Point Corrdinates { get; set; }

        public void Move(int deltaX, int deltaY)
        {

        }
    }

    class Ghost : ICreature
    {
        public Point Corrdinates { get; set; }

        public void Move(int deltaX, int deltaY)
        {

        }
    }

    class Wall : ICreature
    {
        public Point Corrdinates { get; set; }
        public void Move(int deltaX, int deltaY)
        {
            return;;
        }
    }

    internal interface ICreature
    {
        Point Corrdinates { get; set; }
        void Move(int deltaX, int deltaY);
    }
}
