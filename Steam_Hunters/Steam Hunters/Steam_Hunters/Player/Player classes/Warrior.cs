using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class Warrior : Player
    {
        private Point sheetSizeAttack = new Point(4, 2), currentFrameAttack = new Point(0, 0), frameSizeAttack = new Point(50, 50);
        private Point sheetSizeShield = new Point(4, 2), currentFrameShield = new Point(0, 0), frameSizeShield = new Point(50, 50);
        bool isAttacking, isShielding;
        float timerAttack, timerShield;

        public Warrior(Texture2D tex, Texture2D HUDPic, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, int damage, PlayerIndex playerIndex)
            : base(tex, HUDPic, pos, window, gps, hp, mana, speed, damage, playerIndex)
        {
            //                                                      name, int, str, agil, vit, luck, hp, mp, lvl 
            statusWindow = new StatusWindow(TextureManager.turretBullet, pos, "hej", 0, 0, 0, 0, 0, hp, mana, 1, playerIndex);
            frameSize = new Point(50, 50);
            isAttacking = false;
            isShielding = true;
        }


        public override void Update(GameTime gameTime)
        {

            statusWindow.SetPos = pos;
            statusWindow.Update(gameTime);

            if (statusWindow.hp < statusWindow.maxHp)
            {
                if (ghostMode == false)
                    statusWindow.hp += 5 * ((1 + (statusWindow.vitality / 20)) * time / 2);
            }

            //Dina knapptryckningar in här
            if (paused == false)
            {
                if (RTpress == true)
                    isAttacking = true;

                if (LTpress == true)
                    isShielding = true;
            }

            if (isAttacking)
            {
                //tex = TextureManager.warriorAttack;
                timerAttack += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timerAttack > 50)
                {
                    timerAttack -= 50;
                    ++currentFrameAttack.X;
                    if (currentFrameAttack.X >= sheetSizeAttack.X)
                    {

                        currentFrameAttack.X = 0;
                        ++currentFrameAttack.Y;
                        if (currentFrameAttack.Y >= sheetSizeAttack.Y)
                        {
                            currentFrameAttack.Y = 0;
                            //tex = TextureManager.warriorAnimation;
                            isAttacking = false;
                        }
                    }
                }
            }

            if (isShielding)
            {
                //tex = TextureManager.warriorAttack;
                timerShield += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timerShield > 50)
                {
                    timerShield -= 50;
                    ++currentFrameShield.X;
                    if (currentFrameShield.X >= sheetSizeShield.X)
                    {

                        currentFrameShield.X = 0;
                        ++currentFrameShield.Y;
                        if (currentFrameShield.Y >= sheetSizeShield.Y)
                        {
                            currentFrameShield.Y = 0;
                            //tex = TextureManager.warriorAnimation;
                            isShielding = false;
                        }
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isAttacking)
            {
                spriteBatch.Draw(TextureManager.warriorAttack, new Vector2(pos.X, pos.Y), new Rectangle(currentFrameAttack.X * frameSizeAttack.X, currentFrameAttack.Y * frameSizeAttack.Y, frameSizeAttack.X, frameSizeAttack.Y), color, angle, new Vector2(frameSizeAttack.X / 2, frameSizeAttack.Y / 2 + 8), 1, EntityFx, 0);
            }
            else if (isShielding)
            {
                spriteBatch.Draw(TextureManager.warriorShield, new Vector2(pos.X, pos.Y), new Rectangle(currentFrameShield.X * frameSizeShield.X, currentFrameShield.Y * frameSizeShield.Y, frameSizeShield.X, frameSizeShield.Y), color, angle, new Vector2(frameSizeShield.X / 2, frameSizeShield.Y / 2 + 6), 1, EntityFx, 0);
            }
            else
            {
                spriteBatch.Draw(tex, pos, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), color, angle, new Vector2(frameSize.X / 2, frameSize.Y / 2), 1, EntityFx, 0);

            }

            spriteBatch.DrawString(FontManager.font, "current: " + currentFrameAttack +
                                                     "\nframe size: " + currentFrameAttack +
                                                     "\nsheet size: " + sheetSizeAttack, new Vector2(pos.X + 100, pos.Y + 100), Color.Blue);


            //base.Draw(spriteBatch);
        }
    }
}
