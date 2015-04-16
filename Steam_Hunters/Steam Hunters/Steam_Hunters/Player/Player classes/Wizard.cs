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
        private List<Projectile> BoulderList = new List<Projectile>();
        private ParticleEngine particleEngineWater, particleEngineFire, particleEngineRocks; 
        private double timerWindRuch;
        private int oldSpeed, timeWindRuch;
        private bool windruchOn, boulderOn, shieldActivated;
        private float boulderspeed = 0.08f, shieldTimer;
        

        public Wizard(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, PlayerIndex playerIndex)
            : base(tex, pos, window, gps, hp, mana, speed, playerIndex)
        {
            this.oldSpeed = speed;
            this.timeWindRuch = 1000;
            this.particleEngineWater = new ParticleEngine(TextureManager.steamTextures, pos, Color.Blue);
            this.particleEngineFire = new ParticleEngine(TextureManager.steamTextures, pos, Color.Red);
            this.particleEngineRocks = new ParticleEngine(TextureManager.steamTextures, pos, Color.Gray);

            //                                                      name, int, str, agil, vit, luck, hp, mp, lvl 
            statusWindow = new StatusWindow(TextureManager.turretBullet, pos, "Sir Anton", 0, 0, 0, 0, 0, hp, mana, 100, playerIndex);

            projTex = TextureManager.bulletWiz;
            projectileTimerLife = 500;
            offsetBullet = new Vector2(-8, 20);
            frameSize = new Point(45, 45);
        }


        public override void Update(GameTime gameTime)
        {
            particleEngineSteam.Update();
            particleEngineWater.Update();
            particleEngineFire.Update();
            particleEngineRocks.Update();
            statusWindow.SetPos = pos;
            statusWindow.Update(gameTime);

            if (buying == false && statusWindow.active == false)
            {
                //klar
                #region Attack A
                if (Apress == true)
                {
                    Vector2 FireAngle = new Vector2(prevThumbStickRightValue.X, prevThumbStickRightValue.Y);
                    Projectile f = new Projectile(pos, TextureManager.fireBall, FireAngle, angle, new Vector2(0, 0), 0.35f, 80, new Point(40, 40), new Point(4, 1), 60, true);
                    FireBallList.Add(f);
                    particleEngineFire.EmitterLocation = new Vector2(pos.X, pos.Y);
                    particleEngineFire.total = 15;
                    rumble.Vibrate(0.1f, 1f);
                }
                else
                    particleEngineFire.total = 0;
                #endregion
                //klar
                #region Attack X
                if (Xpress == true)
                {
                    Vector2 FireAngle = new Vector2(prevThumbStickRightValue.X, prevThumbStickRightValue.Y);
                    Projectile w = new Projectile(pos, TextureManager.waterBall, FireAngle, angle, new Vector2(0, 0), 0.35f, 80, new Point(40, 40), new Point(4, 1), 60, true);
                    WaterBallList.Add(w);
                    particleEngineWater.EmitterLocation = new Vector2(pos.X, pos.Y);
                    particleEngineWater.total = 15;
                    rumble.Vibrate(0.1f, 1f);
                }
                else
                    particleEngineWater.total = 0;
                #endregion
                //klar
                #region Attack B
                if (Bpress == true)
                {
                    windruchOn = true;
                }
                #endregion
                //Klar
                #region Attack Y
                if (Ypress == true && boulderOn == false)
                {
                    Vector2 FireAngle = new Vector2(prevThumbStickRightValue.X, prevThumbStickRightValue.Y);
                    Projectile b = new Projectile(pos, TextureManager.BoulderSheetTexture, FireAngle, angle, new Vector2(0, 0), 0.25f, 120, new Point(72, 72), new Point(5, 4), 45, true);
                    BoulderList.Add(b);
                    boulderOn = true;
                }
                #endregion

                if (LTpress == true)
                {
                    shieldActivated = true;
                }

                

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
                    if (speed <= 6)
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
                        if (b != null)
                        {
                            b.Update(gameTime);
                            b.direction.X += (float)(newState.ThumbSticks.Right.X * boulderspeed);
                            b.direction.Y -= (float)(newState.ThumbSticks.Right.Y * boulderspeed);
                            b.direction.Normalize();
                        }
                        if (b.BulletRemove == true)
                        {
                            particleEngineRocks.total = 0;
                            BoulderList.Remove(b);
                            boulderOn = false;
                            break;
                        }
                    }
                }
                #endregion

                if (shieldActivated)
                {
                    shieldTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (shieldTimer >= 800)
                    {
                        shieldActivated = false;
                        shieldTimer = 0;
                    }
                }

                base.Update(gameTime);
                ShootRightThumbStick(newState, gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            statusWindow.Draw(spriteBatch);
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
