using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lvl_editor
{
    class Enemy
    {
        public int type;

        public int x;
        public int y;

        public int speedx;
        public int speedy;

        public int hp;
        public int bonus;

        public int fire_delay;
        public int delta;

        public Enemy()
        {
            type = x = y = hp = bonus = fire_delay = delta = 0;
        }
    }
}
