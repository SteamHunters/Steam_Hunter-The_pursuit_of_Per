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
                Dispenser dispenser = new Dispenser(TextureManager.dispenserTex, new Vector2(pos.X, pos.Y), 100);
                gps.dispensers.Add(dispenser);
            }
            if(Apress == true)
            {
                gps.turrets.Add(turret);
            }
            if(LTpress == true)
            {
                speed -= 0.4f;
                turretShooting = true;

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
                teleportPos.X += newState.ThumbSticks.Right.X * speed;
                teleportPos.Y -= newState.ThumbSticks.Right.Y * speed;

                if (RBpress == true)
                {
                    pos.X = teleportPos.X + TextureManager.teleportLocation.Width/2;
                    pos.Y = teleportPos.Y + TextureManager.teleportLocation.Height / 2;
                    teleportIsOn = false;
                    rumble.Vibrate(3, 0.75f);
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (teleportIsOn == true)
            {
                spriteBatch.Draw(TextureManager.teleportLocation, teleportPos, Color.White);
            }
            base.Draw(spriteBatch);
        }
    }
}
