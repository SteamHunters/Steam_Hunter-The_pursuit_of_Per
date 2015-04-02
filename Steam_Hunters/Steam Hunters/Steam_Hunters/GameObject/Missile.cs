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

        public Missile(Texture2D tex, Vector2 pos, float radius)
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
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, center, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
