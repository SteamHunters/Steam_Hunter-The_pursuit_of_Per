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
        bool teleport; 
        public Engineer(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, int playerIndex)
            : base(tex, pos, window, gps, hp, mana, speed, playerIndex)
        {

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
                teleport = true;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(teleport == true)
            {
                spriteBatch.Draw(TextureManager.teleportLocation, pos, Color.White);
            }
            base.Draw(spriteBatch);
        }
    }
}
