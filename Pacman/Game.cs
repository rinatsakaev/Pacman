using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public static class Game
    {
        public static ICreature[,] Map;
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);
        public static int Scores;
        public static bool IsOver;


        private const string testMap = 
        "WWW"+
        "W P"+
        "A  ";
 



        public static void CreateMap()
        {
            Map = MapCreator.CreateMap(testMap);
        }
    }
}

