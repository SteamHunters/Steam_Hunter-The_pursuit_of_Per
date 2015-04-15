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
        public static Texture2D WizardAnimation { get; private set; }
        public static Texture2D WizardPic { get; private set; }
        public static Texture2D magicShield { get; private set; }
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

        #region NPC

        public static Texture2D NPCTexture { get; private set; }

        #endregion

        #endregion

        #region Potion Texturer

        public static Texture2D healthPotionSHOPTexture { get; private set; }
        public static Texture2D manaPotionSHOPTexture { get; private set; }
        public static Texture2D buffPotionSHOPTexture { get; private set; }
        public static Texture2D ressPotionSHOPTexture { get; private set; }
        public static Texture2D healthPotionHUDTexture { get; private set; }
        public static Texture2D manaPotionHUDTexture { get; private set; }
        public static Texture2D buffPotionHUDTexture { get; private set; }
        public static Texture2D ressPotionHUDTexture { get; private set; }
        #endregion

        #region Vapen Texturer
        #endregion

        #region Profil Texturer
        #endregion

        #region Fiende Texturer
        #endregion

        #region Projektil Texturer
        public static Texture2D fireBall { get; private set; }
        public static Texture2D waterBall { get; private set; }
        public static Texture2D arrowBasic { get; private set; }
        public static Texture2D turretBullet { get; private set; }
        public static Texture2D BoulderSheetTexture { get; private set; }
        public static Texture2D bulletWiz { get; private set; }
        public static Texture2D bulletEng { get; private set; }
        public static Texture2D rainTex { get; private set; }
        #endregion

        #region HUD Texturer

        #endregion

        #region Bakgrunds Texturer
        public static Texture2D StatusWindowTexture { get; private set; }
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

        public static Texture2D hpTexture { get; private set; }
        public static Texture2D manaTexture { get; private set; }
        #endregion

        #endregion

        // Ta bort sen
        public static Texture2D testTextureEngineer { get; private set; }
        public static Texture2D testTextureArcher { get; private set; }
        public static Texture2D reload { get; private set; }
        //public static Texture2D wizard { get; private set; }
        public static Texture2D warriorAnimation { get; private set; }

        //public static Texture2D [] circle { get; private set; }
        public static List<Texture2D> circles = new List<Texture2D>();
        public static Texture2D circle1 { get; private set; }
        public static Texture2D circle2 { get; private set; }
        public static Texture2D circle3 { get; private set; }
        public static Texture2D circle4 { get; private set; }
        public static Texture2D circle5 { get; private set; }
        public static Texture2D circle6 { get; private set; }
        public static Texture2D circle7 { get; private set; }

        public static Texture2D MonsterTest { get; private set; }

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
            WizardAnimation = Content.Load<Texture2D>(@"WizardAnimation");
            WarriorPic = Content.Load<Texture2D>(@"Texturer/Karakter/Warrior/WarriorPic");
            #endregion

            #region Archer Textur
            ArcherPic = Content.Load<Texture2D>(@"Texturer/Karakter/Archer/Archer Pic");
            #endregion

            #region Wizard Textur
            WizardPic = Content.Load<Texture2D>(@"Texturer/Karakter/Wizard/Wizard Pic");
            magicShield = Content.Load<Texture2D>(@"Magic shield");
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

            healthPotionSHOPTexture = Content.Load<Texture2D>(@"HealthPotionSHOP");
            manaPotionSHOPTexture = Content.Load<Texture2D>(@"ManaPotionSHOP");
            buffPotionSHOPTexture = Content.Load<Texture2D>(@"BuffPotionSHOP");
            ressPotionSHOPTexture = Content.Load<Texture2D>(@"RessPotionSHOP");

            healthPotionHUDTexture = Content.Load<Texture2D>(@"HealthPotionHUD");
            manaPotionHUDTexture = Content.Load<Texture2D>(@"ManaPotionHUD");
            buffPotionHUDTexture = Content.Load<Texture2D>(@"BuffPotionHUD");
            ressPotionHUDTexture = Content.Load<Texture2D>(@"RessPotionHUD");

            #endregion

            #region Vapen Texturer
            #endregion

            #region Profil Texturer
            #endregion

            #region Fiende Texturer
            #endregion

            #region Projektil Texturer
            fireBall = Content.Load<Texture2D>(@"Mage FireBall");
            waterBall = Content.Load<Texture2D>(@"Mage WaterBall");
            arrowBasic = Content.Load<Texture2D>(@"Arrow");
            turretBullet = Content.Load<Texture2D>(@"turretBullet");
            BoulderSheetTexture = Content.Load<Texture2D>(@"StoneAnimation");
            bulletWiz = Content.Load<Texture2D>(@"magic bullet");
            bulletEng = Content.Load<Texture2D>(@"bulletEng");
            rainTex = Content.Load<Texture2D>(@"arrowRain");
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
            //wizard = Content.Load<Texture2D>(@"Wizard done");
            warriorAnimation = Content.Load<Texture2D>(@"WarriorWalk");
            testTextureEngineer = Content.Load<Texture2D>(@"engineer animation walking");
            testTextureArcher = Content.Load<Texture2D>(@"ArcherTest");
            reload = Content.Load<Texture2D>(@"reload");
            MonsterTest = Content.Load<Texture2D>(@"Texturer/Enimes/Mobs 1/Melee mob");
            circles.Add(circle1 = Content.Load<Texture2D>(@"circle/circle1"));
            circles.Add(circle2 = Content.Load<Texture2D>(@"circle/circle2"));
            circles.Add(circle3 = Content.Load<Texture2D>(@"circle/circle3"));
            circles.Add(circle4 = Content.Load<Texture2D>(@"circle/circle4"));
            circles.Add(circle5 = Content.Load<Texture2D>(@"circle/circle5"));
            circles.Add(circle6 = Content.Load<Texture2D>(@"circle/circle6"));
            circles.Add(circle7 = Content.Load<Texture2D>(@"circle/circle7"));

            //circles.Add(Content.Load<Texture2D>(@"circle2"));
            //circles.Add(Content.Load<Texture2D>(@"circle3"));
            //circles.Add(Content.Load<Texture2D>(@"circle4"));
            //circles.Add(Content.Load<Texture2D>(@"circle5"));
            //circles.Add(Content.Load<Texture2D>(@"circle6"));
            //circles.Add(Content.Load<Texture2D>(@"circle7"));



            steamTextures.Add(steam1 = Content.Load<Texture2D>(@"steamSmoke1"));
            steamTextures.Add(steam2 = Content.Load<Texture2D>(@"steamSmoke2"));
            steamTextures.Add(steam3 = Content.Load<Texture2D>(@"steamSmoke3"));

            // 
            // Ska flyttas

            startBackground = Content.Load<Texture2D>(@"StartScreenTest");
            StatusWindowTexture = Content.Load<Texture2D>(@"StatusWindow");
            hpTexture = Content.Load<Texture2D>(@"HpPixel");
            manaTexture = Content.Load<Texture2D>(@"ManaPixel");
            NPCTexture = Content.Load<Texture2D>(@"NPC");



        }


    }
}
