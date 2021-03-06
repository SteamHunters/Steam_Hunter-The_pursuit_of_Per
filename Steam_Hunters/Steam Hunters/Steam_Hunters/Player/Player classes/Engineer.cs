﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Steam_Hunters
{
    class Engineer : Player
    {
        public bool turretShooting;
        bool teleportIsOn, teleportToLocation;
        public Vector2 teleportPos;
        Dispenser dispenser;
        EngineerTower turret;
        Vector2 distancevalue;
        public static bool createMissile;
        //float oldSpeed;

        public Engineer(Texture2D tex, Texture2D HUDPic, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, int damage, PlayerIndex playerIndex)
            : base(tex, HUDPic, pos, window, gps, hp, mana, speed, damage, playerIndex)
        {
            frameSize = new Point(45, 45);
            projectileTimerLife = 700;
            teleportPos = pos;
            projTex = TextureManager.bulletEng;
            offsetBullet = new Vector2(-7, 20);
            //oldSpeed = speed;
            //                                                      name, int, str, agil, vit, luck, hp, mp, lvl 
            statusWindow = new StatusWindow(TextureManager.turretBullet, pos, "Sebastian", 2, 2, 2, 4, 0, hp, mana, 1, playerIndex);
        }

        public override void Update(GameTime gameTime)
        {
            if (paused == false)
            {
                ShootRightThumbStick(newState, gameTime);

                turret = new EngineerTower(TextureManager.turretTexTop, new Vector2(pos.X-15,pos.Y-70), gps, 100);
                particleEngineSteam.Update();
                statusWindow.SetPos = pos;
                statusWindow.Update(gameTime);

                if (buying == false && statusWindow.active == false && ghostMode == false)
                {
                    #region Dispenser
                    if (Xpress == true)
                    {
                        if (statusWindow.mana >= 50)
                        {
                            dispenser = new Dispenser(TextureManager.dispenserTex, new Vector2(pos.X-15, pos.Y+50), 100);
                            rumble.Vibrate(0.15f, 0.5f);
                            gps.dispensers.Add(dispenser);
                            statusWindow.mana -= 50;
                        }
                    }
                    #endregion

                    #region Missile
                    if (Ypress == true)
                    {
                        if (statusWindow.mana >= 100)
                        {
                            if (gps.turrets.Count >= 1)
                            {
                                createMissile = true;
                                rumble.Vibrate(0.2f, 1f);
                                statusWindow.mana -= 100;
                            }
                        }
                    }
                    else
                    {
                        createMissile = false;
                    }
                    #endregion

                    #region Turret
                    if (Apress == true)
                    {
                        if (statusWindow.mana >= 10)
                        {
                            gps.turrets.Add(turret);
                            rumble.Vibrate(0.15f, 0.25f);
                            statusWindow.mana -= 10;
                        }

                    }
                    if (LTpress == true)
                    {
                        speed -= 0.4f;
                        turretShooting = true;

                        if (gps.turrets.Count >= 1)
                        {
                            rumble.Vibrate(0.0015f, 0.5f);

                        }

                        if (speed <= 0)
                            speed = 0;
                    }
                    else
                    {
                        speed = oldSpeed;
                        turretShooting = false;
                    }
                    #endregion

                    #region Teleport
                    if (Bpress == true)
                    {
                        teleportIsOn = true;
                        teleportPos = pos;
                    }
                    if (teleportIsOn == true)
                    {
                        distancevalue = pos - teleportPos;
                        if (Vector2.Distance(pos, teleportPos) <= 400)
                        {
                            teleportPos.X += newState.ThumbSticks.Right.X * speed * 2f;
                            teleportPos.Y -= newState.ThumbSticks.Right.Y * speed * 2f;
                        }
                        else
                        {
                            teleportPos.X = pos.X;
                            teleportPos.Y = pos.Y;
                        }
                        if (RBpress == true)
                        {
                            pos.X = teleportPos.X + TextureManager.teleportLocation.Width / 2;
                            pos.Y = teleportPos.Y + TextureManager.teleportLocation.Height / 2;
                            teleportIsOn = false;
                            rumble.Vibrate(0.15f, 0.75f);
                            particleEngineSteam.EmitterLocation = new Vector2(pos.X, pos.Y);
                            particleEngineSteam.total = 150;
                        }
                    }
                    else
                    {
                        particleEngineSteam.total = 0;
                    }
                    #endregion


                }

                if (statusWindow.hp < statusWindow.maxHp)
                {
                    if (ghostMode == false)
                        statusWindow.hp += 2 * ((1 + (statusWindow.vitality / 20)) * time / 2);
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (teleportIsOn == true)
            {
                spriteBatch.Draw(TextureManager.teleportLocation, teleportPos, Color.Lerp(Color.White,Color.Red,distancevalue.Length()/400));
            }
            particleEngineSteam.Draw(spriteBatch);
            base.Draw(spriteBatch);

            
        }
    }
}
