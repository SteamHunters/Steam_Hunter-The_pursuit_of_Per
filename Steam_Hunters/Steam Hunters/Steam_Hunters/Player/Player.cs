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
        public Color color = Color.White;
        
        Vector2 prevThumbStickRightValue;

        List<Projectile> listProjectile = new List<Projectile>();
        Projectile projectile;

        PlayerIndex playerIndex;

        private GamePadState newState, oldState; 
        
        public int bY, bX, speed, hp, mana, projectileTimerLife;
        public Vector2 direction = Vector2.Zero;
        public Vector2 bulletDirection, force, prevPos;

        float PrevAngle, shootTimer, rightTriggerTimer, rightTriggerValue, lefthTriggerValue;
        bool notMoved, shootOneAtTime;

        int showButton;
        double sec;
        bool showButtonCounter;
        protected bool Apress, Bpress, Xpress, Ypress, RTpress, RBpress, LBpress, Duppress, Drightpress, Dlefthpress, Ddownpress, Startpress, Backpress;
        
        public bool LTpress;

        public Vector2 towerDirection;


        public Player(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, int playerIndex)
            : base(tex, pos)
        {
            this.window = window;
            this.gps = gps;
            this.hp = hp;
            this.mana = mana;
            this.speed = speed;
            notMoved = true;
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
            prevPos = pos;
            newState = GamePad.GetState(playerIndex);

            #region Update button presss and with player index
            AButton(playerIndex);
            XButton(playerIndex);
            BButton(playerIndex);
            YButton(playerIndex);

            RTButton(playerIndex);
            RBButton(playerIndex);
            LTButton(playerIndex);
            LBButton(playerIndex);

            DUpButton(playerIndex);
            DDownButton(playerIndex);
            DRightButton(playerIndex);
            DLefthButton(playerIndex);
            #endregion

            MoveLeftThumbStick(newState);
            ShootRightThumbStick(newState, gameTime);
            changeDirection();

            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);

           

            oldState = GamePad.GetState(playerIndex);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height), null, color, angle, new Vector2(tex.Width / 2, tex.Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(tex, hitBox, Color.Red);

            foreach (Projectile e in listProjectile)
            {
                e.Draw(spriteBatch);
            }
            spriteBatch.DrawString(FontManager.font, "value: " + prevThumbStickRightValue +
                                                        "\npos: " + pos +
                                                        "\nshoot timer: " + shootTimer +
                                                        "\namount of proj: " + listProjectile.Count, new Vector2(200, 200), Color.Red);  
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
        public void LBButton(PlayerIndex playerIndex)
        {
            if (newState.Buttons.LeftShoulder == ButtonState.Pressed && oldState.Buttons.LeftShoulder == ButtonState.Released)
            {
                this.LBpress = true;
            }
            else
                this.LBpress = false;
        }
        public void RBButton(PlayerIndex playerIndex)
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
            projectile = new Projectile(pos, TextureManager.arrowBasic, insertDirection, angle, new Point(), new Point());
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

