using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Steam_Hunters
{
    class Archer : Player
    {
        private ParticleEngine particleEnginePowerShoot;
        private List<Projectile> powerShootList = new List<Projectile>();
        private Projectile powerShoot;
        protected Point frameSizeReload = new Point(90, 90), currentFrameReload = new Point(0, 0), sheetSizeReload = new Point(8, 1);
        protected Point frameSizeRain = new Point(60, 60), currentFrameRain = new Point(0, 0), sheetSizeRain = new Point(8, 1);
        private double timerTransparent;
        private float spinPlus, spinMinus, timerSinceLastFrameReload, timerSinceLastFrameRain;
        private int lifeTransparent, sheetNbr, triggerPress, milliSecondsPerFrameReload = 200;
        private bool isTransparent, isReloading, isPosSaved;
        private Vector2 rainAttack, savePos;

        private Trap trap;
        private List<Trap> trapList = new List<Trap>();

        public Archer(Texture2D tex,Texture2D HUDPic, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, int damage, PlayerIndex playerIndex)
            : base(tex, HUDPic, pos, window, gps, hp, mana, speed, damage, playerIndex)
        {
            projTex = TextureManager.arrowBasic;
            this.particleEnginePowerShoot = new ParticleEngine(TextureManager.steamTextures, pos, Color.Yellow);

            #region leapa-kod
            //Kod för att leapa, inte riktigt färdig! Men kan byggas på för att få rätt resultat
            //if (prevThumbStickRightValue.X != 0 || prevThumbStickRightValue.Y != 0)
            //{
            //    jump += prevThumbStickRightValue;
            //    jump.Normalize();
            //    pos += jump * 100;
            //}
            #endregion 

            isTransparent = false;
            isReloading = false;
            isPosSaved = true;
            lifeTransparent = 2000;
            projectileTimerLife = 200;
            offsetBullet = new Vector2(12, 30);
            frameSize = new Point(45, 45);

            //                                                      name, int, str, agil, vit, luck, hp, mp, lvl 
            statusWindow = new StatusWindow(TextureManager.turretBullet, pos, "hej", 0, 0, 0, 0, 0, hp, mana, 100, playerIndex);
        }

        public override void Update(GameTime gameTime)
        {
            color = Color.White;
            statusWindow.SetPos = pos;
            statusWindow.Update(gameTime);
            particleEnginePowerShoot.Update();
            PowerShoot();
            Transparency(gameTime);
            HandleReload(gameTime);

            foreach (Projectile p in powerShootList)
                p.Update(gameTime);
            foreach (Trap t in trapList)
                t.Update(gameTime);

            #region regnattacken
            if (newState.Buttons.X == ButtonState.Pressed)//du trycker ner knappen
            {
                //if (isPosSaved)
                //{
                //    savePos = pos;
                //    //isPosSaved = false;
                //}

                rainAttack.X += newState.ThumbSticks.Left.X * 4;//här går du
                rainAttack.Y -= newState.ThumbSticks.Left.Y * 4;//och här

                spinPlus += (float)gameTime.ElapsedGameTime.TotalSeconds;//cirklar snurrar
                spinMinus -= (float)gameTime.ElapsedGameTime.TotalSeconds;


                //if (oldState.Buttons.X == ButtonState.Released)
                //{

                //}

                //speed = 0;
                //isMoving = false;//ser till att metoden för att gå inte funkar längre
                //currentFrame = new Point(0, 0);



                //speed = 0;
                //pos.X += newState.ThumbSticks.Left.X * speed;
                //pos.Y -= newState.ThumbSticks.Left.Y * speed;

                if (RTpress == true)
                {
                    triggerPress = 1;

                }

            }
            else//den är sann från början
            {
                rainAttack = pos;//regnattacken sparar senaste pos
               // isMoving = true;//man kan gå igen
                //isPosSaved = false;
            }

            if (triggerPress == 1 && newState.Buttons.X == ButtonState.Released)
            {
                timerSinceLastFrameRain += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                //if (timerSinceLastFrameReload > milliSecondsPerFrameReload)
                //{
                //    timerSinceLastFrameReload = 0;

                if (timerSinceLastFrameRain >= 80)
                {
                    //timerSinceLastFrameReload -= 200;
                    ++currentFrameRain.X;
                    //sheetNbr++;
                    timerSinceLastFrameRain = 0;
                    if (currentFrameRain.X >= sheetSizeRain.X)
                    {
                        currentFrameRain.X = 0;
                        //++currentFrameReload.Y;
                        //if (currentFrameReload.Y >= sheetSizeReload.Y)
                        //    currentFrameReload.Y = 0;
                    }
                }

                if (currentFrameRain.X == 7)
                {
                    triggerPress = 0;
                    currentFrameRain.X = 0;
                }
            }
            #endregion

            if (Xpress == true)
            {
                trap = new Trap(pos, TextureManager.archerTrap, new Point(50, 50), new Point(3, 1), 2000, true);
                trapList.Add(trap);
            }

            base.Update(gameTime);
            ShootRightThumbStick(newState, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            statusWindow.Draw(spriteBatch);
            particleEnginePowerShoot.Draw(spriteBatch);
            base.Draw(spriteBatch);

            //kolla en bit verkligen snurrar rätt runt gubben, gör det för att checka skottens start pos
            //spriteBatch.Draw(tex, new Vector2(pos.X + 30, pos.Y + 30), new Rectangle(0, 0, 20, 20), col, angle, new Vector2(origin.X + 20, origin.Y + 20), 1, EntityFx, 0);

            foreach (Trap t in trapList)
                t.Draw(spriteBatch);

            #region shoot
            if (isReloading == true)
            {
                spriteBatch.Draw(TextureManager.reload, pos, new Rectangle(currentFrameReload.X * frameSizeReload.X, currentFrameReload.Y * frameSizeReload.Y, frameSizeReload.X, frameSizeReload.Y), Color.White, 0, new Vector2(frameSizeReload.X / 2, frameSizeReload.Y / 2), 1, SpriteEffects.None, 0);
                isShooting = false;
            }
            else
            {
                isShooting = true;
            }
            #endregion

            #region font, check values
            spriteBatch.DrawString(FontManager.font, "tid: " + timerSinceLastFrameReload + "  " + milliSecondsPerFrameReload +
                                                     "\nbool: " + isTransparent +
                                                     "\ncolor: " + color +
                                                     "\npos for rain: " + rainAttack +
                                                     "\namount of powershoot: " + powerShootList.Count, new Vector2(pos.X + 100, pos.Y + 100), Color.Blue);
            #endregion

            #region circle move
            if (newState.Buttons.X == ButtonState.Pressed)
            {
                spriteBatch.Draw(TextureManager.circles[0], rainAttack, new Rectangle(0, 0, 300, 300), Color.White, spinPlus, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[1], rainAttack, new Rectangle(0, 0, 300, 300), Color.White, spinMinus, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[2], rainAttack, new Rectangle(0, 0, 300, 300), Color.White, spinPlus, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[3], rainAttack, new Rectangle(0, 0, 300, 300), Color.White, spinPlus, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[4], rainAttack, new Rectangle(0, 0, 300, 300), Color.White, spinMinus, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[5], rainAttack, new Rectangle(0, 0, 300, 300), Color.White, spinPlus, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[6], rainAttack, new Rectangle(0, 0, 300, 300), Color.White, spinMinus, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
            }
            #endregion

            #region rita ut regnattacken
            //savePos = rainAttack;
            if (triggerPress == 1 /*&& newState.Buttons.X == ButtonState.Released*/)
            {
                //isPosSaved = false;
                for (int i = 0; i < 15; i++)
                {
                    spriteBatch.Draw(TextureManager.rainTex, rainAttack * i, new Rectangle(currentFrameRain.X * frameSizeRain.X, currentFrameRain.Y * frameSizeRain.Y, frameSizeRain.X, frameSizeRain.Y), Color.White, 0, new Vector2(frameSizeRain.X / 2, frameSizeRain.Y / 2), 1, SpriteEffects.None, 0);

                    if (i == 14)
                    {
                    }
                }
                //rainAttack = pos;
            }
            else
            {
                isPosSaved = true;
            }
            #endregion 

            foreach (Projectile p in powerShootList)
                p.Draw(spriteBatch);
        }

        private void PowerShoot()
        {
            if (Apress == true)
            {
                Vector2 powerShootAngle = new Vector2(prevThumbStickRightValue.X, prevThumbStickRightValue.Y);
                powerShoot = new Projectile(new Vector2(pos.X + 10, pos.Y), TextureManager.arrowPowerShoot, powerShootAngle, angle, new Vector2(0, 0), 0.5f, 80, new Point(20, 35), new Point(3, 1), 100, true);
                powerShootList.Add(powerShoot);
                particleEnginePowerShoot.EmitterLocation = new Vector2(pos.X, pos.Y);
                particleEnginePowerShoot.total = 5;
            }
            else
                particleEnginePowerShoot.total = 0;
        }

        private void Transparency(GameTime gameTime)
        {
            if (LTpress == true)
                isTransparent = true;

            if (isTransparent == true)
            {
                timerTransparent += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timerTransparent >= lifeTransparent)
                {
                    color = Color.White;
                    isTransparent = false;
                    timerTransparent = 0;
                }
                else
                {
                    color = new Color(100, 100, 100, 100);
                }
            }
        }

        private void HandleReload(GameTime gameTime)
        {
            if (reloadCount >= 12)
            {
                isReloading = true;
                reloadCount = 0;

            }

            if (isReloading == true)
            {
                timerSinceLastFrameReload += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                //if (timerSinceLastFrameReload > milliSecondsPerFrameReload)
                //{
                //    timerSinceLastFrameReload = 0;

                if (timerSinceLastFrameReload >= 200)
                {
                    //timerSinceLastFrameReload -= 200;
                    ++currentFrameReload.X;
                    sheetNbr++;
                    timerSinceLastFrameReload = 0;
                    if (currentFrameReload.X >= sheetSizeReload.X)
                    {
                        currentFrameReload.X = 0;
                        //++currentFrameReload.Y;
                        //if (currentFrameReload.Y >= sheetSizeReload.Y)
                        //    currentFrameReload.Y = 0;
                    }
                }
                //}

            }
            if (sheetNbr >= 8)
            {
                isReloading = false;
                //currentFrameReload.X = 0;
                timerSinceLastFrameReload = 0;
                sheetNbr = 0;
            }
        }
    }
}
