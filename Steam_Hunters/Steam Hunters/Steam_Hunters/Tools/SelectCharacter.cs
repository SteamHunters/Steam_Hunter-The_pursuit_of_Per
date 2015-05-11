using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class SelectCharacter
    {
        private enum Character
        {
            Archer,
            Warrior,
            Engineer,
            Wizard
        }

        private Character SelectedCharacter;
        private PlayerIndex playerIndex;
        private GamePlayScreen gps;
        private Player p;
        private GamePadState gamePadState, oldgamePadState;
        private Game1 game;
        private bool Selected = false;
        int speed = 3;
        public SelectCharacter(Game1 game, PlayerIndex playerIndex)
        {
            this.game = game;
            this.playerIndex = playerIndex;
           
        }

        public void Update()
        {
            
            gamePadState = GamePad.GetState(playerIndex);
            
            

            #region Select character
            switch (SelectedCharacter)
            {
                case Character.Archer:
                    #region Archer
                    if (gamePadState.DPad.Right == ButtonState.Pressed && oldgamePadState.DPad.Right == ButtonState.Released && Selected == false)
                    {
                        SelectedCharacter = Character.Warrior;
                    }
                    if (gamePadState.DPad.Left == ButtonState.Pressed && oldgamePadState.DPad.Left == ButtonState.Released && Selected == false)
                    {
                        SelectedCharacter = Character.Wizard;
                    }
                    if (gamePadState.Buttons.A == ButtonState.Pressed && oldgamePadState.Buttons.A == ButtonState.Released && Selected == false && GameData.archerSelect == false)
                    {
                        p = new Archer(TextureManager.testTextureArcher, TextureManager.ArhHUD, new Vector2(1665, 3235), game.Window, gps, 100, 100, speed,1 , playerIndex);
                        GameData.playerList.Add(p);
                        Selected = true;
                        GameData.archerSelect = true;
                    }
                    #endregion
                    break;
                case Character.Warrior:
                    #region Warrior
                    if (gamePadState.DPad.Right == ButtonState.Pressed && oldgamePadState.DPad.Right == ButtonState.Released && Selected == false)
                    {
                        SelectedCharacter = Character.Engineer;
                    }
                    if (gamePadState.DPad.Left == ButtonState.Pressed && oldgamePadState.DPad.Left == ButtonState.Released && Selected == false)
                    {
                        SelectedCharacter = Character.Archer;
                    }
                    if (gamePadState.Buttons.A == ButtonState.Pressed && oldgamePadState.Buttons.A == ButtonState.Released && Selected == false && GameData.warriorSelect == false)
                    {
                        p = new Warrior(TextureManager.warriorAnimation,TextureManager.WarHUD, new Vector2(1800, 3235), game.Window, gps, 1, 1, speed, 5, playerIndex);
                        GameData.playerList.Add(p);
                        Selected = true;
                        GameData.warriorSelect = true;
                    }
                    #endregion
                    break;
                case Character.Engineer:
                    #region Engineer
                    if (gamePadState.DPad.Right == ButtonState.Pressed && oldgamePadState.DPad.Right == ButtonState.Released && Selected == false)
                    {
                        SelectedCharacter = Character.Wizard;
                    }
                    if (gamePadState.DPad.Left == ButtonState.Pressed && oldgamePadState.DPad.Left == ButtonState.Released && Selected == false)
                    {
                        SelectedCharacter = Character.Warrior;
                    }
                    if (gamePadState.Buttons.A == ButtonState.Pressed && oldgamePadState.Buttons.A == ButtonState.Released && Selected == false && GameData.engineerSelect == false)
                    {
                        p = new Engineer(TextureManager.testTextureEngineer,TextureManager.EngHUD, new Vector2(1665, 3315), game.Window, gps, 100, 100, speed, 3,playerIndex);
                        GameData.playerList.Add(p);
                        Selected = true;
                        GameData.engineerSelect = true;
                    }
                    #endregion
                    break;
                case Character.Wizard:
                    #region Wizard
                    if (gamePadState.DPad.Right == ButtonState.Pressed && oldgamePadState.DPad.Right == ButtonState.Released && Selected == false)
                    {
                        SelectedCharacter = Character.Archer;
                    }
                    if (gamePadState.DPad.Left == ButtonState.Pressed && oldgamePadState.DPad.Left == ButtonState.Released && Selected == false)
                    {
                        SelectedCharacter = Character.Engineer;
                    }
                    if (gamePadState.Buttons.A == ButtonState.Pressed && oldgamePadState.Buttons.A == ButtonState.Released && Selected == false && GameData.wizardSelect == false)
                    {
                        p = new Wizard(TextureManager.WizardAnimation, TextureManager.WizHUD, new Vector2(1800, 3315), game.Window, gps, 75, 250, speed, 10, playerIndex);
                        GameData.playerList.Add(p);
                        Selected = true;
                        GameData.wizardSelect = true;
                    }
                    #endregion
                    break;

            }
            #endregion

            oldgamePadState = GamePad.GetState(playerIndex);
        }

        public void DrawPic(SpriteBatch spriteBatch)
        {

            #region Draw Single player select
            if (GameData.SinglePlayMode == true)
            {
                switch (SelectedCharacter)
                {
                    case Character.Archer:
                        #region Archer
                        spriteBatch.Draw(TextureManager.ArcherPic, new Vector2(414, 277), Color.White);
                        #endregion
                        break;
                    case Character.Warrior:
                        #region Warrior
                        spriteBatch.Draw(TextureManager.WarriorPic, new Vector2(414, 282), Color.White);
                        #endregion
                        break;
                    case Character.Engineer:
                        #region Engineer
                        spriteBatch.Draw(TextureManager.EngineerPic, new Vector2(414, 277), Color.White);
                        #endregion
                        break;
                    case Character.Wizard:
                        #region Wizard
                        spriteBatch.Draw(TextureManager.WizardPic, new Vector2(414, 277), Color.White);
                        #endregion
                        break;
                }

            }
            #endregion


            #region Draw Multiplayer select
            if (GameData.MultiplayerMode == true)
            {
                switch (SelectedCharacter)
                {
                    case Character.Archer:
                        #region Archer
                        if(playerIndex == PlayerIndex.One)
                            spriteBatch.Draw(TextureManager.ArcherPic, new Vector2(27, 203), Color.White);
                        if (playerIndex == PlayerIndex.Two)
                            spriteBatch.Draw(TextureManager.ArcherPic, new Vector2(827, 203), Color.White);
                        if (playerIndex == PlayerIndex.Three)
                            spriteBatch.Draw(TextureManager.ArcherPic, new Vector2(27, 500), Color.White);
                       if (playerIndex == PlayerIndex.Four)
                           spriteBatch.Draw(TextureManager.ArcherPic, new Vector2(815, 500), Color.White);                                      
                        #endregion
                        break;
                    case Character.Warrior:
                        #region Warrior
                        if (playerIndex == PlayerIndex.One)
                            spriteBatch.Draw(TextureManager.WarriorPic, new Vector2(27, 203), Color.White);
                        if (playerIndex == PlayerIndex.Two)
                            spriteBatch.Draw(TextureManager.WarriorPic, new Vector2(827, 203), Color.White);
                        if (playerIndex == PlayerIndex.Three)
                            spriteBatch.Draw(TextureManager.WarriorPic, new Vector2(27, 500), Color.White);
                        if (playerIndex == PlayerIndex.Four)
                            spriteBatch.Draw(TextureManager.WarriorPic, new Vector2(827, 500), Color.White);
                        #endregion
                        break;
                    case Character.Engineer:
                        #region Engineer
                         if (playerIndex == PlayerIndex.One)
                            spriteBatch.Draw(TextureManager.EngineerPic, new Vector2(27, 203), Color.White);
                         if (playerIndex == PlayerIndex.Two)
                             spriteBatch.Draw(TextureManager.EngineerPic, new Vector2(827, 203), Color.White);
                         if (playerIndex == PlayerIndex.Three)
                             spriteBatch.Draw(TextureManager.EngineerPic, new Vector2(27, 500), Color.White);
                         if (playerIndex == PlayerIndex.Four)
                             spriteBatch.Draw(TextureManager.EngineerPic, new Vector2(827, 500), Color.White);
                        #endregion
                        break;
                    case Character.Wizard:
                        #region Wizard
                         if (playerIndex == PlayerIndex.One)
                            spriteBatch.Draw(TextureManager.WizardPic, new Vector2(27, 203), Color.White);
                         if (playerIndex == PlayerIndex.Two)
                             spriteBatch.Draw(TextureManager.WizardPic, new Vector2(827, 203), Color.White);
                         if (playerIndex == PlayerIndex.Three)
                             spriteBatch.Draw(TextureManager.WizardPic, new Vector2(27, 500), Color.White);
                         if (playerIndex == PlayerIndex.Four)
                             spriteBatch.Draw(TextureManager.WizardPic, new Vector2(827, 500), Color.White);
                        #endregion
                        break;
                }

            }
            #endregion

        }

        public void DrawText(SpriteBatch spriteBatch)
        {

            #region Draw Single player select
            if (GameData.SinglePlayMode == true)
            {
                switch (SelectedCharacter)
                {
                    case Character.Archer:
                        #region Archer
                        spriteBatch.DrawString(FontManager.SteamFont, "Archer", new Vector2(450, 410), Color.White);
                        spriteBatch.DrawString(FontManager.SteamFont, "Namn: HEjsan"
                                                                     +"\nType: Range"
                                                                     +"\nAttribut: Agility"
                                                                     +"\nHP: 100"
                                                                     +"\nMP: 100", new Vector2(585, 315), Color.Black);
                        if(GameData.archerSelect == true)
                        spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(375,345), Color.Green, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);

                        #endregion
                        break;
                    case Character.Warrior:
                        #region Warrior
                        spriteBatch.DrawString(FontManager.SteamFont, "Warrior", new Vector2(450, 410), Color.White);
                        spriteBatch.DrawString(FontManager.SteamFont, "Namn: LoL"
                                                                     + "\nType: Melee"
                                                                     + "\nAttribut: strength"
                                                                     + "\nHP: 100"
                                                                     + "\nMP: 100", new Vector2(585, 315), Color.Black);
                        if (GameData.warriorSelect == true)
                            spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(375, 345), Color.Green, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        #endregion
                        break;
                    case Character.Engineer:
                        #region Engineer
                        spriteBatch.DrawString(FontManager.SteamFont, "Engineer", new Vector2(450, 410), Color.White);
                        spriteBatch.DrawString(FontManager.SteamFont, "Namn: Sebastian"
                                                                     + "\nType: Range"
                                                                     + "\nAttribut: Balance"
                                                                     + "\nHP: 100"
                                                                     + "\nMP: 100", new Vector2(585, 315), Color.Black);
                        if (GameData.engineerSelect == true)
                            spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(375, 345), Color.Green, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        #endregion
                        break;
                    case Character.Wizard:
                        #region Wizard
                        spriteBatch.DrawString(FontManager.SteamFont, "Wizard", new Vector2(450, 410), Color.White);
                        spriteBatch.DrawString(FontManager.SteamFont, "Namn: Sir Anton"
                                                                     + "\nType: Range"
                                                                     + "\nAttribut: Intelligence"
                                                                     + "\nHP: 75"
                                                                     + "\nMP: 250", new Vector2(585, 315), Color.Black);
                        if (GameData.wizardSelect == true)
                            spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(375, 345), Color.Green, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        #endregion
                        break;
                }

            }
            #endregion

            #region Draw Multiplayer select
            if (GameData.MultiplayerMode == true)
            {
                switch (SelectedCharacter)
                {
                    case Character.Archer:
                        #region Archer
                        if (playerIndex == PlayerIndex.One)
                        {
                            spriteBatch.DrawString(FontManager.font, "Archer", new Vector2(35, 350), Color.White);
                            if (GameData.archerSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(0, 250), Color.Green, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }
                        if (playerIndex == PlayerIndex.Two)
                        {
                            spriteBatch.DrawString(FontManager.font, "Archer", new Vector2(830, 350), Color.White);
                            if (GameData.archerSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(800, 255), Color.Blue, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }
                        if (playerIndex == PlayerIndex.Three)
                        {
                            spriteBatch.DrawString(FontManager.font, "Archer", new Vector2(35, 650), Color.White);
                            if (GameData.archerSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(0, 549), Color.Red, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }
                        if (playerIndex == PlayerIndex.Four)
                        {
                            spriteBatch.DrawString(FontManager.font, "Archer", new Vector2(830, 650), Color.White);
                            if (GameData.archerSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(785, 548), Color.White, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }

                        #endregion
                        break;
                    case Character.Warrior:
                        #region WarriorText
                        if (playerIndex == PlayerIndex.One)
                        {
                            spriteBatch.DrawString(FontManager.font, "Warrior", new Vector2(35, 350), Color.White);
                            if (GameData.warriorSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(0, 250), Color.Green, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }
                        if (playerIndex == PlayerIndex.Two)
                        {
                            spriteBatch.DrawString(FontManager.font, "Warrior", new Vector2(830, 350), Color.White);
                            if (GameData.warriorSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(800, 255), Color.Blue, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }
                        if (playerIndex == PlayerIndex.Three)
                        {
                            spriteBatch.DrawString(FontManager.font, "Warrior", new Vector2(35, 650), Color.White);
                            if (GameData.warriorSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(0, 549), Color.Red, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }
                        if (playerIndex == PlayerIndex.Four)
                        {
                            spriteBatch.DrawString(FontManager.font, "Warrior", new Vector2(830, 650), Color.White);
                            if (GameData.warriorSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(785, 548), Color.White, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }
                        #endregion
                        break;
                    case Character.Engineer:
                        #region EngineerText
                        if (playerIndex == PlayerIndex.One)
                        {
                            spriteBatch.DrawString(FontManager.font, "Engineer", new Vector2(35, 350), Color.White);
                            if (GameData.engineerSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(0, 250), Color.Green, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }
                        if (playerIndex == PlayerIndex.Two)
                        {
                            spriteBatch.DrawString(FontManager.font, "Engineer", new Vector2(830, 350), Color.White);
                            if (GameData.engineerSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(800, 255), Color.Blue, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }
                        if (playerIndex == PlayerIndex.Three)
                        {
                            spriteBatch.DrawString(FontManager.font, "Engineer", new Vector2(35, 650), Color.White);
                            if (GameData.engineerSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(0, 549), Color.Red, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }
                        if (playerIndex == PlayerIndex.Four)
                        {
                            spriteBatch.DrawString(FontManager.font, "Engineer", new Vector2(830, 650), Color.White);
                            if (GameData.engineerSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(785, 548), Color.White, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }
                        #endregion
                        break;
                    case Character.Wizard:
                        #region Text
                        if (playerIndex == PlayerIndex.One)
                        {
                            spriteBatch.DrawString(FontManager.font, "Wizard", new Vector2(35, 350), Color.White);
                            if (GameData.wizardSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(0, 250), Color.Green, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }
                        if (playerIndex == PlayerIndex.Two)
                        {
                            spriteBatch.DrawString(FontManager.font, "Wizard", new Vector2(830, 350), Color.White);
                            if (GameData.wizardSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(800, 255), Color.Blue, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }
                        if (playerIndex == PlayerIndex.Three)
                        {
                            spriteBatch.DrawString(FontManager.font, "Wizard", new Vector2(35, 650), Color.White);
                            if (GameData.wizardSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(0, 549), Color.Red, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }
                        if (playerIndex == PlayerIndex.Four)
                        {
                            spriteBatch.DrawString(FontManager.font, "Wizard", new Vector2(830, 650), Color.White);
                            if (GameData.wizardSelect == true)
                                spriteBatch.DrawString(FontManager.SelectFont, "Selected", new Vector2(785, 548), Color.White, -.60f, Vector2.Zero, 1, SpriteEffects.None, 1);
                        }
                        #endregion
                        break;
                }

            }
            #endregion

        }

    }
}
