using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace GameStateManagement
{
    public class Animation
    {
        public Texture2D spriteTexture;
        public Vector2 XY;
        public bool play = false;

        private int frameCount;
        private double timeFrame;
        private int frame;
        private double totalElapsed;
        private int frameWidth;



        public Animation(int frameCountE, int framesPerSec, Vector2 xy)
        {
            XY = xy;
            frameCount = frameCountE;
            timeFrame = (float)1 / framesPerSec;
            frame = 0;
            totalElapsed = 0;
        }


        public void UpdateFrame(double elapsed)
        {
            totalElapsed += elapsed;
            if (totalElapsed > timeFrame)
            {
                frame++;
                //if (frame == frameCount) play = false; 

                frame = frame % (frameCount);
                totalElapsed -= timeFrame;
            }
        }


    
        public void Load(ContentManager content, String stringTexture)
        {
            spriteTexture = content.Load<Texture2D>(stringTexture);
            frameWidth = spriteTexture.Width / frameCount;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (play)
            {
                Rectangle rectangle = new Rectangle(frameWidth * frame,
                                                    0,
                                                    frameWidth,
                                                    spriteTexture.Height);

                spriteBatch.Draw(spriteTexture, XY, rectangle, Color.White);
            }
        }

    }
}
