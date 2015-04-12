using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class Enemies : Entity
    {
        public Enemies(Texture2D tex, Vector2 pos, Point frameSize, Point sheetSize, int Hp, int MaxHp, int Gold, int Item, float AttackRangeRadius, float SearchRadius, float MovementSpeed,
         float AttackSpeed, float MapPos, bool Aggro, float AttackCooldown)
            : base(tex, pos, frameSize, sheetSize, Hp, MaxHp, Gold, Item, AttackRangeRadius, SearchRadius, MovementSpeed, AttackSpeed, MapPos, Aggro, AttackCooldown)
        {

        }
        double totalElapsedSeconds = 0;
        const double MovementChangeTimeSeconds = 2.0; //seconds


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Aggro == false)
            {
                totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

                if (totalElapsedSeconds >= MovementChangeTimeSeconds)
                {
                    totalElapsedSeconds -= MovementChangeTimeSeconds;
                    this.direction = GetRandomDirection();
                }

                pos += direction;

            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
        }
        Vector2 GetRandomDirection()
        {
            Random random = new Random();
            int randomDirection = random.Next(4);

            switch (randomDirection)
            {
                case 1:
                    return new Vector2(-1, 0);
                case 2:
                    return new Vector2(1, 0);
                case 3:
                    return new Vector2(0, -1);
                case 4:
                    return new Vector2(0, 1);
                default:
                    return Vector2.Zero;
            }
        }
    }
}




