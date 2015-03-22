using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameStateManagement
{
    class MouseScreen
    {
        Texture2D mouseTexture;
        Vector2 XY;

        public MouseScreen() 
        {}

        public void LoadContent(ContentManager content)
        { mouseTexture = content.Load<Texture2D>("mouse"); }

        public void Update(GameTime gameTime)
        {
            XY.X = Mouse.GetState().X;
            XY.Y = Mouse.GetState().Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        { 
            spriteBatch.Draw(mouseTexture, XY, Color.Red); 
        }
    }
}
