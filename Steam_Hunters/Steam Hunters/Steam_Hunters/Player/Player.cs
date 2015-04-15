using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class Player : GameObject
    {
          private enum Potion
        {
            Health,
            Mana,
            Ress,
            Buff
        }

        #region Variabler
        protected SpriteEffects EntityFx = 0;

        protected PlayerIndex playerIndex;
        private Potion selectedPotion;
        protected Point sheetSize = new Point(4, 2), currentFrame = new Point(0, 0), frameSize = new Point(45, 45);

        protected Texture2D projTex;

        public Vector2 direction = Vector2.Zero, prevPos, towerDirection, prevThumbStickRightValue, offsetBullet;

        public GameWindow window;
        public GamePlayScreen gps;

        public Color color = Color.White;

        protected List<Projectile> listProjectile = new List<Projectile>();
        protected Projectile projectile;

        protected GamePadState newState, oldState;

        protected Rumble rumble;

        protected ParticleEngine particleEngineSteam;

        public int reloadCount, healthPotion, manaPotion, ressPotion, buffPotion, hp, mana, projectileTimerLife, gold;
        protected int timerSinceLastFrame = 0, milliSecondsPerFrame = 75;

        public float PrevAngle, shootTimer, rightTriggerTimer, rightTriggerValue, lefthTriggerValue, speed;

        public bool LTpress, LBpress, buying, Backpress, notMoved, shootOneAtTime;
        protected bool Apress, Bpress, Xpress, Ypress, RTpress, RBpress, Duppress, Drightpress, Dlefthpress, Ddownpress, Startpress, isShooting, isOccupied;
        #endregion

        protected StatusWindow statusWindow;




        // Varför? vi använder den inte? eller?
        public GraphicsDevice graphics;
        // ta bort sen?
        double sec;
        bool showButtonCounter;

        public Player(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, PlayerIndex playerIndex)
            : base(tex, pos)
        {
            this.window = window;
            this.gps = gps;
            this.hp = hp;
            this.mana = mana;
            this.speed = speed;
            this.graphics = graphics;
            this.rumble = new Rumble(playerIndex);
            notMoved = true;
            //projectileTimerLife = 2000;
            shootOneAtTime = true;
            showButtonCounter = true;
            isShooting = true;

            this.center = new Vector2(pos.X + frameSize.X / 2, pos.Y + frameSize.Y / 2);
            particleEngineSteam = new ParticleEngine(TextureManager.steamTextures, pos, Color.White);
           
            this.playerIndex = playerIndex;
           
        }


        public override void Update(GameTime gameTime)
        {
            newState = GamePad.GetState(playerIndex);
            #region Update button presss and with player index
            AButton(playerIndex);
            XButton(playerIndex);
            BButton(playerIndex);
            YButton(playerIndex);

            RTButton(playerIndex);
            RSButton(playerIndex);
            LTButton(playerIndex);
            LSButton(playerIndex);

            DUpButton(playerIndex);
            DDownButton(playerIndex);
            DRightButton(playerIndex);
            DLefthButton(playerIndex);

            StartButton(playerIndex);
            BackButton(playerIndex);
            #endregion
            prevPos = pos;
            center = new Vector2(pos.X + frameSize.X / 2, pos.Y + frameSize.Y / 2);
            hitBox = new Rectangle((int)pos.X - tex.Width / 12, (int)pos.Y - (int)(tex.Height - tex.Height / 1.3f), tex.Width / 6, tex.Height / 2);

            #region Buying
            if (buying == false)
            {
                MoveLeftThumbStick(newState);
            }
            if(buying == true)
            {
                if (Backpress == true)
                    buying = false;
            }
            #endregion

            ShootRightThumbStick(newState, gameTime);
            changeDirection();
            WalkAnimation(gameTime);

            BuyPotions();


            

            if (statusWindow != null)
            {
                if (buying == false)
                {
                    if (Drightpress == true)
                    {
                        statusWindow.active = true;
                    }

                    if (statusWindow.StatusWinwosActiv() == true && Backpress == true)
                        statusWindow.SetStatusWinwosActiv = false;
                }
            }


            oldState = GamePad.GetState(playerIndex);
            rumble.Update((float)gameTime.ElapsedGameTime.TotalSeconds, playerIndex);
        }



        public override void Draw(SpriteBatch spriteBatch)
        {

            //spriteBatch.Draw(tex, new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height), null, color, angle, new Vector2(tex.Width / 2, tex.Height / 2), SpriteEffects.None, 0);
            //spriteBatch.Draw(tex, hitBox, Color.Red);


            spriteBatch.Draw(tex, pos, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), color, angle, new Vector2(frameSize.X / 2, frameSize.Y / 2), 1, EntityFx, 0);

            //spriteBatch.Draw(tex, new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height), null, color, angle, new Vector2(tex.Width / 2, tex.Height / 2), SpriteEffects.None, 0);


            foreach (Projectile e in listProjectile)
            {
                e.Draw(spriteBatch);
            }
                spriteBatch.DrawString(FontManager.font, "value: " + prevThumbStickRightValue +
                                                             "\npos: " + pos +
                                                             "\nshoot timer: " + shootTimer +
                                                             "\namount of proj: " + listProjectile.Count +
                                                             "\namount of bullets: " + reloadCount, new Vector2(pos.X - 100, pos.Y - 200), Color.Red);

                if (buying == true)
                {
                    switch (selectedPotion)
                    {
                        case Potion.Health:
                            #region intelligence
                            spriteBatch.Draw(TextureManager.healthPotionSHOPTexture, new Vector2(pos.X, pos.Y - 150), Color.White);
                            #endregion
                            break;
                        case Potion.Mana:
                            #region strength
                            spriteBatch.Draw(TextureManager.manaPotionSHOPTexture, new Vector2(pos.X, pos.Y - 150), Color.White);
                            #endregion
                            break;
                        case Potion.Buff:
                            #region agility
                            spriteBatch.Draw(TextureManager.buffPotionSHOPTexture, new Vector2(pos.X, pos.Y - 150), Color.White);
                            #endregion
                            break;
                        case Potion.Ress:
                            #region vitality
                            spriteBatch.Draw(TextureManager.ressPotionSHOPTexture, new Vector2(pos.X, pos.Y - 150), Color.White);
                            #endregion
                            break;
                    }
                }
        }

        #region Get gamePad button
        public void AButton(PlayerIndex playerIndex)
        {
            if (newState.Buttons.A == ButtonState.Pressed && oldState.Buttons.A == ButtonState.Released)
            {
                this.Apress = true;
            }
            else
                this.Apress = false;
        }
        public void BButton(PlayerIndex playerIndex)
        {
            if (newState.Buttons.B == ButtonState.Pressed && oldState.Buttons.B == ButtonState.Released)
            {
                this.Bpress = true;
            }
            else
                this.Bpress = false;
        }
        public void XButton(PlayerIndex playerIndex)
        {
            if (newState.Buttons.X == ButtonState.Pressed && oldState.Buttons.X == ButtonState.Released)
            {
                this.Xpress = true;
            }
            else
                this.Xpress = false;
        }
        public void YButton(PlayerIndex playerIndex)
        {
            if (newState.Buttons.Y == ButtonState.Pressed && oldState.Buttons.Y == ButtonState.Released)
            {
                this.Ypress = true;
            }
            else
                this.Ypress = false;
        }

        public void RTButton(PlayerIndex playerIndex)
        {
            rightTriggerValue = newState.Triggers.Right;
            if (rightTriggerValue != 0)
            {
                this.RTpress = true;
            }
            else
                this.RTpress = false;
        }
        public void LTButton(PlayerIndex playerIndex)
        {
            lefthTriggerValue = newState.Triggers.Left;
            if (lefthTriggerValue != 0)
            {
                this.LTpress = true;
            }
            else
                this.LTpress = false;
        }
        public void LSButton(PlayerIndex playerIndex)
        {
            if (newState.Buttons.LeftShoulder == ButtonState.Pressed && oldState.Buttons.LeftShoulder == ButtonState.Released)
            {
                this.LBpress = true;
            }
            else
                this.LBpress = false;
        }
        public void RSButton(PlayerIndex playerIndex)
        {
            if (newState.Buttons.RightShoulder == ButtonState.Pressed && oldState.Buttons.RightShoulder == ButtonState.Released)
            {
                this.RBpress = true;
            }
            else
                this.RBpress = false;
        }

        public void StartButton(PlayerIndex playerIndex)
        {
            if (newState.Buttons.Start == ButtonState.Pressed && oldState.Buttons.Start == ButtonState.Released)
            {
                this.Startpress = true;
            }
            else
                this.Startpress = false;
        }
        public void BackButton(PlayerIndex playerIndex)
        {
            if (newState.Buttons.Back == ButtonState.Pressed && oldState.Buttons.Back == ButtonState.Released)
            {
                this.Backpress = true;
            }
            else
                this.Backpress = false;
        }

        public void DUpButton(PlayerIndex playerIndex)
        {
            if (newState.DPad.Up == ButtonState.Pressed && oldState.DPad.Up == ButtonState.Released)
            {
                this.Duppress = true;
            }
            else
                this.Duppress = false;
        }
        public void DDownButton(PlayerIndex playerIndex)
        {
            if (newState.DPad.Down == ButtonState.Pressed && oldState.DPad.Down == ButtonState.Released)
            {
                this.Ddownpress = true;
            }
            else
                this.Ddownpress = false;
        }
        public void DRightButton(PlayerIndex playerIndex)
        {
            if (newState.DPad.Right == ButtonState.Pressed && oldState.DPad.Right == ButtonState.Released)
            {
                this.Drightpress = true;
            }
            else
                this.Drightpress = false;
        }
        public void DLefthButton(PlayerIndex playerIndex)
        {
            if (newState.DPad.Left == ButtonState.Pressed && oldState.DPad.Left == ButtonState.Released)
            {
                this.Dlefthpress = true;
            }
            else
                this.Dlefthpress = false;
        }
        #endregion

        private void BuyPotions()
        {
            if (buying == true)
            {
                switch (selectedPotion)
                {
                    case Potion.Health:
                        #region Health
                        if (Drightpress == true)
                        {
                            selectedPotion = Potion.Mana;
                        }
                        if (Dlefthpress == true)
                        {
                            selectedPotion = Potion.Ress;
                        }
                        if (Apress == true)
                        {
                            if (manaPotion == 0 && ressPotion == 0 && buffPotion == 0)
                            {
                                healthPotion += 1;
                                gold -= 100;
                            }
                            if (Bpress == true && healthPotion >= 1)
                            {
                                healthPotion -= 1;
                                gold += 50;
                            }
                        }
                        #endregion
                        break;
                    case Potion.Mana:
                        #region Mana
                        if (Drightpress == true)
                        {
                            selectedPotion = Potion.Buff;
                        }
                        if (Dlefthpress == true)
                        {
                            selectedPotion = Potion.Health;
                        }
                        if (Apress == true)
                        {
                            if (healthPotion == 0 && ressPotion == 0 && buffPotion == 0)
                            {
                                manaPotion += 1;
                                gold -= 100;
                            }
                            if (Bpress == true && manaPotion >= 1)
                            {
                                manaPotion -= 1;
                                gold += 50;
                            }
                        }
                        #endregion
                        break;
                    case Potion.Buff:
                        #region Buff
                        if (Drightpress == true)
                        {
                            selectedPotion = Potion.Ress;
                        }
                        if (Dlefthpress == true)
                        {
                            selectedPotion = Potion.Mana;
                        }
                        if (Apress == true)
                        {
                            if (healthPotion == 0 && ressPotion == 0 && manaPotion == 0)
                            {
                                buffPotion += 1;
                                gold -= 100;
                            }
                            if (Bpress == true && buffPotion >= 1)
                            {
                                buffPotion -= 1;
                                gold += 50;
                            }
                        }
                        #endregion
                        break;
                    case Potion.Ress:
                        #region Ress
                        if (Drightpress == true)
                        {
                            selectedPotion = Potion.Health;
                        }
                        if (Dlefthpress == true)
                        {
                            selectedPotion = Potion.Buff;
                        }
                        if (Apress == true)
                        {
                            if (healthPotion == 0 && manaPotion == 0 && buffPotion == 0)
                            {
                                ressPotion += 1;
                                gold -= 100;
                            }
                            if (Bpress == true && ressPotion >= 1)
                            {
                                ressPotion -= 1;
                                gold += 50;
                            }
                        }
                        #endregion
                        break;
                }
            }
        }

        private void changeDirection()
        {
            PrevAngle = angle;

            angle = (float)Math.Atan2(GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right.X, GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right.Y);

            if (GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right.Length() == 0)
            {
                angle = PrevAngle;
            }
        }

        private void WalkAnimation(GameTime gameTime)
        {
            if (newState.ThumbSticks.Left.X != 0.0f)
            {
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
            else if (newState.ThumbSticks.Left.Y != 0.0f)
            {
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
            else
            {
                currentFrame.X = 0;
                currentFrame.Y = 0;
                timerSinceLastFrame = 0;
            }
        }

        private void MoveLeftThumbStick(GamePadState newState)
        {
            pos.X += newState.ThumbSticks.Left.X * speed;
            pos.Y -= newState.ThumbSticks.Left.Y * speed;
        }
       

        private void ShootRightThumbStick(GamePadState newState, GameTime gameTime)
        {
            rightTriggerValue = newState.Triggers.Right;

            if (notMoved)
            {
                if (rightTriggerValue != 0)
                {
                    if (shootOneAtTime == true)
                    {
                        if (isShooting)
                        {
                            AddProjectile(new Vector2(0, -1));
                            reloadCount++;
                        }
                    }
                    shootOneAtTime = false;

                    if (shootOneAtTime == false)
                    {
                        rightTriggerTimer += gameTime.ElapsedGameTime.Milliseconds;
                        if (rightTriggerTimer > projectileTimerLife)
                        {
                            shootOneAtTime = true;
                        }
                    }
                }
            }
            else
            {
                if (rightTriggerValue != 0)
                {
                    if (shootOneAtTime == true)
                    {
                        if (newState.ThumbSticks.Right.X != 0.0f)
                        {
                            if (isShooting)
                            {
                                AddProjectile(new Vector2(GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right.X,
                                                                             -GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right.Y));
                                reloadCount++;
                            }
                        }
                        else if (newState.ThumbSticks.Right.Y != 0.0f)
                        {
                            if (isShooting)
                            {
                                AddProjectile(new Vector2(GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right.X,
                                                                             -GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right.Y));
                                reloadCount++;
                            }
                        }

                        if (newState.ThumbSticks.Right.X == 0.0f && newState.ThumbSticks.Right.Y == 0.0f)
                        {
                            if (rightTriggerValue != 0)
                            {
                                if (isShooting)
                                {
                                    AddProjectile(new Vector2(prevThumbStickRightValue.X, prevThumbStickRightValue.Y));
                                    reloadCount++;
                                }
                            }
                        }
                        shootOneAtTime = false;

                    }
                    else if (shootOneAtTime == false)//skjuta
                    {
                        rightTriggerTimer += gameTime.ElapsedGameTime.Milliseconds;
                        if (rightTriggerTimer > projectileTimerLife)
                        {
                            shootOneAtTime = true;
                        }
                    }
                }
            }

            RightThumbStickInCenter(newState);
            RemoveProjectile(gameTime);
            UpdateProjectile(gameTime);
        }

        private void RightThumbStickInCenter(GamePadState newState)
        {
            if (newState.ThumbSticks.Right.X != 0.0f)
            {
                prevThumbStickRightValue = new Vector2(GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right.X,
                                                      -GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right.Y);
                notMoved = false;
            }
            else if (newState.ThumbSticks.Right.Y != 0.0f)
            {
                prevThumbStickRightValue = new Vector2(GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right.X,
                                                       -GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right.Y);
                notMoved = false;
            }
        }

        private void AddProjectile(Vector2 insertDirection)
        {
            projectile = new Projectile(new Vector2(pos.X + 10, pos.Y), projTex, insertDirection, angle, offsetBullet, 0.4f, 80, new Point(), new Point(), 0, false);
            towerDirection = insertDirection;
            listProjectile.Add(projectile);

            rightTriggerTimer = 0;
        }

        private void UpdateProjectile(GameTime gameTime)
        {
            foreach (Projectile e in listProjectile)
            {
                e.Update(gameTime);
            }
        }

        private void RemoveProjectile(GameTime gameTime)
        {
            for (int i = 0; i < listProjectile.Count; i++)
            {
                //shootTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (listProjectile[i].BulletRemove)
                {
                    listProjectile.RemoveAt(i);
                    //i--;
                    //shootTimer = 0;
                }

            }
        }

        public void HandleCollision()
        {
            pos = prevPos;
        }


        public GamePlayScreen SetGPS
        {
            get { return gps; }
            set { gps = value; }
        }

    }
}