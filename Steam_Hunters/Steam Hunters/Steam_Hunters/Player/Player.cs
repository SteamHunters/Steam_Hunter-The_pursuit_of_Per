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


        int bY, bX, speed, hp, mana;
        public Vector2 direction = Vector2.Zero;
        public Vector2 bulletDirection, force;

       // public int playerDamagedCounter; // fråga sebbe om denna
        float PrevAngle, shootTimer, rightTriggerTimer, rightTriggerValue;
        bool notMoved;

        public Player(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps)
            : base(tex, pos)
        {
            this.gps = gps;
            this.window = window;
            notMoved = true;
            speed = 10;
        }
        public override void Update(GameTime gameTime)
        {
            GamePadState newState = GamePad.GetState(PlayerIndex.One);

            MoveLeftThumbStick(newState);
            ShootRightThumbStick(newState, gameTime);
            changeDirection();

            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
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
        }

        private void changeDirection()
        {
            PrevAngle = angle;

            angle = (float)Math.Atan2(GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.X, GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.Y);

            if (GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.Length() == 0)
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

                    rightTriggerTimer += gameTime.ElapsedGameTime.Milliseconds;

                    if (rightTriggerTimer > 200)
                    {
                        AddProjectile(new Vector2(0, -1));
                    }
                }
            }
            else
            {
                if (rightTriggerValue != 0)
                {
                    rightTriggerTimer += gameTime.ElapsedGameTime.Milliseconds;

                    if (rightTriggerTimer > 200)
                    {
                        if (newState.ThumbSticks.Right.X != 0.0f)
                        {
                            AddProjectile(new Vector2(GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.X,
                                                     -GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.Y));
                        }
                        else if (newState.ThumbSticks.Right.Y != 0.0f)
                        {
                            AddProjectile(new Vector2(GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.X,
                                                     -GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.Y));
                        }

                        if (newState.ThumbSticks.Right.X == 0.0f && newState.ThumbSticks.Right.Y == 0.0f)
                        {
                            if (rightTriggerValue != 0)
                            {
                                AddProjectile(new Vector2(prevThumbStickRightValue.X, prevThumbStickRightValue.Y));
                            }
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
                prevThumbStickRightValue = new Vector2(GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.X,
                                                      -GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.Y);
                notMoved = false;
            }
            else if (newState.ThumbSticks.Right.Y != 0.0f)
            {
                prevThumbStickRightValue = new Vector2(GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.X,
                                                       -GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.Y);
                notMoved = false;
            }
        }

        private void AddProjectile(Vector2 insertDirection)
        {
            projectile = new Projectile(pos, TextureManager.testTexture, insertDirection, angle, new Point(), new Point());
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

                if (shootTimer > 200)
                {
                    listProjectile.RemoveAt(i);
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
