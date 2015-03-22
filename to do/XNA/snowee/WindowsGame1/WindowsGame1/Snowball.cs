using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class Snowball
    {
        int dx = 0;
        int dy = 0;
        Texture2D obj;
        Vector2 xy;
        Random R = new Random();
        BoundingBox bb;
        public bool exist = true;


        public Snowball(Vector2 xy, Texture2D[] source)
        {
            this.xy = xy;

            while (dx * dy == 0)
            {
                dx = R.Next(-5, 5);
                dy = R.Next(-5, 5);
            }

            obj = source[R.Next(1, 21)];
        }

        public void Update(bool MouseClick)
        {
            if (exist)
            {
                if (MouseClick)
                {
                    MouseState state = new MouseState();
                    state = Mouse.GetState();
                    Vector2 position = new Vector2(state.X, state.Y);
                    bb = new BoundingBox(new Vector3(xy, 0), new Vector3(xy + new Vector2(70, 70), 0));
                    BoundingBox newbb = new BoundingBox(new Vector3(position, 0), new Vector3(position, 0));
                    if (bb.Intersects(newbb))
                    {
                        exist = false;
                    }


                }

                if (xy.X > 720 || xy.X < 0) { dx = -dx; }
                if (xy.Y > 500 || xy.Y < 0) { dy = -dy; }

                xy.X += dx;
                xy.Y += dy;

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (exist) spriteBatch.Draw(obj, xy, Color.White);
        }



    }
}
