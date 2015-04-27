using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    static class MusicManager
    {
        // skriv detta om det är en sång/låt/melodi
        //public static Song "Namn på variablen" { get; private set; }

        #region sånger/låtar/melodier
        public static Song MenyMusic { get; private set; }
        public static Song Level1Music { get; private set; }
        #endregion

        public static void LoadContent(ContentManager Content)
        {
            // Vad man ska skriva                  Vart den finns i content mappen ex:
            //"Namn på variablen" = Content.Load<Song>(@"Texture/Tower/LazerTower");

            #region Laddar in sånger/låtar/melodier
            MenyMusic = Content.Load<Song>(@"Music/Sakura Sword");
            Level1Music = Content.Load<Song>(@"Music/SteampunkOrchestra");
            #endregion

        }


    }
}
