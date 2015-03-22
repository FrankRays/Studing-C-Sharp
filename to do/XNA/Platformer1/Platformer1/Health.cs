using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Platformer1
{
    class Health
    {
        private Texture2D texture;
        private Vector2 XY;
        private float Frame;
        private float x;
        private int y;
        
        public int health = 100;
        public readonly Color Color = Color.Yellow;
        public bool canTakeDmg = true;
        

        public Level Level
        {
            get { return level; }
        }
        Level level;

        public Health(Level level, int hp)
        {
            this.level = level;
            this.XY = new Vector2(200, 10);
            health = hp;

            LoadContent();
        }

        public void LoadContent()
        {
            texture = level.Content.Load<Texture2D>("health");
            x = texture.Width / 10;
            y = texture.Height;
        }

        public void Update(Vector2 xy)
        {
            XY = xy;
        }

        public void TakeDmg(int dmg)
        {
            if (canTakeDmg)
                health -= dmg;
            Frame = 10 - health / 10;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, XY, new Rectangle((int)(Frame*x), 10, (int)x, y), Color.Red);
        }
    }
}
