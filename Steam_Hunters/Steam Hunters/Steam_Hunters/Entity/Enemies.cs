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
        const double MovementChangeTimeSeconds = 1.0; //seconds
        
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Aggro == false)
            {
                totalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

                if (totalElapsedSeconds >= MovementChangeTimeSeconds)
                {
                    totalElapsedSeconds -= MovementChangeTimeSeconds;
                     GetRandomDirection2();
                }

                pos += direction;

            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(tex, center, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, rotation, origin, 1, EntityFx, 1);
            //spriteBatch.Draw(tex, hitBox, Color.Black);
        }
     
        private void GetRandomDirection2()
        {
            Random random = new Random();
            int randomDirection = random.Next(5);

            switch (randomDirection)
            {   
                    
                case 1:
                    // Går vänster
                    this.rotation = -67.5f;
                    this.direction = new Vector2 (-1, 0);
                    
                    break;
                case 2:
                    // Går höger
                    this.rotation = 67.5f;
                    this.direction = new Vector2 (1, 0);
                   
                    break;
                case 3:
                    // Går upp
                    this.rotation = 135;
                    this.direction = new Vector2 (0, -1);
                  
                    break;
                case 4:
                    // Går ner
                    this.rotation = 0f;
                    this.direction = new Vector2 (0, 1);
                    
                    break;
                default:
                    this.direction = Vector2.Zero;
                    break;

            }
        }
       
    }
}



