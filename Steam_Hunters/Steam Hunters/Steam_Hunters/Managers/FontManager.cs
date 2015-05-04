using Microsoft.Xna.Framework.Content;
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
        public static SpriteFont SteamFont { get; private set; }
        public static SpriteFont HUDFont { get; private set; }

        public static SpriteFont SelectFont { get; private set; }


        #endregion

        public static void LoadContent(ContentManager Content)
        {
            //Vad man ska skriva                           Vart fonten är i content
            //"Namn på variablen" = Content.Load<SpriteFont>(@"Fonts/HudFont");

            #region Fonts
            font = Content.Load<SpriteFont>(@"font");
            SteamFont = Content.Load<SpriteFont>(@"SteamFont");
            HUDFont = Content.Load<SpriteFont>(@"HUDFont");
            SelectFont = Content.Load<SpriteFont>(@"Font_35");
            #endregion

        }
    }
}
