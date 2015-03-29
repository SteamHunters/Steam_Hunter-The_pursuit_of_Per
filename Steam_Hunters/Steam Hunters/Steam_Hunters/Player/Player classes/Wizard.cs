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
        public Wizard(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps, int playerIndex)
            : base(tex, pos, window, gps, playerIndex)
        {

        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Apress == true)
                spriteBatch.DrawString(TextureManager.font, "Hejsan", new Vector2(0, 40), Color.White);

            base.Draw(spriteBatch);
        }

    }
}
