using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Steam_Hunters
{
    class NPC : GameObject
    {
        public Rectangle hitBox;
        public float radius;
        public Vector2 direction;
        public Player buyer;
        public bool buy;

        public Player Buyer
        {
            get { return buyer; }
        }
        public float Radius
        {
            get { return radius; }

            set
            {
                radius = value;
            }
        }

        public NPC(Texture2D tex, Vector2 pos, float radius)
            : base(tex, pos)
        {
            this.hitBox = new Rectangle((int)pos.X, (int)pos.Y, 50, 50);
            this.radius = radius;

            center = new Vector2(pos.X + tex.Width / 2, pos.Y + tex.Height / 2);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
        }
        

        public override void Update(GameTime gameTime)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            center = new Vector2(pos.X + tex.Width / 2, pos.Y + tex.Height / 2);

            if (buyer != null)
            {
                FaceBuyer();

                if (!IsInRange(buyer.center))
                {
                    buyer = null;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, center, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);

            if(buy == true)
                spriteBatch.Draw(tex, center, null, Color.Green, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }
        public bool IsInRange(Vector2 pos)
        {
            return Vector2.Distance(center, pos) <= radius;
        }
        public void GetClosestBuyer(List<Player> playerList)
        {
            buyer = null;
            float smallestRange = radius;

            foreach (Player p in playerList)
            {
                if (Vector2.Distance(center, p.center) < smallestRange)
                {
                    smallestRange = Vector2.Distance(center, p.center);
                    buyer = p;
                }
            }
        }
        protected void FaceBuyer()
        {
            direction = center - buyer.center;
            direction.Normalize();
            rotation = (float)Math.Atan2(direction.X, -direction.Y);
        }
    }
}
