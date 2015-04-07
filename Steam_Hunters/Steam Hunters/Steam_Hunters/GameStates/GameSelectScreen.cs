using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class GameSelectScreen
    {
        enum Charakter
        {
            Archer,
            Warrior,
            Engineer,
            Wizard
        }
        GamePlayScreen gps;
        //
        private Player p1, p2, p3, p4;
        private Game1 game;
        public GamePadState gamePadState, oldgamePadState = GamePad.GetState(PlayerIndex.One);
        Charakter SelectedCharakter;
        public GameSelectScreen(Game1 game)
        {
            this.game = game;
            this.gps = new GamePlayScreen(game);
            this.SelectedCharakter = Charakter.Archer;
        }

        public void Update()
        {
            oldgamePadState = gamePadState;
            gamePadState = GamePad.GetState(PlayerIndex.One);

            #region Singleplayer
            if (GameData.SinglePlayMode == true)
            {
                switch (SelectedCharakter)
                {
                    case Charakter.Archer:
                        #region Archer
                        if (gamePadState.DPad.Right == ButtonState.Pressed && oldgamePadState.DPad.Right == ButtonState.Released)
                        {
                            SelectedCharakter = Charakter.Warrior;
                        }
                        if (gamePadState.DPad.Left == ButtonState.Pressed && oldgamePadState.DPad.Left == ButtonState.Released)
                        {
                            SelectedCharakter = Charakter.Wizard;
                        }
                        if (gamePadState.Buttons.Start == ButtonState.Pressed && oldgamePadState.Buttons.Start == ButtonState.Released)
                        {
                            p1 = new Archer(TextureManager.testTextureArcher, new Vector2(50, 400), game.Window, gps, 1, 1, 5, 1);
                            GameData.playerList.Add(p1);
                        }
                        #endregion
                        break;
                    case Charakter.Warrior:
                        #region Warrior
                        if (gamePadState.DPad.Right == ButtonState.Pressed && oldgamePadState.DPad.Right == ButtonState.Released)
                        {
                            SelectedCharakter = Charakter.Engineer;
                        }
                        if (gamePadState.DPad.Left == ButtonState.Pressed && oldgamePadState.DPad.Left == ButtonState.Released)
                        {
                            SelectedCharakter = Charakter.Archer;
                        }
                        if (gamePadState.Buttons.Start == ButtonState.Pressed && oldgamePadState.Buttons.Start == ButtonState.Released)
                        {
                            p1 = new Warrior(TextureManager.testTextureArcher, new Vector2(50, 400), game.Window, gps, 1, 1, 5, 1);
                            GameData.playerList.Add(p1);
                        }
                        #endregion
                        break;
                    case Charakter.Engineer:
                        #region Engineer
                        if (gamePadState.DPad.Right == ButtonState.Pressed && oldgamePadState.DPad.Right == ButtonState.Released)
                        {
                            SelectedCharakter = Charakter.Wizard;
                        }
                        if (gamePadState.DPad.Left == ButtonState.Pressed && oldgamePadState.DPad.Left == ButtonState.Released)
                        {
                            SelectedCharakter = Charakter.Warrior;
                        }
                        if (gamePadState.Buttons.Start == ButtonState.Pressed && oldgamePadState.Buttons.Start == ButtonState.Released)
                        {
                            p1 = new Engineer(TextureManager.testTextureEngineer, new Vector2(50, 400), game.Window, gps, 1, 1, 5, 1);
                            GameData.playerList.Add(p1);
                        }
                        #endregion
                        break;
                    case Charakter.Wizard:
                        #region Wizard
                        if (gamePadState.DPad.Right == ButtonState.Pressed && oldgamePadState.DPad.Right == ButtonState.Released)
                        {
                            SelectedCharakter = Charakter.Archer;
                        }
                        if (gamePadState.DPad.Left == ButtonState.Pressed && oldgamePadState.DPad.Left == ButtonState.Released)
                        {
                            SelectedCharakter = Charakter.Warrior;
                        }
                        if (gamePadState.Buttons.Start == ButtonState.Pressed && oldgamePadState.Buttons.Start == ButtonState.Released)
                        {
                            p1 = new Wizard(TextureManager.testTextureArcher, new Vector2(50, 400), game.Window, gps, 1, 1, 5, 1);
                            GameData.playerList.Add(p1);
                        }
                        #endregion
                        break;

                }
                if (gamePadState.Buttons.B == ButtonState.Pressed && oldgamePadState.Buttons.B == ButtonState.Released)
                    game.StartScreen();


            }
            #endregion

            #region Multiplayer
            if (GameData.MultiplayerMode == true)
            {

            }
            #endregion

            if (gamePadState.Buttons.Start == ButtonState.Pressed && oldgamePadState.Buttons.Start == ButtonState.Released && GameData.playerList.Count != 0)
            {
                game.StartGame();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            #region Draw Single player select
            if (GameData.SinglePlayMode == true)
            {
                spriteBatch.Draw(TextureManager.chooseSingleplayer, new Vector2(0f, 0f), Color.White);
                switch (SelectedCharakter)
                {
                    case Charakter.Archer:
                        #region Archer

                        #endregion
                        break;
                    case Charakter.Warrior:
                        #region Warrior


                        #endregion
                        break;
                    case Charakter.Engineer:
                        #region Engineer

                        #endregion
                        break;
                    case Charakter.Wizard:
                        #region Wizard

                        #endregion
                        break;
                }
            }
            #endregion

            #region Draw Multi player Select
            if (GameData.MultiplayerMode == true)
            {
            }
            #endregion

            spriteBatch.End();
        }

    }
}
