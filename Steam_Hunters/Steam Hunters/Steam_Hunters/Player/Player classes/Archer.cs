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
        double timerTransparent;
        bool isTransparent;
        int lifeTransparent;
        bool isReloading;
        //Texture2D circle;
        //int circleSize;
        protected Point frameSizeReload = new Point(90, 90);
        protected Point currentFrameReload = new Point(0, 0);
        protected Point sheetSizeReload = new Point(8, 1);
        float timerSinceLastFrameReload;
        protected int milliSecondsPerFrameReload = 200;
        int sheetNbr;
        float spin1, spin2, spin3, spin4, spin5, spin6, spin7;

        Vector2 rainAttack;
        Vector2 savePos;
        bool isPosSaved;

        public Archer(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, PlayerIndex playerIndex)
            : base(tex, pos, window, gps, hp, mana, speed, playerIndex)
        {

            //Kod för att leapa, inte riktigt färdig! Men kan byggas på för att få rätt resultat
            //if (prevThumbStickRightValue.X != 0 || prevThumbStickRightValue.Y != 0)
            //{
            //    jump += prevThumbStickRightValue;
            //    jump.Normalize();
            //    pos += jump * 100;
            //}
            //circleSize = 200;
            isTransparent = false;
            lifeTransparent = 2000;
            isReloading = false;
            isPosSaved = true;
            //circle = CreateCircle(circleSize);
            projectileTimerLife = 200;
            //                                                      name, int, str, agil, vit, luck, hp, mp, lvl 
            statusWindow = new StatusWindow(TextureManager.turretBullet, pos, "hej", 0, 0, 0, 0, 0, hp, mana, 1, playerIndex);
        }

        public override void Update(GameTime gameTime)
        {
            if (LBpress == true)
            {
                isTransparent = true;
            }

            //if (circleSize <= 300)
            //{
            //    circleSize--;
            //}
            //CreateCircle(500);

            

            if (newState.Buttons.X == ButtonState.Pressed)
            {
                if (isPosSaved)
                {
                    savePos = pos;
                    //isPosSaved = false;
                }

                rainAttack.X += newState.ThumbSticks.Right.X * speed;
                rainAttack.Y -= newState.ThumbSticks.Right.Y * speed;

                spin1 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                spin2 -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                spin3 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                spin4 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                spin5 -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                spin6 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                spin7 -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                



                //speed = 0;
                //pos.X += newState.ThumbSticks.Left.X * speed;
                //pos.Y -= newState.ThumbSticks.Left.Y * speed;
            }



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


            statusWindow.SetPos = pos;
            statusWindow.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            statusWindow.Draw(spriteBatch);
            //spriteBatch.Draw(tex, pos, new Rectangle(0 ,0 , 20, 20), Color.Blue);

            base.Draw(spriteBatch);
            //spriteBatch.Draw(tex, new Vector2(pos.X, pos.Y), new Rectangle(0, 0, 20, 20), Color.Blue, angle, new Vector2(origin.X + 20, origin.Y + 20), 1, EntityFx, 0);
            //spriteBatch.Draw(circle, pos, new Rectangle(0, 0, circleSize * 2, circleSize * 2), Color.Red, 0, new Vector2(origin.X, origin.Y ), 1, SpriteEffects.None, 0);

            if (isReloading == true)
            {
                //spriteBatch.Draw(TextureManager.reload, new Vector2(pos.X, pos.Y), new Rectangle(0, 0, TextureManager.reload.Width, TextureManager.reload.Height), Color.Blue, angle, new Vector2(origin.X + 20, origin.Y + 20), 1, EntityFx, 0);
                spriteBatch.Draw(TextureManager.reload, pos, new Rectangle(currentFrameReload.X * frameSizeReload.X, currentFrameReload.Y * frameSizeReload.Y, frameSizeReload.X, frameSizeReload.Y), Color.White, 0, new Vector2(frameSizeReload.X / 2, frameSizeReload.Y / 2), 1, SpriteEffects.None, 0);
                isShooting = false;
            }
            else
            {
                isShooting = true;
            }
            spriteBatch.DrawString(FontManager.font, "tid" + timerSinceLastFrameReload + "  " + milliSecondsPerFrameReload, new Vector2(pos.X + 100, pos.Y + 100), Color.Blue);



            if (newState.Buttons.X == ButtonState.Pressed)
            {
                spriteBatch.Draw(TextureManager.circles[0], rainAttack, new Rectangle(0, 0, 300, 300), Color.White, spin1, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[1], rainAttack, new Rectangle(0, 0, 300, 300), Color.White, spin2, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[2], rainAttack, new Rectangle(0, 0, 300, 300), Color.White, spin3, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[3], rainAttack, new Rectangle(0, 0, 300, 300), Color.White, spin4, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[4], rainAttack, new Rectangle(0, 0, 300, 300), Color.White, spin5, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[5], rainAttack, new Rectangle(0, 0, 300, 300), Color.White, spin6, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(TextureManager.circles[6], rainAttack, new Rectangle(0, 0, 300, 300), Color.White, spin7, new Vector2(TextureManager.circles[0].Width / 2, TextureManager.circles[0].Height / 2), 1, SpriteEffects.None, 0);
            }

        }

        //public Texture2D CreateCircle(int radius)
        //{
        //    int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
        //    Texture2D texture = new Texture2D(graphics, outerRadius, outerRadius);

        //    Color[] data = new Color[outerRadius * outerRadius];

        //    // Colour the entire texture transparent first.
        //    for (int i = 0; i < data.Length; i++)
        //        data[i] = Color.Transparent;

        //    // Work out the minimum step necessary using trigonometry + sine approximation.
        //    double angleStep = 1f / radius;

        //    for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
        //    {
        //        // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
        //        int x = (int)Math.Round(radius + radius * Math.Cos(angle));
        //        int y = (int)Math.Round(radius + radius * Math.Sin(angle));

        //        data[y * outerRadius + x + 1] = Color.White;
        //    }

        //    texture.SetData(data);
        //    return texture;
        //}

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(tex, hitBox, Color.Red);
        //    if (isTransparent == true)
        //    {
        //        spriteBatch.Draw(tex, pos, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), new Color(100, 100, 100, 100), angle, new Vector2(frameSize.X / 2, frameSize.Y / 2), 1, EntityFx, 0);

        //    }
        //    else
        //    {
        //        spriteBatch.Draw(tex, pos, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), color, angle, new Vector2(frameSize.X / 2, frameSize.Y / 2), 1, EntityFx, 0);
        //    }



        //    base.Draw(spriteBatch);
        //}


    }
}
