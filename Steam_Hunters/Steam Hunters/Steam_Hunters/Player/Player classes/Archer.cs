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
        private ParticleEngine particleEnginePowerShoot, particleEngineDash;
        private List<Projectile> powerShootList = new List<Projectile>();
        private Projectile powerShoot;
        protected Point frameSizeReload = new Point(90, 90), currentFrameReload = new Point(0, 0), sheetSizeReload = new Point(8, 1);
        protected Point frameSizeRain = new Point(60, 60), currentFrameRain = new Point(0, 0), sheetSizeRain = new Point(8, 1);
        private double timerTransparent;
        private float spinPlus, spinMinus, timerSinceLastFrameReload, timerSinceLastFrameRain;
        private int lifeTransparent, sheetNbr, milliSecondsPerFrameReload = 200;
        private bool isTransparent, isReloading;
        private Vector2 rainAttackPos, startPos;
        private float timer;

        private Trap trap;
        private List<Trap> trapList = new List<Trap>();
        private bool rainTargetActive;
        private bool rightTriggerPressed;
        RainAttack rainAttack;
        private List<RainAttack> rainAttackList = new List<RainAttack>();
        private bool isBPress, addTrap;
        Random rnd = new Random();
        float popOutRainTimer;
        Random rainRandom = new Random();
        private bool addRain, addPos;
        bool canPlace;

        bool getThumstickValue;
        float x;
        float y;
        Vector2 leftStickValue;
        bool availableBPress;

        public Archer(Texture2D tex, Texture2D HUDPic, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, int damage, PlayerIndex playerIndex)
            : base(tex, HUDPic, pos, window, gps, hp, mana, speed, damage, playerIndex)
        {
            projTex = TextureManager.arrowBasic;
            this.particleEnginePowerShoot = new ParticleEngine(TextureManager.steamTextures, pos, Color.Yellow);
            this.particleEngineDash = new ParticleEngine(TextureManager.steamTextures, pos, Color.Yellow);


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
            lifeTransparent = 2000;
            projectileTimerLife = 200;
            offsetBullet = new Vector2(12, 30);
            frameSize = new Point(45, 45);
            availableBPress = true;
            //                                                      name, int, str, agil, vit, luck, hp, mp, lvl 
            statusWindow = new StatusWindow(TextureManager.turretBullet, pos, "hej", 0, 0, 0, 0, 0, hp, mana, 100, playerIndex);
            rainTargetActive = false;
            rightTriggerPressed = false;
        }

        public override void Update(GameTime gameTime)
        {
            color = Color.White;
            statusWindow.SetPos = pos;
            statusWindow.Update(gameTime);
            particleEnginePowerShoot.Update();
            particleEngineDash.Update();
            PowerShoot();
            Transparency(gameTime);
            HandleReload(gameTime);
            RemoveRainArrows();
            RainAttack(gameTime);
            SpeedMove(gameTime);
            AddTrap();

            foreach (Projectile p in powerShootList)
                p.Update(gameTime);
            foreach (Trap t in trapList)
                t.Update(gameTime);
            foreach (RainAttack r in rainAttackList)
                r.Update(gameTime);

            base.Update(gameTime);
            ShootRightThumbStick(newState, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            statusWindow.Draw(spriteBatch);
            particleEnginePowerShoot.Draw(spriteBatch);
            particleEngineDash.Draw(spriteBatch);


            foreach (Trap t in trapList)
                t.Draw(spriteBatch);
            foreach (Projectile p in powerShootList)
                p.Draw(spriteBatch);
            foreach (RainAttack r in rainAttackList)
                r.Draw(spriteBatch);

            base.Draw(spriteBatch);

            //kolla en bit verkligen snurrar rätt runt gubben, gör det för att checka skottens start pos
            //spriteBatch.Draw(tex, new Vector2(pos.X + 30, pos.Y + 30), new Rectangle(0, 0, 20, 20), col, angle, new Vector2(origin.X + 20, origin.Y + 20), 1, EntityFx, 0);

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
                                                     "\npos for rain: " + rainAttackPos +
                                                     "\namount of powershoot: " + powerShootList.Count +
                                                     "\namount of rain arrows: " + rainAttackList.Count +
                                                     "\ncan place bool: " + canPlace +
                                                     "\namount of traps: " + trapList.Count, new Vector2(pos.X + 100, pos.Y + 100), Color.Blue);
            #endregion

            #region circle move
            if (addPos == true)
            {
                spriteBatch.Draw(TextureManager.circles[0], rainAttackPos, new Rectangle(0, 0, 300, 300), Color.White, spinPlus, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[1], rainAttackPos, new Rectangle(0, 0, 300, 300), Color.White, spinMinus, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[2], rainAttackPos, new Rectangle(0, 0, 300, 300), Color.White, spinPlus, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[3], rainAttackPos, new Rectangle(0, 0, 300, 300), Color.White, spinPlus, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[4], rainAttackPos, new Rectangle(0, 0, 300, 300), Color.White, spinMinus, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[5], rainAttackPos, new Rectangle(0, 0, 300, 300), Color.White, spinPlus, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[6], rainAttackPos, new Rectangle(0, 0, 300, 300), Color.White, spinMinus, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
            }
            #endregion
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

        private void RemoveRainArrows()
        {
            for (int i = 0; i < rainAttackList.Count; i++)
            {
                if (rainAttackList[i].remove)
                {
                    rainAttackList.RemoveAt(i);
                }
            }
        }

        private void RainAttack(GameTime gameTime)
        {
            #region regnattacken
            if (Xpress == true)
                rainTargetActive = true;

            if (rainTargetActive == true)
            {
                rainAttackPos = pos;
                //rainAttack = new RainAttack(rainAttackPos, TextureManager.rainTex, frameSizeRain, sheetSizeRain, 80, true);
                //rainAttackList.Add(rainAttack);

                addPos = true;
                rainTargetActive = false;
            }
            if (addPos)
            {
                particleEnginePowerShoot.EmitterLocation = new Vector2(pos.X, pos.Y);
                particleEnginePowerShoot.total = 5;

                rainAttackPos.X += newState.ThumbSticks.Right.X * 6;
                rainAttackPos.Y -= newState.ThumbSticks.Right.Y * 6;

                //rainAttackPos.X = startPos.X + newState.ThumbSticks.Right.X * 6;
                //rainAttackPos.Y = startPos.Y - newState.ThumbSticks.Right.Y * 6;

                spinPlus += (float)gameTime.ElapsedGameTime.TotalSeconds;
                spinMinus -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (RTpress == true)
                {
                    rightTriggerPressed = true;
                    addPos = false;
                }


            }
            //else
            //{
            //    rainAttackPos = pos;
            //}



            if (rightTriggerPressed == true && Xpress == false)
            {
                addRain = true;


                //timerSinceLastFrameRain += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                //if (timerSinceLastFrameRain >= 80)
                //{
                //    ++currentFrameRain.X;
                //    timerSinceLastFrameRain = 0;
                //    if (currentFrameRain.X >= sheetSizeRain.X)
                //    {
                //        currentFrameRain.X = 0;
                //    }
                //}

                //if (currentFrameRain.X == 7)
                //{
                //rainTargetActive = false;
                rightTriggerPressed = false;
                //    currentFrameRain.X = 0;
                //}
            }
            #endregion

            if (addRain == true)
            {
                popOutRainTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (popOutRainTimer > rainRandom.Next(0, 20))
                {
                    int valueX = rnd.Next(0, 300);
                    int valueY = rnd.Next(0, 300);

                    rainAttack = new RainAttack(new Vector2(rainAttackPos.X - 150 + valueX, rainAttackPos.Y - 150 + valueY), TextureManager.rainTex, frameSizeRain, sheetSizeRain, 80, true);
                    rainAttackList.Add(rainAttack);
                    popOutRainTimer = 0;

                    if (rainAttackList.Count == 30)
                        addRain = false;
                }
            }
        }

        private void SpeedMove(GameTime gameTime)
        {
            if (availableBPress)
            {
                if (newState.Buttons.B == ButtonState.Pressed && (newState.ThumbSticks.Left.X != 0 || newState.ThumbSticks.Left.Y != 0))
                {
                    isBPress = true;
                    getThumstickValue = true;
                }
            }

            if (isBPress == true)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (getThumstickValue)
                {
                    //ma.X = GamePad.GetState(playerIndex, GamePadDeadZone.None).ThumbSticks.Left.X;//har jag igång none så funkar det, konstigt
                    //ma.Y = -GamePad.GetState(playerIndex, GamePadDeadZone.None).ThumbSticks.Left.Y;
                    leftStickValue.X = newState.ThumbSticks.Left.X;//annars funkar dessa
                    leftStickValue.Y = -newState.ThumbSticks.Left.Y;
                    //angle = (float)Math.Atan2(GamePad.GetState(playerIndex, GamePadDeadZone.None).ThumbSticks.Left.X, GamePad.GetState(playerIndex, GamePadDeadZone.None).ThumbSticks.Left.Y);
                    angle = (float)Math.Atan2(newState.ThumbSticks.Left.X, newState.ThumbSticks.Left.Y);
                    getThumstickValue = false;
                }

                //if (ma.X == 0 && ma.Y == 0)
                //{
                //    x = prevThumbStickLeftValue.X;
                //    y = prevThumbStickLeftValue.Y;
                //}

                Vector2 specialMove = new Vector2(leftStickValue.X, leftStickValue.Y);
                specialMove.Normalize();

                if (timer < 500)
                {
                    isArcherMoving = false;

                    specialMove.Normalize();
                    pos += specialMove * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    particleEngineDash.EmitterLocation = new Vector2(pos.X, pos.Y);
                    particleEngineDash.total = 15;
                    //speed += 5;
                    //pos += direction * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds; 
                    availableBPress = false;
                }
                else
                {
                    availableBPress = true;
                    particleEngineDash.total = 0;
                    isBPress = false;
                    timer = 0;
                    isArcherMoving = true;
                }

            }

            String temp = pos.X.ToString() + " " + pos.Y.ToString();
            window.Title = temp;
        }

        private void AddTrap()
        {
            if (Ypress == true)
            {
                trap = new Trap(pos, TextureManager.archerTrap, new Point(50, 50), new Point(3, 1), 500, true);
                canPlace = true;
                for (int i = 0; i < trapList.Count; i++)
                {
                    if (trap != trapList[i] && trap.hitBox.Intersects(trapList[i].hitBox))
                    {
                        canPlace = false;
                    }
                }

                if (canPlace)
                {
                    trapList.Add(trap);
                }
                //for (int i = 0; i < trapList.Count; i++)
                //{
                //foreach (Trap t in trapList)
                //{
                //if (!t.hitBox.Intersects(trapList[i].hitBox))
                //{

                //}
                //else
                //{

                //}
                //} 
                //}
            }
        }
    }
}
