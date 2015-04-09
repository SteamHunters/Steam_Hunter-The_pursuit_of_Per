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
        //public override void Update()
        //{
        //    base.Update();
        //}
        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
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
//                         bulletTimer = 0;
//                     }
//                 }


//             }


//public void GetClosestEnemy(List<Enemy> enemies)
//{
//    target = null;
//    float smallestRange = radius;

//    foreach(Enemy e in enemies)
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
//    Vector2 direction = center - target.Center;
//    direction.Normalize();
//    rotation = (float)Math.Atan2(-direction.X, direction.Y);
//}




//if (EnemyPos >PlayerPos)
//  EnemyPos --;
//else if (EnemyPos < PlayerPos)
//  EnemyPos ++;


