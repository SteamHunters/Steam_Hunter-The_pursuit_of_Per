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

        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
           
            

            base.Draw(spriteBatch);
        }

    }
}
