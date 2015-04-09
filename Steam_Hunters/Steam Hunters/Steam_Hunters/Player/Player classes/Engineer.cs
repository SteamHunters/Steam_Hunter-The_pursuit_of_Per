using Microsoft.Xna.Framework;
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
        Vector2 distancevalue;

        public static bool createMissile;

        public Engineer(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, PlayerIndex playerIndex)
            : base(tex, pos, window, gps, hp, mana, speed, playerIndex)
        {
            projectileTimerLife = 700;
            teleportPos = pos;
        }

        public override void Update(GameTime gameTime)
        {
            EngineerTower turret = new EngineerTower(TextureManager.turretTexTop, pos, gps, 100);
           
            if(Xpress == true)
            {
                dispenser = new Dispenser(TextureManager.dispenserTex, new Vector2(pos.X, pos.Y), 100);
                rumble.Vibrate(0.15f, 0.5f);
                gps.dispensers.Add(dispenser);
            }
            if (Ypress == true)
            {
                if (gps.turrets.Count >= 1)
                    createMissile = true;
            }
            else
            {
                createMissile = false;
            }
            if(Apress == true)
            {
                gps.turrets.Add(turret);
                rumble.Vibrate(0.15f, 0.25f);
            }
            if(LTpress == true)
            {
                speed -= 0.4f;
                turretShooting = true;

                if(gps.turrets.Count >= 1)
                    rumble.Vibrate(0.0015f, 0.5f);

                if (speed <= 0)
                    speed = 0;
            }
            else
            {
                speed = 5;
                turretShooting = false;
            }
            if(Bpress == true)
            {
                teleportIsOn = true;
                teleportPos = pos;
            }
            if (teleportIsOn == true)
            {
                distancevalue = pos - teleportPos;
                if (Vector2.Distance(pos, teleportPos) <= 400)
                {
                    teleportPos.X += newState.ThumbSticks.Right.X * speed;
                    teleportPos.Y -= newState.ThumbSticks.Right.Y * speed;
                }
                else
                {
                    teleportPos.X = pos.X;
                    teleportPos.Y = pos.Y;
                }
                if (RBpress == true)
                {
                    pos.X = teleportPos.X + TextureManager.teleportLocation.Width/2;
                    pos.Y = teleportPos.Y + TextureManager.teleportLocation.Height / 2;
                    teleportIsOn = false;
                    rumble.Vibrate(0.15f, 0.75f);
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
