using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Steam_Hunters
{
    class Missile : GameObject
    {
        public Color color = Color.White;
        public int spriteWidth, spriteHeight, missileTimer;
        private float radius;
        public Vector2 direction;
        float speed;
        protected ParticleEngine particleEngineSteam;

        public Enemies target;
        public bool missileRemove;

        public Enemies Target
        {
            get { return target; }
        }

        public float Radius
        {
            get { return radius; }

            set
            {
                radius = value;
            }
        }

        public Missile(Texture2D tex, Vector2 pos, float radius,float rotation)
            : base(tex,pos)
        {
            this.radius = radius;
            this.rotation = rotation;

            center = new Vector2(pos.X + tex.Width / 2, pos.Y + tex.Height/2);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            speed = 400;
            particleEngineSteam = new ParticleEngine(TextureManager.steamTextures, pos, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            center = new Vector2(pos.X + tex.Width / 2, pos.Y + tex.Height/2);
            missileTimer++;
            particleEngineSteam.EmitterLocation = new Vector2(center.X, center.Y);
            particleEngineSteam.total = 1;
            particleEngineSteam.Update();

            if(missileTimer >= 200)
            {
                missileRemove = true;
            }

            pos -= direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            
            if (target != null)
            {
                FaceTarget();

         //target är död måste läggas in!
                if (!IsInRange(target.Center) )
                {
                    target = null;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0);
            particleEngineSteam.Draw(spriteBatch);
        }

        public bool IsInRange(Vector2 pos)
        {
            return Vector2.Distance(center, pos) <= radius;
        }

        public void GetClosestEnemy(List<Enemies> enemyList)
        {
            target = null;
            float smallestRange = radius;

            foreach (Enemies e in enemyList)
            {
                if (Vector2.Distance(center, e.Center) < smallestRange)
                {
                    smallestRange = Vector2.Distance(center, e.Center);
                    target = e;
                }
            }
        }
        protected void FaceTarget()
        {
            direction = center - target.Center;
            direction.Normalize();
            rotation = (float)Math.Atan2(-direction.X, direction.Y);
        }
    }
}
