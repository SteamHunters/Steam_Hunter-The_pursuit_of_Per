using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class EnemyBomb : Entity
    {
        public EnemyBomb(Texture2D tex, Vector2 pos, Point frameSize, Point sheetSize, int Hp, int MaxHp, int Gold, int Item, float AttackRangeRadius, float SearchRadius, float backRadius, float MovementSpeed,
         float AttackSpeed, float MapPos, bool Aggro, float AttackCooldown)
            : base(tex, pos, frameSize, sheetSize, Hp, MaxHp, Gold, Item, AttackRangeRadius, SearchRadius, backRadius, MovementSpeed, AttackSpeed, MapPos, Aggro, AttackCooldown)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

      

        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(tex, center, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, rotation, origin, 1, EntityFx, 1);
        }

         

    }
}
