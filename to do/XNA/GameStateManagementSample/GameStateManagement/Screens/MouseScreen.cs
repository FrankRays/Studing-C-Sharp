using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameStateManagement
{
    class MouseScreen : GameScreen
    {
        ContentManager content;
        Texture2D mouse;
        Vector2 xy;

        public MouseScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            IsPopup = true;
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            mouse = content.Load<Texture2D>("mouse");
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        public override void HandleInput(InputState input)
        {


            if (input == null)
                throw new ArgumentNullException("input");
            xy = input.WhereTheMouse();

            
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, true, false);
            //xy = new Vector2(1, 1);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            
            spriteBatch.Begin();

            spriteBatch.Draw(mouse, xy, new Color(255,255,255,255));
            spriteBatch.End();
        }

    }
}
