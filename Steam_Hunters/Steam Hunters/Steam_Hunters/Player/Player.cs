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

        Vector2 dPad_value;

        List<Projectile> listProjectile = new List<Projectile>();
        Projectile projectile;

        int speed = 7;
        public Vector2 direction = Vector2.Zero;
        public Vector2 bulletDirection, force, prevPos;
        public int playerDamagedCounter;
        float PrevAngle;
        float shootTimer;
        float rightTriggerTimer;

        int bY, bX;

        private GamePadState oldState;

        public Player(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps)
            : base(tex, pos)
        {
            this.gps = gps;
            this.window = window;
        }
        public override void Update(GameTime gameTime)
        {

            GamePadState newState = GamePad.GetState(PlayerIndex.One);

            //Vector2 notShoot = newState.ThumbSticks.Right;

            pos.X += newState.ThumbSticks.Left.X * speed;
            pos.Y -= newState.ThumbSticks.Left.Y * speed;


            if (newState.Buttons.RightShoulder == ButtonState.Pressed &&
                                oldState.Buttons.RightShoulder == ButtonState.Released)
            {
                //if (GamePad.GetState())
                //{

                //}
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X == 0f && GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X == 0f)
                {

                }
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X == 0f)
                {
                    projectile = new Projectile(pos, TextureManager.testTexture, new Vector2(GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.X, -GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.Y), angle, new Point(), new Point());
                    listProjectile.Add(projectile);
                }
                else
                {

                    projectile = new Projectile(pos, TextureManager.testTexture, new Vector2(GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.X, -GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.Y), angle, new Point(), new Point());
                    listProjectile.Add(projectile);
                }

                // the button has just been pressed
                // do something here
            }

            dPad_value = new Vector2(GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X, GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y);

            float rightTriggerValue = newState.Triggers.Right;

            rightTriggerTimer += gameTime.ElapsedGameTime.Milliseconds;


            if (rightTriggerTimer > 200)
            {
                if (rightTriggerValue == 1)
                {
                    if (!(GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X == 0f && GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X == 0f))
                    {

                        projectile = new Projectile(pos, TextureManager.testTexture, new Vector2(GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.X, -GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.Y), angle, new Point(), new Point());
                        listProjectile.Add(projectile);
                        rightTriggerTimer = 0;

                    }
                    if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X == 0f)
                    {
                        if (!(GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y == 0f))
                        {
                            projectile = new Projectile(pos, TextureManager.testTexture, new Vector2(GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.X, -GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.Y), angle, new Point(), new Point());
                            listProjectile.Add(projectile);
                            rightTriggerTimer = 0;

                        }

                    }
                    //else
                    //{
                    //    projectile = new Projectile(pos, TextureManager.testTexture, new Vector2(GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.X, -GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.Y), angle, new Point(), new Point());
                    //    listProjectile.Add(projectile);
                    //}
                    //else
                    //{

                    //    projectile = new Projectile(pos, TextureManager.testTexture, new Vector2(GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.X, -GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.Y), angle, new Point(), new Point());
                    //    listProjectile.Add(projectile);
                    //}
                }
            }


            for (int i = 0; i < listProjectile.Count; i++)
            {
                shootTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (shootTimer > 200)
                {
                    listProjectile.RemoveAt(i);
                    i--;
                    shootTimer = 0;
                }

            }

            //foreach (Projectile p in listProjectile)
            //{
            //    shootTimer += gameTime.ElapsedGameTime.Milliseconds;

            //    if (shootTimer > 200)
            //    {

            //        shootTimer = 0;
            //    }
            //}

            foreach (Projectile e in listProjectile)
            {
                e.Update(gameTime);
            }

            // At the end, we update old state to the state we grabbed at the start of this update.
            // This allows us to reuse it in the next update.
            oldState = newState;

            prevPos = pos;
            pos += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);

            PrevAngle = angle;

            angle = (float)Math.Atan2(GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.X, GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.Y);

            if (GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Right.Length() == 0)
            {
                angle = PrevAngle;
            }



            //DpadControl();

            //if (GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular).ThumbSticks.Left.Y == 0f)
            //{
            //    direction.Y = 0;
            //}
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height), null, Color.White, angle, new Vector2(tex.Width / 2, tex.Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(tex, hitBox, Color.Red);

            foreach (Projectile e in listProjectile)
            {
                e.Draw(spriteBatch);
            }
            spriteBatch.DrawString(TextureManager.font, "value: " + dPad_value +
                                                        "\npos: " + pos +
                                                        "\nshoot timer: " + shootTimer +
                                                        "\namount of proj: " + listProjectile.Count, new Vector2(200, 200), Color.Red);
        }
        private void DpadControl()
        {
            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
                direction.X = -1;

            else if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
                direction.X = 1;
            else if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Released && GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Released)
                direction.X = 0;

            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                direction.Y = -1;

            else if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                direction.Y = 1;

            else if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Released && GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Released)
                direction.Y = 0;
        }
    }
}
