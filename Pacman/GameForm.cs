using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    class GameForm : Form
    {
        private Timer timer;
        private Level currentLevel;
        private Image player;
        private Image ghost;
        private Size spaceSize;
        private bool right;
        private bool left;
        private bool up;
        private bool down;
        private int tickCount;
        private string levelName = "testLevel";
        private void ChangeLevel(Level newLevel)
        {
            currentLevel = newLevel;
        }

        public GameForm()
        {
            Game.CreateMap();
            currentLevel =new Level(levelName, Game.Map);
            var timer = new Timer
            {
                Interval = 15
            };
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            var controlX = left ? -1 : (right ? 1 : 0);
            var controlY = down ? -1 : (up ? 1 : 0);
            if (tickCount == 0)
            {
                currentLevel.MovePlayer(controlX, controlY); 
                currentLevel.MoveGhosts();
            }
           
            if (tickCount == 7)
                currentLevel.RefreshMap();
            tickCount++;
            if (tickCount == 8) tickCount = 0;
            Invalidate();
        }

        private void HandleKey(Keys e, bool isPressed)
        {
            if (e == Keys.A) left = isPressed;
            if (e == Keys.D) right = isPressed;
            if (e == Keys.W) up = isPressed;
            if (e == Keys.S) down = isPressed;
        }
    }
 

}
