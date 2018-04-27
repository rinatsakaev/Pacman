﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int MapWidth => Map.GetLength(0);
        public int MapHeight => Map.GetLength(1);
        public Player Player;

        public void MovePlayer(int deltaX, int deltaY)
        {

        }

        public bool InMap(Point point)
        {
            return (point.X >= 0 && point.X < MapWidth
                                 && point.Y >= 0 && point.Y < MapHeight);
        }
    }
}
