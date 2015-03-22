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
using System.IO;

namespace SD
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        ContentManager content;
        SpriteBatch spriteBatch;

        Texture2D[] snowPic;

        Random r = new Random();
        
        Texture2D wand;
        Texture2D ending;

        List<Snowball> snowbolls = new List<Snowball>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = new ContentManager(Services);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            wand = content.Load<Texture2D>(@"Content\wand");

            snowPic = new Texture2D[22];
            for (int i = 1; i < 22; i++)
            {
                snowPic[i] = content.Load<Texture2D>(@"Content\snow\" + i.ToString());
            }

            ending = content.Load<Texture2D>(@"Content\end");
        }

        protected override void UnloadContent()
        {
            content.Unload();
        }

        bool clickedLeft;
        bool clickedRight;
        int count = 0;
        MouseState CurrentMouseState;
        MouseState LastMouseState;

        protected override void Update(GameTime gameTime)
        {
            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();

            clickedLeft = LastMouseState.LeftButton.Equals(ButtonState.Pressed) 
                && CurrentMouseState.LeftButton.Equals(ButtonState.Released);

            clickedRight = CurrentMouseState.RightButton.Equals(ButtonState.Pressed);

            
            if (clickedLeft)
            {
                count = r.Next(1, 5);
                snowbolls.Add(new Snowball(new Vector2(CurrentMouseState.X, CurrentMouseState.Y), snowPic));
            }
            if (count > 0) 
            {
                count--;
                snowbolls.Add(new Snowball(new Vector2(CurrentMouseState.X, CurrentMouseState.Y), snowPic));
            }

            foreach (Snowball ball in snowbolls.ToArray())
            {
                ball.Update(clickedRight);
                if (!ball.exist) snowbolls.Remove(ball);
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();


            if (snowbolls.Count == 0)
            {
                spriteBatch.Draw(ending, new Vector2(0, 0), Color.Pink);
            }
            else
            {
                foreach (Snowball ball in snowbolls.ToArray())
                {
                    ball.Draw(spriteBatch);
                }
            }

            spriteBatch.Draw(wand, new Vector2(CurrentMouseState.X, CurrentMouseState.Y), Color.Pink);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
