using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class Entity : GameObject
    {
        protected SpriteEffects EntityFx = 0;


        protected int Hp;
        protected int MaxHp;
        protected int Gold;
        //om de ska dropa items
        protected int Item;
        protected float AttackRangeRadius;
        protected float SearchRadius;
        protected float MovementSpeed;
        protected float AttackSpeed;
        //vilken map den ska spawnas i och pos är var i den mapen den är
        protected float MapPos;
        //Aggro går igång när player är i searchRadius och rör moben till AttackRangeRadius
        protected bool Aggro;
        protected double AttackCooldown;
        public Vector2 Center
        {
            get { return center; }
        }
        //Animation
        public Point frameSize;
        public Point currentFrame = new Point(0, 0);
        public Point sheetSize;

        protected int timerSinceLastFrame = 0;
        protected int milliSecondsPerFrame = 30;

        protected int size = 50;
        public Entity(Texture2D tex, Vector2 pos, Point frameSize, Point sheetSize, int Hp, int MaxHp, int Gold, int Item, float AttackRangeRadius, float SearchRadius, float MovementSpeed,
           float AttackSpeed, float MapPos, bool Aggro, float AttackCooldown)
            : base(tex, pos)
        {
            this.tex = tex;
            this.pos = pos;

            this.frameSize = frameSize;
            this.sheetSize = sheetSize;
            center = new Vector2(pos.X + frameSize.X / 2, pos.Y + frameSize.Y / 2);
            origin = new Vector2(frameSize.X / 2, frameSize.Y / 2);


            this.MapPos = MapPos;
            this.Hp = Hp;
            this.MaxHp = MaxHp;
            this.Gold = Gold;
            this.Item = Item;
            this.AttackRangeRadius = AttackRangeRadius;
            this.SearchRadius = SearchRadius;
            this.MovementSpeed = MovementSpeed;
            this.AttackSpeed = AttackSpeed;
            this.Aggro = Aggro;
            this.AttackCooldown = AttackCooldown;

        }

        public override void Update(GameTime gameTime)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, frameSize.X, frameSize.Y);

            center = new Vector2(pos.X + frameSize.X / 2, pos.Y + frameSize.Y / 2);

            //Animation loop
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
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, 0, origin, 1, EntityFx, 1);
        }
        public bool IsInRange(Vector2 pos)
        {
            return Vector2.Distance(center, pos) <= this.SearchRadius;
        }

    }
}
