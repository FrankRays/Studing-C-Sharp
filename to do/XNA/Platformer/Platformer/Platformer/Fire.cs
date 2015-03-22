using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Platformer
{
    class Fire
    {
        private Texture2D texture;
        private Vector2 origin;
        
        public const int DmgValue = 10;
        public readonly Color Color = Color.Yellow;

        private Vector2 Position;
        private float bounce;

        private int delta;
        private int MaxDistanse = 5000;

        public Level Level
        {
            get { return level; }
        }
        Level level;

        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
        }

        public bool dead;

        public Fire(Level level, Vector2 position, int delta)
        {
            dead = false;
            this.level = level;
            this.Position = position;
            Position.Y -= 30;
            this.delta = delta;
            texture = Level.Content.Load<Texture2D>("Sprites/Fire");
            origin = new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);
        
        }

        public void Update(GameTime gameTime, int Pos)
        {
            // Bounce control constants
            const float BounceHeight = 0.18f;
            const float BounceRate = 3.0f;
            const float BounceSync = -0.75f;

            // Bounce along a sine curve over time.
            // Include the X coordinate so that neighboring gems bounce in a nice wave pattern.            
            double t = gameTime.TotalGameTime.TotalSeconds * BounceRate + Position.X * BounceSync;
            bounce = (float)Math.Sin(t) * BounceHeight * texture.Height;
            Position.X += delta;
            Position.Y += bounce;
            if (delta*(Position.X - Pos) > MaxDistanse) dead = true; 
        }

        SpriteEffects flip;
        public void Draw(SpriteBatch spriteBatch)
        {
            if (delta > 0)
                flip = SpriteEffects.FlipHorizontally;
            else if (delta < 0)
                flip = SpriteEffects.None;
        
            spriteBatch.Draw(texture, Position, null, Color, 0.0f, origin, 1.0f, flip, 0.0f);
        }
    }
}
