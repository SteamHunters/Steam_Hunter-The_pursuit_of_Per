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
        public List<Projectile> FireBallList = new List<Projectile>();
        public List<Projectile> WaterBallList = new List<Projectile>();
        public List<Projectile> BoulderList = new List<Projectile>();
        private ParticleEngine particleEngineWater, particleEngineFire, particleEngineRocks; 
        private double timerWindRuch;
        private int  timeWindRuch;
        public bool windruchOn, boulderOn, shieldActivated;
        private float boulderspeed = 0.08f;



        public Wizard(Texture2D tex, Texture2D HUDPic, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, int damage, PlayerIndex playerIndex)
            : base(tex, HUDPic, pos, window, gps, hp, mana, speed, damage, playerIndex)
        {
            this.timeWindRuch = 1000;
            this.particleEngineWater = new ParticleEngine(TextureManager.steamTextures, pos, Color.Blue);
            this.particleEngineFire = new ParticleEngine(TextureManager.steamTextures, pos, Color.Red);
            this.particleEngineRocks = new ParticleEngine(TextureManager.steamTextures, pos, Color.Gray);
            this.damage = 10;

            //                                                      name, int, str, agil, vit, luck, hp, mp, lvl 
            statusWindow = new StatusWindow(TextureManager.turretBullet, pos, "Sir Anton", 20, 0, 0, 0, 0, hp, mana, 100, playerIndex);

            projTex = TextureManager.bulletWiz;
            projectileTimerLife = 500;
            offsetBullet = new Vector2(-8, 20);
            frameSize = new Point(45, 45);
        }


        public override void Update(GameTime gameTime)
        {
            if (paused == false)
            {
                particleEngineSteam.Update();
                particleEngineWater.Update();
                particleEngineFire.Update();
                particleEngineRocks.Update();
                statusWindow.SetPos = pos;
                statusWindow.Update(gameTime);

                if (shieldActivated == true)
                    time = (float)gameTime.ElapsedGameTime.TotalSeconds;


                if (buying == false && statusWindow.active == false && ghostMode == false)
                {
                    //klar
                    #region Attack A
                    if (Apress == true)
                    {
                        if (statusWindow.mana >= 35)
                        {
                            Vector2 FireAngle = new Vector2(prevThumbStickRightValue.X, prevThumbStickRightValue.Y);
                            Projectile f = new Projectile(pos, TextureManager.fireBall, FireAngle, angle, new Vector2(0, 0), 0.35f, 80, new Point(40, 40), new Point(4, 1), 60, true);
                            FireBallList.Add(f);
                            particleEngineFire.EmitterLocation = new Vector2(pos.X, pos.Y);
                            particleEngineFire.total = 15;
                            rumble.Vibrate(0.1f, 1f);
                            statusWindow.mana -= 35;

                        }
                    }
                    else
                        particleEngineFire.total = 0;
                    #endregion
                    //klar
                    #region Attack X
                    if (Xpress == true)
                    {
                        if (statusWindow.mana >= 35)
                        {
                            Vector2 FireAngle = new Vector2(prevThumbStickRightValue.X, prevThumbStickRightValue.Y);
                            Projectile w = new Projectile(pos, TextureManager.waterBall, FireAngle, angle, new Vector2(0, 0), 0.35f, 80, new Point(40, 40), new Point(4, 1), 60, true);
                            WaterBallList.Add(w);
                            particleEngineWater.EmitterLocation = new Vector2(pos.X, pos.Y);
                            particleEngineWater.total = 15;
                            rumble.Vibrate(0.1f, 1f);
                            statusWindow.mana -= 35;
                        }
                    }
                    else
                        particleEngineWater.total = 0;
                    #endregion
                    //klar
                    #region Attack B
                    if (Bpress == true)
                    {
                        if (statusWindow.mana >= 20)
                        {
                            windruchOn = true;
                            statusWindow.mana -= 20;
                        }
                    }
                    #endregion
                    //Klar
                    #region Attack Y
                    if (Ypress == true && boulderOn == false)
                    {
                        if (statusWindow.mana >= 100)
                        {
                            Vector2 FireAngle = new Vector2(prevThumbStickRightValue.X, prevThumbStickRightValue.Y);
                            Projectile b = new Projectile(pos, TextureManager.BoulderSheetTexture, FireAngle, angle, new Vector2(0, 0), 0.25f, 120, new Point(72, 72), new Point(5, 4), 45, true);
                            BoulderList.Add(b);
                            boulderOn = true;
                            statusWindow.mana -= 100;
                        }
                    }
                    #endregion

                    #region Mana Shield
                    if (LTpress == true)
                    {
                        if (statusWindow.mana > 0)
                        {
                            shieldActivated = true;
                            statusWindow.mana -= 5 * time;
                        }
                        else
                            shieldActivated = false;
                    }
                    else
                        shieldActivated = false;
                    #endregion
                }


                if (statusWindow.hp < statusWindow.maxHp)
                {
                    if (ghostMode == false)
                        statusWindow.hp += 2 * ((1 + (statusWindow.vitality / 20)) * time / 2);
                }


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

                if (windruchOn == true)
                {
                    particleEngineSteam.EmitterLocation = new Vector2(pos.X, pos.Y);
                    particleEngineSteam.total = 20;
                    if (speed <= oldSpeed + 3)
                        speed += 0.5f;

                    timerWindRuch += gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (timerWindRuch >= timeWindRuch)
                    {
                        windruchOn = false;
                        timerWindRuch = 0;

                    }
                }
                if (windruchOn == false)
                {
                    if (speed > oldSpeed)
                        speed -= 0.2f;
                    particleEngineSteam.total = 0;
                }

                #endregion

                #region Update Boulder
                if (boulderOn == true)
                {
                    rumble.Vibrate(0.15f, 0.20f);
                    foreach (Projectile b in BoulderList)
                    {
                        particleEngineRocks.EmitterLocation = new Vector2(b.pos.X, b.pos.Y);
                        particleEngineRocks.total = 15;
                        if (b != null || BoulderList.Count == 0)
                        {
                            b.Update(gameTime);
                            b.direction.X += (float)(newState.ThumbSticks.Right.X * boulderspeed);
                            b.direction.Y -= (float)(newState.ThumbSticks.Right.Y * boulderspeed);
                            b.direction.Normalize();
                        }
                        if (b.BulletRemove == true || boulderOn == false)
                        {
                            particleEngineRocks.total = 0;
                            BoulderList.Remove(b);
                            boulderOn = false;
                            break;
                        }
                    }
                }
                #endregion

                ShootRightThumbStick(newState, gameTime);
            }
                base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
           
            particleEngineFire.Draw(spriteBatch);
            particleEngineWater.Draw(spriteBatch);
            particleEngineSteam.Draw(spriteBatch);
            particleEngineRocks.Draw(spriteBatch);

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

            #region Draw Boulder
            foreach (Projectile b in BoulderList)
            {
                if (b != null)
                    b.Draw(spriteBatch);
            }
            #endregion

            

            base.Draw(spriteBatch);
            
            if (shieldActivated)
            {
                spriteBatch.Draw(TextureManager.magicShield, pos, new Rectangle(0, 0, TextureManager.magicShield.Width, TextureManager.magicShield.Height), Color.White, 0, new Vector2(TextureManager.magicShield.Width / 2, TextureManager.magicShield.Height / 2), 1, SpriteEffects.None, 0);

            }
        }

        #region Metod
        #endregion
    }
}
