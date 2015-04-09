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
        public static Texture2D WarriorPic { get; private set; }
        #endregion

        #region Archer Textur
        public static Texture2D ArcherPic { get; private set; }
        #endregion

        #region Wizard Textur
        public static Texture2D WizardPic { get; private set; }
        #endregion

        #region Enginer Textur
        public static Texture2D EngineerPic { get; private set; }
        // Turrent
        public static Texture2D dispenserTex { get; private set; }
        public static Texture2D turretTexBot { get; private set; }
        public static Texture2D turretTexTop { get; private set; }
        public static Texture2D teleportLocation { get; private set; }
        public static Texture2D missile { get; private set; }
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
        public static Texture2D arrowBasic { get; private set; }
        public static Texture2D turretBullet { get; private set; }
        #endregion

        #region HUD Texturer

        #endregion

        #region Bakgrunds Texturer

        public static Texture2D startBackground { get; private set; }
        public static Texture2D instructionScreen { get; private set; }
        public static Texture2D chooseSingleplayer { get; private set; }
        public static Texture2D chooseMultiplayer { get; private set; }

        #endregion

        #region Knapp/Buttons Texturer

        public static Texture2D singleplayerButton { get; private set; }
        public static Texture2D multiplayerButton { get; private set; }
        public static Texture2D instructionButton { get; private set; }
        public static Texture2D exitButton { get; private set; }

        #endregion

        #region M.m. Texturer

        public static List<Texture2D> steamTextures = new List<Texture2D>();
        public static Texture2D steam1 { get; private set; }
        public static Texture2D steam2 { get; private set; }
        public static Texture2D steam3 { get; private set; }
        #endregion

        #endregion

        // Ta bort sen
        public static Texture2D testTextureEngineer { get; private set; }
        public static Texture2D testTextureArcher { get; private set; }
        public static Texture2D reload { get; private set; }


        public static Texture2D map { get; private set; }
        //        



        public static void LoadContent(ContentManager Content)
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
            WarriorPic = Content.Load<Texture2D>(@"Texturer/Karakter/Warrior/WarriorPic");
            #endregion

            #region Archer Textur
            ArcherPic = Content.Load<Texture2D>(@"Texturer/Karakter/Archer/Archer Pic");
            #endregion

            #region Wizard Textur
            WizardPic = Content.Load<Texture2D>(@"Texturer/Karakter/Wizard/Wizard Pic");
            #endregion

            #region Enginer Textur
            EngineerPic = Content.Load<Texture2D>(@"Texturer/Karakter/Engineer/Engineer Pic");
            dispenserTex = Content.Load<Texture2D>(@"Dispenser");
            turretTexBot = Content.Load<Texture2D>(@"turret bot");
            turretTexTop = Content.Load<Texture2D>(@"turret top");
            teleportLocation = Content.Load<Texture2D>(@"teleportLocation");
            missile = Content.Load<Texture2D>(@"missile");

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

            arrowBasic = Content.Load<Texture2D>(@"Arrow");
            turretBullet = Content.Load<Texture2D>(@"turretBullet");

            #endregion

            #region HUD Texturer

            #endregion

            #region Bakgrunds Texturer
            instructionScreen = Content.Load<Texture2D>(@"InstructionScreen");
            chooseSingleplayer = Content.Load<Texture2D>(@"PickACharacterSingle");
            chooseMultiplayer = Content.Load<Texture2D>(@"PickACharacterMulti");
            #endregion

            #region Knapp/Buttons Texturer
            singleplayerButton = Content.Load<Texture2D>(@"Singleplayer");
            multiplayerButton = Content.Load<Texture2D>(@"Multiplayer");
            exitButton = Content.Load<Texture2D>(@"Exit");
            instructionButton = Content.Load<Texture2D>(@"Instructions");
            #endregion

            #region M.m. Texturer


            #endregion

            #endregion

            // Ta bort sen
            testTextureEngineer = Content.Load<Texture2D>(@"engineer animation walking");
            testTextureArcher = Content.Load<Texture2D>(@"ArcherTest");
            reload = Content.Load<Texture2D>(@"reload");

            steamTextures.Add(steam1 = Content.Load<Texture2D>(@"steamSmoke1"));
            steamTextures.Add(steam2 = Content.Load<Texture2D>(@"steamSmoke2"));
            steamTextures.Add(steam3 = Content.Load<Texture2D>(@"steamSmoke3"));

            // 
            // Ska flyttas

            startBackground = Content.Load<Texture2D>(@"StartScreenTest");





        }


    }
}
