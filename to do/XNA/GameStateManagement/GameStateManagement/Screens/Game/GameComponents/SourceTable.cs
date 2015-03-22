using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameStateManagement
{
    class Source
    {
        public int amount;
        public Vector2 pos;
        public void Draw(SpriteBatch sb, SpriteFont font)
        {
            sb.DrawString(font, amount.ToString(), pos, Color.Red);
        }

    }

    class SourceTable
    {
        public Source population, food, кирпич;
        public Vector2 pos;
        Texture2D table;
        bool Visible = true;

        public SourceTable(ContentManager content)
        {
            pos = new Vector2(50, 0);

            population = new Source();
            food = new Source();
            кирпич = new Source();

            population.amount = 10;
            population.pos = pos + new Vector2(50, 40);

            food.amount = population.amount;
            food.pos = population.pos + new Vector2(100, 0);

            кирпич.pos = food.pos + new Vector2(100, 0);
            кирпич.amount = 20;

            table = content.Load<Texture2D>("source");
        }

        public virtual void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (Visible)
            {
                spriteBatch.Draw(table, pos, Color.White);

                population.Draw(spriteBatch, font);
                food.Draw(spriteBatch, font);
                кирпич.Draw(spriteBatch, font);
            }
            else
            {
            }
        }


    }
}
