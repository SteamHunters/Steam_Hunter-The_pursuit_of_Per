using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class Engineer : Player
    {
        public Engineer(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps, int playerIndex)
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
                spriteBatch.DrawString(TextureManager.font, "A", new Vector2(0, 20), Color.White);
            if (Bpress == true)
                spriteBatch.DrawString(TextureManager.font, "B", new Vector2(0, 20), Color.White);
            if (Xpress == true)
                spriteBatch.DrawString(TextureManager.font, "X", new Vector2(0, 20), Color.White);
            if (Ypress == true)
                spriteBatch.DrawString(TextureManager.font, "Y", new Vector2(0, 20), Color.White);

            base.Draw(spriteBatch);
        }
    }
}
