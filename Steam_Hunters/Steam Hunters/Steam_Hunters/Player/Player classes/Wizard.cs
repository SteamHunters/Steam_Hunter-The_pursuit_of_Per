using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class Wizard : Player
    {
        public Wizard(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, PlayerIndex playerIndex)
            : base(tex, pos, window, gps, hp, mana, speed, playerIndex)
        {
            //                                                      name, int, str, agil, vit, luck, hp, mp, lvl 
            statusWindow = new StatusWindow(TextureManager.turretBullet, pos, "hej", 0, 0, 0, 0, 0, hp, mana, 1, playerIndex);
        }


        public override void Update(GameTime gameTime)
        {
            statusWindow.SetPos = pos;
            statusWindow.Update(gameTime);

            #region Attack A
            if (Apress == true)
            {

            }
            #endregion

            #region Attack X
            if (Xpress == true)
            {

            }
            #endregion

            #region Attack B
            if (Bpress == true)
            {

            }
            #endregion

            #region Attack Y
            if (Ypress == true)
            {

            }
            #endregion

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            statusWindow.Draw(spriteBatch);
            

            base.Draw(spriteBatch);
        }

    }
}
