using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace WindowsGame1
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 a,b,c;
        Texture2D picture, fg;
        Color[,] source;
        Color[] color = new Color[1];

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            a = new Vector2(100, 100);
            b = new Vector2(300, 150);
            c = new Vector2(200, 300);

            picture = Content.Load<Texture2D>("pict");
            source = new Color[picture.Width, picture.Height];

            for (int x = 1; x != picture.Width; x++)
                for (int y = 1; y != picture.Height; y++)
                {
                    picture.GetData<Color>(0, new Rectangle(x, y, 1, 1), color, 0, 1);
                    source[x, y] = color[0];
                }


            Change(0,0);
            spriteBatch = new SpriteBatch(GraphicsDevice);
       }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        KeyboardState keyboard;
        KeyboardState lastkeyboard;
        Color[,] arr;
        int maxX, maxY, minX, minY;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            keyboard = Keyboard.GetState();
            if (lastkeyboard != keyboard)
            {
                if (keyboard.IsKeyDown(Keys.Right))
                    Change(5, 0);
                if (keyboard.IsKeyDown(Keys.Left))
                    Change(-5, 0);
                if (keyboard.IsKeyDown(Keys.Up))
                    Change(0, -5);
                if (keyboard.IsKeyDown(Keys.Down))
                    Change(0, 5);

                lastkeyboard = keyboard;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
 
            spriteBatch.Draw(fg, new Vector2(0, 0), Color.White);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }


        void CreateLine(Vector2 x1, Vector2 x2)
        {
            if (x1.X == x2.X)
            {
                int X = (int)x1.X;
                int step = (x1.Y > x2.Y) ? -1 : 1;
                for (int y = (int)x1.Y; y < (int)x2.Y; y += step)
                {
                    arr[X, y] = Color.Black;
                }
            }
            else
            {
                int Y;
                int step = (x1.X > x2.X) ? -1 : 1;
                for (int X = (int)x1.X; !(X == (int)x2.X); X += step)
                {
                    Y = (int)((((X - x1.X) * (x2.Y - x1.Y))) / (x2.X - x1.X) + x1.Y);
                    arr[X, Y] = Color.Black;
                }
            }
        }

        void FillBlack(Vector2 a, Vector2 b, Vector2 c)
        {
            bool begin = false;
            bool was = false;
            byte filled = 0;

            for (int x = minX; !(x == maxX); x++)
            {
                for (int y = minY; !(y == maxY); y++)
                {
                    if (begin)
                    {
                        if (arr[x, y] == Color.Black && was && filled > 0) begin = false;
                        else
                        {
                            was = true;
                            arr[x, y] = Color.Black;
                            filled++;
                        }
                    }
                    else if (!was)
                        if (arr[x, y] == Color.Black) begin = true;
                }
                begin = false;
                was = false;
                filled = 0;
            }
        }

        void FillPicture()
        {
            Rectangle pix;
            Content.Unload();
            fg = Content.Load<Texture2D>("point");

            for (int x = minX; !(x == maxX); x++)
                for (int y = minY; !(y == maxY); y++)
                {
                    if (arr[x, y] == Color.Black)
                    {
                        pix = new Rectangle(x, y, 1, 1);
                        color[0] = source[x, y];
                        fg.SetData<Color>(0, pix, color, 0, 1);
                    }
                }
        }

        void Change(int x, int y)
        {
            a += new Vector2(x, y);
            b += new Vector2(x, y);
            c += new Vector2(x, y);


            maxX = maximum((int)a.X, (int)b.X, (int)c.X);
            minX = miniimum((int)a.X, (int)b.X, (int)c.X) + 1;

            maxY = maximum((int)a.Y, (int)b.Y, (int)c.Y);
            minY = miniimum((int)a.Y, (int)b.Y, (int)c.Y);

            arr = new Color[picture.Width, picture.Height];

            CreateLine(a, c);
            CreateLine(a, b);
            CreateLine(b, c);

            FillBlack(a, b, c);
            FillPicture();
        }

        public int maximum(int a, int b, int c)
        {
            if (a > b)
                if (a > c) return a;
                else return c;
            else if (b > c) return b;
                 else return c;
        }

        public int miniimum(int a, int b, int c)
        {
            if (a < b)
                if (a < c) return a;
                else return c;
            else if (b < c) return b;
            else return c;
        }

    }
}
