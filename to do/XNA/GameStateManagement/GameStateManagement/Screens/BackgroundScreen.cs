using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement
{
    class BackgroundScreen : GameScreen
    {
        ContentManager content;
        Animation back;

        public BackgroundScreen()
        {
            back = new Animation(2, 2, new Vector2(0, 0));
            back.play = true;
            //TransitionOnTime = TimeSpan.FromSeconds(0.5);
            //TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            back.Load(content, "background");
        }
        
        public override void UnloadContent()
        {
            content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            back.UpdateFrame(gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            
            back.Draw(spriteBatch);
            
        }
    }
}
