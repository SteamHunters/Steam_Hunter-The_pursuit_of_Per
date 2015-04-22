using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class Trap : GameObject
    {
        private Point sheetSize, currentFrame = new Point(0, 0), frameSize;
        private int timerSinceLastFrame = 0, milliSecondsPerFrame;
        private bool animation;

        public Trap(Vector2 pos, Texture2D tex, Point frameSize, Point sheetSize, int milliSecondsPerFrame, bool animation)
            : base(tex, pos)
        {
            this.frameSize = frameSize;
            this.sheetSize = sheetSize;
            this.milliSecondsPerFrame = milliSecondsPerFrame;
            this.animation = animation;
        }

        public override void Update(GameTime gameTime)
        {
            if (animation == true)
            {
                timerSinceLastFrame += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timerSinceLastFrame > milliSecondsPerFrame)
                {
                    timerSinceLastFrame -= milliSecondsPerFrame;
                    ++currentFrame.X;
                    if (currentFrame.X >= sheetSize.X)
                    {
                        currentFrame.X = 0;
                        ++currentFrame.Y;
                        if (currentFrame.Y >= sheetSize.Y)
                            currentFrame.Y = 0;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, angle, origin, 1, SpriteEffects.None, 0);
        }
    }
}
