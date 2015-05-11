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
        private int timerSinceLastFrame = 0, milliSecondsPerFrame, timerHoldEnemy;
        private bool animation, trapDisappear, catchEnemy;
        //public Rectangle hitBox;

        public Trap(Vector2 pos, Texture2D tex, Point frameSize, Point sheetSize, int milliSecondsPerFrame, bool animation)
            : base(tex, pos)
        {
            this.frameSize = frameSize;
            this.sheetSize = sheetSize;
            this.milliSecondsPerFrame = milliSecondsPerFrame;
            this.animation = animation;
            origin = new Vector2(frameSize.X / 2, frameSize.Y / 2);
            trapDisappear = false;
            catchEnemy = false;
        }

        public override void Update(GameTime gameTime)
        {
            hitBox = new Rectangle((int)pos.X - (frameSize.X / 2), (int)pos.Y - (frameSize.Y / 2), frameSize.X, frameSize.Y);

            if (animation == true)
            {
                if (catchEnemy == true)
                {
                    timerSinceLastFrame += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (timerSinceLastFrame > milliSecondsPerFrame)
                    {
                        timerSinceLastFrame -= milliSecondsPerFrame;
                        ++currentFrame.X;
                        if (currentFrame.X >= 2)//verkar som att den e på den sista animationen
                        {
                            animation = false;

                            timerHoldEnemy += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                            if (timerHoldEnemy >= 200)
                            {
                                trapDisappear = true;
                            }
                            //currentFrame.X = 0;
                            //++currentFrame.Y;
                            //if (currentFrame.Y >= sheetSize.Y)
                            //    currentFrame.Y = 0;
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(tex, hitBox, Color.Red);
            //spriteBatch.Draw(tex, new Rectangle((int)pos.X, (int)pos.Y, 30, 2), Color.Red);
            spriteBatch.Draw(tex, pos, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.Red, angle, origin, 1, SpriteEffects.None, 0);
        }
    }
}
