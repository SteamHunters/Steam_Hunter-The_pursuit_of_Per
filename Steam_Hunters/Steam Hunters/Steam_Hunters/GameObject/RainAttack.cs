using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class RainAttack : GameObject
    {
        private Point sheetSize, currentFrame = new Point(0, 0), frameSize;
        private int timerSinceLastFrame = 0, milliSecondsPerFrame;
        private bool animation;
        public bool remove;

        public RainAttack(Vector2 pos, Texture2D tex, Point frameSize, Point sheetSize, int milliSecondsPerFrame, bool animation)
            : base(tex, pos)
        {
            this.frameSize = frameSize;
            this.sheetSize = sheetSize;
            this.milliSecondsPerFrame = milliSecondsPerFrame;
            this.animation = animation;
            origin = new Vector2(frameSize.X / 2, frameSize.Y / 2);
        }

        public override void Update(GameTime gameTime)
        {
            if (animation == true)
            {
                timerSinceLastFrame += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timerSinceLastFrame >= milliSecondsPerFrame)
                {
                    ++currentFrame.X;
                    timerSinceLastFrame = 0;
                    if (currentFrame.X >= sheetSize.X)
                    {
                        //currentFrame.X = 0;
                        animation = false;
                        remove = true;
                    }
                    if (currentFrame.X == 6)
                    {
                        hitBox = new Rectangle((int)pos.X - (frameSize.X / 2), (int)pos.Y - (frameSize.Y / 2), frameSize.X, frameSize.Y);
                    }
                }

                //if (currentFrame.X == 7)
                //{
                //    animation = false;
                //    //    rainTargetActive = false;
                //    //    rightTriggerPressed = false;
                //    //    currentFrameRain.X = 0;
                //}
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (currentFrame.X < 7)
            {
                spriteBatch.Draw(tex, pos, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, angle, origin, 1, SpriteEffects.None, 0);

                //if (currentFrame.X == 6)
                //{
                //    spriteBatch.Draw(tex, hitBox, Color.Red);

                //}

            }
        }
    }
}
