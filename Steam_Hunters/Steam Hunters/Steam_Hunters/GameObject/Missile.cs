using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Steam_Hunters
{
    abstract class Missile:GameObject
    {

        public Missile(Texture2D tex, Vector2 pos)
            : base(tex, pos)
        {

        }
    }
}
