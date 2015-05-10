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
        protected float backRadius;
        public float MovementSpeed;
        protected float AttackSpeed;
        //vilken map den ska spawnas i och pos är var i den mapen den är
        protected float MapPos;
        //Aggro går igång när player är i searchRadius och rör moben till AttackRangeRadius
        public bool Aggro, canAttack, ExplodeOn;
        protected double AttackCooldown;
        protected Vector2 direction;
        public Player target;
        double totalElapsedSeconds = 0;
        const double MovementChangeTimeSeconds = 1.0; //seconds
        

        public Vector2 Center
        {
            get { return center; }
        }


        public Player Target
        {
            get { return target; }
        }
        //Animation
        public Point frameSize;
        public Point currentFrame = new Point(0, 0);
        public Point sheetSize;

        protected int timerSinceLastFrame = 0;
        protected int milliSecondsPerFrame = 90;

        protected int size = 50;
        public Entity(Texture2D tex, Vector2 pos, Point frameSize, Point sheetSize, int Hp, int MaxHp, int Gold, int Item, float AttackRangeRadius, float SearchRadius,float backRadius, float MovementSpeed,
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
            this.backRadius = backRadius;
            this.MovementSpeed = MovementSpeed;
            this.AttackSpeed = AttackSpeed;
            this.Aggro = Aggro;
            this.AttackCooldown = AttackCooldown;

        }

        public override void Update(GameTime gameTime)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, frameSize.X, frameSize.Y);

            center = new Vector2(pos.X + frameSize.X / 2, pos.Y + frameSize.Y / 2);
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
            if (Aggro == true && canAttack == false) 
            { 
                pos -= direction * MovementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (target != null && target.ghostMode == false)
            {
                FaceTarget();

                if (!IsInAttackRange(target.center))
                {

                }
                
                if (IsInRange(target.center) == false || target.ghostMode == true)
                {
                    target = null;
                    Aggro = false;
                }
               
               
            }
      

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
            spriteBatch.Draw(tex, center, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, rotation, origin, 1, EntityFx, 1);
            //spriteBatch.Draw(tex, center, new Rectangle(50,50, 50, 50), Color.White, rotation, origin, 1, EntityFx, 1);
            spriteBatch.Draw(tex, hitBox, Color.Black);
        }
        public bool IsInRange(Vector2 pos)
        {
            if (Vector2.Distance(center, pos) <= SearchRadius)
            {
                return Aggro = true;
            }
            else
                return Aggro = false;
        }
        public bool IsInAttackRange(Vector2 pos)
        {
            if (pos != null)
            {
                if (Vector2.Distance(center, pos) <= AttackRangeRadius)
                {
                    return canAttack = true;
                }
                else
                    return canAttack = false;
            }
            else
                return canAttack = false;
        }

        public void GetClosestPlayer(List<Player> playerList)
        {
            target = null;
            float smallestRange = SearchRadius;

            foreach (Player p in playerList)
            {
                if (Vector2.Distance(center, p.center) < smallestRange && !p.ghostMode == true)
                {
                    smallestRange = Vector2.Distance(center, p.center);
                    target = p;

                }
            }
        }
        protected void FaceTarget()
        {
            direction = center - target.center;
            direction.Normalize();
            rotation = (float)Math.Atan2(direction.X, -direction.Y);
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
                    this.direction = new Vector2(-1, 0);

                    break;
                case 2:
                    // Går höger
                    this.rotation = 67.5f;
                    this.direction = new Vector2(1, 0);

                    break;
                case 3:
                    // Går upp
                    this.rotation = 135;
                    this.direction = new Vector2(0, -1);

                    break;
                case 4:
                    // Går ner
                    this.rotation = 0f;
                    this.direction = new Vector2(0, 1);

                    break;
                default:
                    this.direction = Vector2.Zero;
                    break;

            }
        }
        public void AOEDamage(List<Player> playerList)
        {

            foreach (Player p in playerList)
            {
                if (Vector2.Distance(center, p.center) < 300 && !p.ghostMode == true)
                {
                    p.statusWindow.hp -= 25;



                }
            }
        }
    }
}
//if (Aggro == true)
//             {
//                 if (target != null)
//                 {
//                     FaceTarget();

//                     if (!IsInRange(target.Center) || target.IsDead == true)
//                     {
//                         target = null;
//                     }
//                 }


//             }

