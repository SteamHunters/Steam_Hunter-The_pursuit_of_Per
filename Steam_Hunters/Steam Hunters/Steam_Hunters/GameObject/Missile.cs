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

        //public Enemy target;


        //public Enemy Target
        //{
        //    get { return target; }
        //}

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
            spriteWidth = 50;//måste titta vad den har för värden! 
            spriteHeight = 50;//måste titta vad den har för värden! 
            this.radius = radius;
            this.rotation = rotation;
 
            center = new Vector2(pos.X + spriteWidth / 2, pos.Y + spriteHeight / 2);
            origin = new Vector2(spriteWidth / 2, spriteHeight / 2);
            speed = 200;
        }

        public override void Update(GameTime gameTime)
        {
            pos += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            //if (target != null)
            //{
            //    FaceTarget();

            //    if (!IsInRange(target.Center) || target.IsDead == true)
            //    {
            //        target = null;
            //        bulletTimer = 0;
            //    }
            //}
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, center, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }

        //public bool IsInRange(Vector2 pos)
        //{
        //    return Vector2.Distance(center, pos) <= radius;
        //}

        //public void GetClosestEnemy(List<Enemy> enemies)
        //{
        //    target = null;
        //    float smallestRange = radius;

        //    foreach (Enemy e in enemies)
        //    {
        //        if (Vector2.Distance(center, e.Center) < smallestRange)
        //        {
        //            smallestRange = Vector2.Distance(center, e.Center);
        //            target = e;
        //        }
        //    }
        //}
        //protected void FaceTarget()
        //{
        //    direction = center - target.Center;
        //    direction.Normalize();
        //    rotation = (float)Math.Atan2(-direction.X, direction.Y);
        //}


    }
}
