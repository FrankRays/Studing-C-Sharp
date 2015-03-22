using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lvl_editor
{
    class EnemyObj
    {

        public Enemy properties;
        public PictureBox obj;
        public int namber;
        public EnemyObj(PictureBox o, int n)
        {
            obj = o;
            properties = new Enemy(); 
            namber = n;
        }
    }

}
