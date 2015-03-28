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
    }
}
