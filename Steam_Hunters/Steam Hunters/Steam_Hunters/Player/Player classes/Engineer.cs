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
        public Engineer(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps)
            : base(tex, pos, window, gps)
        {

        }

        //public override void Draw(SpriteBatch sb)
        //{
        //    sb.Draw(pos, hitBox, Color.Red);
        //}
    }
}
