using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Steam_Hunters
{
    class EngineerTower:GameObject
    {
        public Color color = Color.White;
        int spriteWidth, spriteHeight;
        private float radius, bulletTimer;
        GamePlayScreen gps;
        bool towerRemove;
        float towerPower;
        public int towerLife;

        public bool TowerRemove
        {
            get { return towerRemove; }
        }

         public EngineerTower(Texture2D tex, Vector2 pos, GamePlayScreen gps, int towerLife)
            : base(tex,pos)
        {
            this.gps = gps;
            spriteWidth = 50;//är något annnat ! 
            spriteHeight = 50;//är något annnat ! 
            this.towerLife = towerLife;
 
            center = new Vector2(pos.X + spriteWidth / 2, pos.Y + spriteHeight / 2);
            origin = new Vector2(spriteWidth / 2, spriteHeight / 2);
        }

         public override void Update(GameTime gameTime)
         {
             towerPower+= 1;

             if (towerPower == 700)
             {
                 towerRemove = true;
             }

             //if (bulletTimer >= 0.75f && target != null)
             //{
             //    TowerBullet tbullet = new TowerBullet(TextureManager.towerBulletTexture, Vector2.Subtract(center,
             //        new Vector2(TextureManager.towerBulletTexture.Width / 2)), rotation, 9);

             //    gps.tbullets.Add(tbullet);
             //    bulletTimer = 0;
             //}
         }
         public override void Draw(SpriteBatch spriteBatch)
         {
             spriteBatch.Draw(TextureManager.turretTexBot, center, null, color, 0, origin, 1.0f, SpriteEffects.None, 0);
             spriteBatch.Draw(TextureManager.turretTexTop, center, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0);
         }
    }
}
