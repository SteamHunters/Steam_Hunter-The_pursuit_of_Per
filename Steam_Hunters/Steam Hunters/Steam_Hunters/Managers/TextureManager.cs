using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    static class TextureManager
    {
        // Hur man ska skriva
        //public static Texture2D "Namn på variablen" { get; private set; }

        #region Alla Texturer

        #region Tiles Texture

        #region Byggnad Texturer
        #endregion

        #region Träd Texturer
        #endregion

        #region Växt Texturer
        #endregion

        #region Mark Texturer
        #endregion

        #endregion

        #region Karaktär Texturer

        #region Warrior Textur
        #endregion

        #region Archer Textur
        #endregion

        #region Wizard Textur
        #endregion

        #region Enginer Textur
        #endregion

        #endregion

        #region Potion Texturer
        #endregion

        #region Vapen Texturer
        #endregion

        #region Profil Texturer
        #endregion

        #region Fiende Texturer
        #endregion

        #region Projektil Texturer
        #endregion

        #region HUD Texturer
        #endregion

        #region Bakgrunds Texturer
        #endregion

        #region Knapp/Buttons Texturer
        #endregion

        #region M.m. Texturer
        #endregion

        #endregion

        public static Texture2D testTexture { get; private set; }
        public static Texture2D startBackground { get; private set; }
        public static Texture2D map { get; private set; }
        public static SpriteFont font { get; private set; }
        public static Texture2D arrow { get; private set; }


        public static void LoadContent(Game1 game)
        {
            // Vad man ska skriva                        Vart den finns i content mappen ex:
            //"Namn på variablen" = Content.Load<Texture2D>(@"Texture/Tower/LazerTower");

            #region In laddning av texturer

            #region Tiles Texture

            #region Byggnad Texturer
            #endregion

            #region Träd Texturer
            #endregion

            #region Växt Texturer
            #endregion

            #region Mark Texturer
            #endregion

            #endregion

            #region Karaktär Texturer

            #region Warrior Textur
            #endregion

            #region Archer Textur
            #endregion

            #region Wizard Textur
            #endregion

            #region Enginer Textur
            #endregion

            #endregion

            #region Potion Texturer
            #endregion

            #region Vapen Texturer
            #endregion

            #region Profil Texturer
            #endregion

            #region Fiende Texturer
            #endregion

            #region Projektil Texturer
            #endregion

            #region HUD Texturer
            #endregion

            #region Bakgrunds Texturer
            #endregion

            #region Knapp/Buttons Texturer
            #endregion

            #region M.m. Texturer
            #endregion

            #endregion

            testTexture = game.Content.Load<Texture2D>("one frame wizard");
            startBackground = game.Content.Load<Texture2D>("StartScreen AlphaPix");
            map = game.Content.Load<Texture2D>("map");
            font = game.Content.Load<SpriteFont>("font");
            arrow = game.Content.Load<Texture2D>("Very big arrow");


        }


    }
}
