﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    static class FontManager
    {
        //Vad man ska skriva
        //public static SpriteFont "Namn på variablen" { get; private set; }

        #region Fonts
        public static SpriteFont font { get; private set; }

        #endregion

        public static void LoadContent(ContentManager Content)
        {
            //Vad man ska skriva                           Vart fonten är i content
            //"Namn på variablen" = Content.Load<SpriteFont>(@"Fonts/HudFont");

            #region Fonts
            font = Content.Load<SpriteFont>(@"font");
            #endregion

        }
    }
}
