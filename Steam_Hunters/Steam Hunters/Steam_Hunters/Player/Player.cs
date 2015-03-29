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
        public GameWindow window;
        public GamePlayScreen gps;
        
        Vector2 prevThumbStickRightValue;

        List<Projectile> listProjectile = new List<Projectile>();
        Projectile projectile;

        PlayerIndex playerIndex;

        private GamePadState newState, oldState; 
        
        int bY, bX, speed, hp, mana, projectileTimerLife;
        public Vector2 direction = Vector2.Zero;
        public Vector2 bulletDirection, force;

        float PrevAngle, shootTimer, rightTriggerTimer, rightTriggerValue;
        bool notMoved, shootOneAtTime;

        int showButton;
        double sec;
        bool showButtonCounter;
        protected bool Apress, Bpress, Xpress, Ypress, RTpress, RBpress, LTpress, LBpress, Duppress, Drightpress, Dlefth, Ddownpress, Startpress, Backpress;



        public Player(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps, int playerIndex)
            : base(tex, pos)
        {
            this.gps = gps;
            this.window = window;
            notMoved = true;
            speed = 10;
            projectileTimerLife = 2000;
            shootOneAtTime = true;
            showButton = 0;
            showButtonCounter = true;

            #region Identify player index
            if (playerIndex == 1)
            {
                this.playerIndex = PlayerIndex.One;
            }
            if (playerIndex == 2)
            {
                this.playerIndex = PlayerIndex.Two;
            }
            if (playerIndex == 3)
            {
                this.playerIndex = PlayerIndex.Three;
            }
            if (playerIndex == 4)
            {
                this.playerIndex = PlayerIndex.Four;
            }
            #endregion

            
            
        }


        public override void Update(GameTime gameTime)
        {
            
            newState = GamePad.GetState(this.playerIndex);

            MoveLeftThumbStick(newState);
            ShootRightThumbStick(newState, gameTime);
            changeDirection();

            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);

           // ButtonPress(gameTime);

            #region Update button presss and with player index
            AButton(playerIndex);
            XButton(playerIndex);
            BButton(playerIndex);
            YButton(playerIndex);

            #endregion
            oldState = GamePad.GetState(this.playerIndex);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height), null, Color.White, angle, new Vector2(tex.Width / 2, tex.Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(tex, hitBox, Color.Red);

            foreach (Projectile e in listProjectile)
            {
                e.Draw(spriteBatch);
            }
            spriteBatch.DrawString(TextureManager.font, "value: " + prevThumbStickRightValue +
                                                        "\npos: " + pos +
                                                        "\nshoot timer: " + shootTimer +
                                                        "\namount of proj: " + listProjectile.Count, new Vector2(200, 200), Color.Red);
            #region asfaf
            switch (showButton)
            {
                case 1:
                    spriteBatch.DrawString(TextureManager.font, "\n\n\n\n\n X " + sec, new Vector2(200, 200), Color.Red);
                    break;
                case 2:
                    spriteBatch.DrawString(TextureManager.font, "\n\n\n\n\n Y " + sec, new Vector2(200, 200), Color.Red);
                    break;
                case 3:
                    spriteBatch.DrawString(TextureManager.font, "\n\n\n\n\n A " + sec, new Vector2(200, 200), Color.Red);
                    break;
                case 4:
                    spriteBatch.DrawString(TextureManager.font, "\n\n\n\n\n B " + sec, new Vector2(200, 200), Color.Red);
                    break;
                case 5:
                    spriteBatch.DrawString(TextureManager.font, "\n\n\n\n\n Right Shoulder " + sec, new Vector2(200, 200), Color.Red);                   
                    break;
                case 6:
                    spriteBatch.DrawString(TextureManager.font, "\n\n\n\n\n Left Shoulder " + sec, new Vector2(200, 200), Color.Red);
                    break;
                case 7:
                    spriteBatch.DrawString(TextureManager.font, "\n\n\n\n\n Left Trigger " + sec, new Vector2(200, 200), Color.Red);
                    break;
                case 8:
                    spriteBatch.DrawString(TextureManager.font, "\n\n\n\n\n Right Trigger " + sec, new Vector2(200, 200), Color.Red);
                    break;
            }
            #endregion
        }

        //  oldState = newState;

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
            if (newState.Buttons.RightShoulder == ButtonState.Pressed)
            {
                this.RTpress = true;
            }
            else
                this.RTpress = false;
        }


        
        #endregion
        

        private void ButtonPress(GameTime gT)
        {
            GamePadState newState = GamePad.GetState(playerIndex);
            if (newState.Buttons.X == ButtonState.Pressed &&
                                oldState.Buttons.X == ButtonState.Released)
            {
                // the button has just been pressed
                // do something here
                showButton = 1;
                showButtonCounter = true;
                

            }
            else if (newState.Buttons.Y == ButtonState.Pressed &&
                                oldState.Buttons.Y == ButtonState.Released)
            {
                // the button has just been pressed
                // do something here
                showButton = 2;
                showButtonCounter = true;

            }
            else if (newState.Buttons.A == ButtonState.Pressed &&
                                oldState.Buttons.A == ButtonState.Released)
            {
                // the button has just been pressed
                // do something here
                showButton = 3;
                showButtonCounter = true;

            }
            else if (newState.Buttons.B == ButtonState.Pressed &&
                                oldState.Buttons.B == ButtonState.Released)
            {
                // the button has just been pressed
                // do something here
                showButton = 4;
                showButtonCounter = true;

            }
            else if (newState.Buttons.RightShoulder == ButtonState.Pressed &&
                                oldState.Buttons.RightShoulder == ButtonState.Released)
            {
                // the button has just been pressed
                // do something here
                showButton = 5;
                showButtonCounter = true;

            }
            else if (newState.Buttons.LeftShoulder == ButtonState.Pressed &&
                                oldState.Buttons.LeftShoulder == ButtonState.Released)
            {
                // the button has just been pressed
                // do something here
                showButton = 6;
                showButtonCounter = true;

            }
            else if (newState.Triggers.Left != 0)
            {
                // the button has just been pressed
                // do something here
                showButton = 7;
                showButtonCounter = true;

            }
            else if (newState.Triggers.Right != 0)
            {
                // the button has just been pressed
                // do something here
                showButton = 8;
                showButtonCounter = true;
            }
            else
            {
                showButton = 0;
            }

            if (showButtonCounter == true)
            {
                sec += gT.ElapsedGameTime.TotalSeconds;

                if (sec >= 1)
                {
                    showButton = 0;
                    sec = 0;
                    showButtonCounter = false;
                }
            }
            

            // At the end, we update old state to the state we grabbed at the start of this update.
            // This allows us to reuse it in the next update.
            oldState = newState;
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
                        AddProjectile(new Vector2(0, -1));
                    }
                    shootOneAtTime = false;

                    if (shootOneAtTime == false)
                    {
                        rightTriggerTimer += gameTime.ElapsedGameTime.Milliseconds;
                        if (rightTriggerTimer > 100)
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
                            AddProjectile(new Vector2(GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right.X,
                                                     -GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right.Y));
                        }
                        else if (newState.ThumbSticks.Right.Y != 0.0f)
                        {
                            AddProjectile(new Vector2(GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right.X,
                                                     -GamePad.GetState(playerIndex, GamePadDeadZone.Circular).ThumbSticks.Right.Y));
                        }

                        if (newState.ThumbSticks.Right.X == 0.0f && newState.ThumbSticks.Right.Y == 0.0f)
                        {
                            if (rightTriggerValue != 0)
                            {
                                AddProjectile(new Vector2(prevThumbStickRightValue.X, prevThumbStickRightValue.Y));
                            }
                        }
                        shootOneAtTime = false;

                    }
                    else if (shootOneAtTime == false)
                    {
                        rightTriggerTimer += gameTime.ElapsedGameTime.Milliseconds;
                        if (rightTriggerTimer > 100)
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
            projectile = new Projectile(pos, TextureManager.arrow, insertDirection, angle, new Point(), new Point());
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
                shootTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (shootTimer > projectileTimerLife)
                {
                    listProjectile.RemoveAt(i);
                    i--;
                    shootTimer = 0;
                }

            }
        }


        // skriv om så items används
        //private void DpadControl()
        //{
        //    if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
        //        direction.X = -1;

        //    else if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
        //        direction.X = 1;
        //    else if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Released && GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Released)
        //        direction.X = 0;

        //    if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
        //        direction.Y = -1;

        //    else if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
        //        direction.Y = 1;

        //    else if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Released && GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Released)
        //        direction.Y = 0;
        //}

    }
}
