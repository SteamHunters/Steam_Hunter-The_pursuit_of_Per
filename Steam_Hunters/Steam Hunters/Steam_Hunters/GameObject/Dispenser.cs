using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Steam_Hunters
{
    class Dispenser:GameObject
    {
        public Color color = Color.White;
        public int spriteWidth, spriteHeight;
        private float radius, dispenserPower;
        bool dispenserRemove;

        public float Radius
        {
            get { return radius; }

            set
            {
                radius = value;
            }
        }

        public bool DispenserRemove
        {
            get { return dispenserRemove; }
        }

        public Dispenser(Texture2D tex, Vector2 pos, float radius)
            : base(tex,pos)
        {
            spriteWidth = 50;//måste titta vad den har för värden! 
            spriteHeight = 50;//måste titta vad den har för värden! 
            this.radius = radius;
 
            center = new Vector2(pos.X + spriteWidth / 2, pos.Y + spriteHeight / 2);
            origin = new Vector2(spriteWidth / 2, spriteHeight / 2);
        }

         public override void Update(GameTime gameTime)
         {
             this.center = new Vector2(pos.X + tex.Width / 2, pos.Y + tex.Height / 2);
             dispenserPower += (float)gameTime.ElapsedGameTime.TotalSeconds;

             if (dispenserPower == 20)
                 dispenserRemove = true;

             hitBox = new Rectangle((int)pos.X, (int)pos.Y, spriteWidth, spriteHeight);
         }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, center, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0);     
        }

        public bool IsInRange(Vector2 pos)
        {
            return Vector2.Distance(center, pos) <= radius;
        }
    }
}


