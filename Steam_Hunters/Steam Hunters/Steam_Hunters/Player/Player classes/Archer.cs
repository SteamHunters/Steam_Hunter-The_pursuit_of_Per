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
        private List<Projectile> powerShootList = new List<Projectile>();
        protected Point frameSizeReload = new Point(90, 90);
        protected Point currentFrameReload = new Point(0, 0);
        protected Point sheetSizeReload = new Point(8, 1);
        float timerSinceLastFrameReload, timerSinceLastFrameRain;
        protected int milliSecondsPerFrameReload = 200;
        int sheetNbr;
        float spinPlus, spinMinus;

        protected Point frameSizeRain = new Point(60, 60);
        protected Point currentFrameRain = new Point(0, 0);
        protected Point sheetSizeRain = new Point(8, 1);

        //Color col = new Color(100,100,100,100);
        Vector2 rainAttack;
        Vector2 savePos;
        bool isPosSaved;

        int triggerPress;

        Projectile powerShoot;

        private ParticleEngine particleEnginePowerShoot;


        public Archer(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, int damage, PlayerIndex playerIndex)
            : base(tex, pos, window, gps, hp, mana, speed, damage, playerIndex)
        {
            projTex = TextureManager.arrowBasic;
            this.particleEnginePowerShoot = new ParticleEngine(TextureManager.steamTextures, pos, Color.Yellow);

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
            offsetBullet = new Vector2(12, 30);
            //color = new Color(100,100,100,100);
            frameSize = new Point(45, 45);
        }

        public override void Update(GameTime gameTime)
        {
            particleEnginePowerShoot.Update();

            //color = new Color(100, 100, 100, 100);
            if (LTpress == true)
            {
                isTransparent = true;
                //color = new Color(100, 100, 100, 100);
            }

            if (Apress == true)
            {
                //projTex = TextureManager.powerShootArc;
                Vector2 powerShootAngle = new Vector2(prevThumbStickRightValue.X, prevThumbStickRightValue.Y);
                powerShoot = new Projectile(new Vector2(pos.X + 10, pos.Y), TextureManager.arrowPowerShoot, powerShootAngle, angle, new Vector2(0, 0), 0.5f, 80, new Point(20, 35), new Point(3, 1), 100, true);
                powerShootList.Add(powerShoot);
                particleEnginePowerShoot.EmitterLocation = new Vector2(pos.X, pos.Y);
                particleEnginePowerShoot.total = 5;
                //projectile = new Projectile(new Vector2(pos.X + 10, pos.Y), projTex, insertDirection, angle, offsetBullet, 0.4f, 80, new Point(), new Point(), 0, false);
            }
            else
                particleEnginePowerShoot.total = 0;

            //if (circleSize <= 300)
            //{
            //    circleSize--;
            //}
            //CreateCircle(500);



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

            color = Color.White;

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

            foreach (Projectile p in powerShootList)
            {
                p.Update(gameTime);
            }

            base.Update(gameTime);
            ShootRightThumbStick(newState, gameTime);


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(tex, pos, new Rectangle(0 ,0 , 20, 20), Color.Blue);
            particleEnginePowerShoot.Draw(spriteBatch);

            base.Draw(spriteBatch);
            //spriteBatch.Draw(tex, new Vector2(pos.X + 30, pos.Y + 30), new Rectangle(0, 0, 20, 20), col, angle, new Vector2(origin.X + 20, origin.Y + 20), 1, EntityFx, 0);
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
            spriteBatch.DrawString(FontManager.font, "tid: " + timerSinceLastFrameReload + "  " + milliSecondsPerFrameReload +
                                                     "\nbool: " + isTransparent +
                                                     "\ncolor: " + color +
                                                     "\npos for rain: " + rainAttack +
                                                     "\namount of powershoot: " + powerShootList.Count, new Vector2(pos.X + 100, pos.Y + 100), Color.Blue);



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

            foreach (Projectile p in powerShootList)
            {
                p.Draw(spriteBatch);
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
