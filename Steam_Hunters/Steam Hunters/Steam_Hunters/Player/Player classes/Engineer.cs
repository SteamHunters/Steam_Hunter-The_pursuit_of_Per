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
                speed = 0;
                turretShooting = true;
            }
            else
            {
                speed = 5;
                turretShooting = false;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            #region Test button komand
            if (Apress == true)
                spriteBatch.DrawString(FontManager.font, "A", new Vector2(0, 20), Color.White);
            if (Bpress == true)
                spriteBatch.DrawString(FontManager.font, "B", new Vector2(0, 20), Color.White);
            if (Xpress == true)
                spriteBatch.DrawString(FontManager.font, "X", new Vector2(0, 20), Color.White);
            if (Ypress == true)
                spriteBatch.DrawString(FontManager.font, "Y", new Vector2(0, 20), Color.White);

            if (RTpress == true)
                spriteBatch.DrawString(FontManager.font, "RT", new Vector2(0, 20), Color.White);
            if (RBpress == true)
                spriteBatch.DrawString(FontManager.font, "RB", new Vector2(0, 20), Color.White);
            if (LTpress == true)
                spriteBatch.DrawString(FontManager.font, "LT", new Vector2(0, 20), Color.White);
            if (LBpress == true)
                spriteBatch.DrawString(FontManager.font, "LB", new Vector2(0, 20), Color.White);

            if (Duppress == true)
                spriteBatch.DrawString(FontManager.font, "up", new Vector2(0, 20), Color.White);
            if (Ddownpress == true)
                spriteBatch.DrawString(FontManager.font, "down", new Vector2(0, 20), Color.White);
            if (Drightpress == true)
                spriteBatch.DrawString(FontManager.font, "right", new Vector2(0, 20), Color.White);
            if (Dlefthpress == true)
                spriteBatch.DrawString(FontManager.font, "lefth", new Vector2(0, 20), Color.White);
            #endregion

            base.Draw(spriteBatch);
        }
    }
}
