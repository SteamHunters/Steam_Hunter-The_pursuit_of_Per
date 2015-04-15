using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class Projectile : GameObject
    {
        public Vector2 direction;
        private float speed;
        int projectileLife;
        bool bulletRemove;
        double timerRemove;
        protected int timerSinceLastFrame = 0, milliSecondsPerFrame;
        private bool animation;
        protected Point sheetSize, currentFrame = new Point(0, 0), frameSize;

        public bool BulletRemove
        {
            get { return bulletRemove; }
        }

        public Projectile(Vector2 pos, Texture2D tex, Vector2 movement, float angle, float speed, int projectileLife, Point frameSize, Point sheetSize, int milliSecondsPerFrame, bool animation)
            : base(tex, pos)
        {
            this.pos = pos;
            this.angle = angle;
            this.speed = speed;
            this.projectileLife = projectileLife;
            this.frameSize = frameSize;
            this.sheetSize = sheetSize;
            this.milliSecondsPerFrame = milliSecondsPerFrame;
            this.animation = animation;

            if(animation == false)
            origin = new Vector2(tex.Width / 2, tex.Height / 2);

            if(animation == true)
                origin = new Vector2(frameSize.X / 2, frameSize.Y / 2);

            this.direction = movement;
            //projectileLife = 80;

            //if (movement != Vector2.Zero)
            this.direction.Normalize();


        }

        public override void Update(GameTime gameTime)
        {
            if(animation == false)
                hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            if (animation == true)
                hitBox = new Rectangle((int)pos.X - frameSize.X / 2, (int)pos.Y - frameSize.Y / 2, frameSize.X, frameSize.Y);

            pos += direction * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            timerRemove += 1;

            if (timerRemove >= projectileLife)
            {
                bulletRemove = true;
            }

            #region Projectuíle Animation
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
            #endregion
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(tex, pos, hitBox, Color.Blue);
            if(animation == false)
                spriteBatch.Draw(tex, new Rectangle((int)pos.X , (int)pos.Y, tex.Width, tex.Height), null, Color.White, angle, origin, SpriteEffects.None, 0);

            if (animation == true)
            {
                spriteBatch.Draw(tex, pos, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, angle, new Vector2(frameSize.X / 2, frameSize.Y / 2), 1, SpriteEffects.None, 0);
                //spriteBatch.Draw(tex, hitBox, Color.White);
            }
        }
    }
}
