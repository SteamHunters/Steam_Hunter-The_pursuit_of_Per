using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class Wizard : Player
    {
        private List<Projectile> FireBallList = new List<Projectile>();
        private List<Projectile> WaterBallList = new List<Projectile>();
        private double timerWindRuch;
        private int oldSpeed, timeWindRuch;
        private bool windruch;
        public Wizard(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, PlayerIndex playerIndex)
            : base(tex, pos, window, gps, hp, mana, speed, playerIndex)
        {
            this.oldSpeed = speed;
            this.timeWindRuch = 1000;

            //                                                      name, int, str, agil, vit, luck, hp, mp, lvl 
            statusWindow = new StatusWindow(TextureManager.turretBullet, pos, "Sir Anton", 0, 0, 0, 0, 0, hp, mana, 1, playerIndex);
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            statusWindow.SetPos = pos;
            statusWindow.Update(gameTime);
            //klar
            #region Attack A
            if (Apress == true)
            {
                Vector2 FireAngle = new Vector2(prevThumbStickRightValue.X, prevThumbStickRightValue.Y);
                Projectile f = new Projectile(pos, TextureManager.fireBall, FireAngle, angle, 0.35f, 80, new Point(40, 40), new Point(4, 1), 60, true);
                FireBallList.Add(f);
            }
            #endregion
            //klar
            #region Attack X
            if (Xpress == true)
            {
                Vector2 FireAngle = new Vector2(prevThumbStickRightValue.X, prevThumbStickRightValue.Y);
                Projectile w = new Projectile(pos, TextureManager.waterBall, FireAngle, angle, 0.35f, 80, new Point(40, 40), new Point(4, 1), 60, true);
                WaterBallList.Add(w);
            }
            #endregion
            //klar
            #region Attack B
            if (Bpress == true)
            {
                windruch = true;
            }
            #endregion

            #region Attack Y
            if (Ypress == true)
            {

            }
            #endregion




            #region Update Fireball
            foreach (Projectile f in FireBallList)
            {
                if (f != null)
                    f.Update(gameTime);

                if (f.BulletRemove == true)
                {
                    FireBallList.Remove(f);
                    break;
                }
            }
            #endregion

            #region Update waterball
            foreach (Projectile w in WaterBallList)
            {
                if (w != null)
                    w.Update(gameTime);

                if (w.BulletRemove == true)
                {
                    WaterBallList.Remove(w);
                    break;
                }
            }
            #endregion

            #region Update Windruch

            if (windruch == true)
            {
                if(speed <= 6)
                speed += 0.5f;

                timerWindRuch += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timerWindRuch >= timeWindRuch)
                {
                    windruch = false;
                    timerWindRuch = 0;
                    
                }
            }
            if (windruch == false)
            {
                if(speed > oldSpeed)
                    speed -= 0.2f;
            }
                
            #endregion

            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            statusWindow.Draw(spriteBatch);


            #region Draw Fireball
            foreach (Projectile f in FireBallList)
            {
                if (f != null)
                    f.Draw(spriteBatch);
            }
            #endregion

            #region Draw waterball
            foreach (Projectile w in WaterBallList)
            {
                if (w != null)
                    w.Draw(spriteBatch);
            }
            #endregion

            base.Draw(spriteBatch);
        }


        public void BoulderUpdate_Animation()
        {

        }

    }
}
