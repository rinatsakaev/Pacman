﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    class Level
    {
        public Level(string name, Point start, char[,] map)
        {
            Name = name;
            Start = start;
            Map = map;
        }

        public bool IsCompleted;
        public readonly string Name;
        public readonly Point Start;
        public char[,] Map;
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);
        public Player Player;
        public static Keys KeyPressed;

        public void MovePlayer(int deltaX, int deltaY)
        {
            Player.Act(deltaX, deltaY);
        }

        public void MoveGhosts(int deltaX, int deltaY)
        {

        }
    }
}
