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
    
        private SelectCharacter selectCharacterP1, selectCharacterP2, selectCharacterP3, selectCharacterP4;
        private GamePlayScreen gps;
        private Player p1;
        private Game1 game;
        private GamePadState gamePadStateP1, oldgamePadStateP1 = GamePad.GetState(PlayerIndex.One);
        private GamePadState gamePadStateP2, oldgamePadStateP2 = GamePad.GetState(PlayerIndex.Two);
        private GamePadState gamePadStateP3, oldgamePadStateP3 = GamePad.GetState(PlayerIndex.Three);
        private GamePadState gamePadStateP4, oldgamePadStateP4 = GamePad.GetState(PlayerIndex.Four);

        public GameSelectScreen(Game1 game)
        {
            this.game = game;
            this.gps = new GamePlayScreen(game);
            selectCharacterP1 = new SelectCharacter(game, PlayerIndex.One);
            if (GameData.MultiplayerMode == true)
            {
                selectCharacterP2 = new SelectCharacter(game, PlayerIndex.Two);
                selectCharacterP3 = new SelectCharacter(game, PlayerIndex.Three);
                selectCharacterP4 = new SelectCharacter(game, PlayerIndex.Four);
            }
            
        }

        public void Update()
        {
            #region Singleplayer
            if (GameData.SinglePlayMode == true)
            {
                selectCharacterP1.Update();
            }            
            #endregion

            #region Multiplayer
            if (GameData.MultiplayerMode == true)
            {
                selectCharacterP1.Update();
                selectCharacterP2.Update();
                selectCharacterP3.Update();
                selectCharacterP4.Update();
            }
            #endregion

            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed && oldgamePadStateP1.Buttons.B == ButtonState.Released)
            {
                GameData.playerList.Clear();
                GameData.SinglePlayMode = false;
                GameData.MultiplayerMode = false;
                game.StartScreen();
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed && oldgamePadStateP1.Buttons.Start == ButtonState.Released && GameData.playerList.Count != 0)
                game.StartGame();

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            #region Singleplayer
            if (GameData.SinglePlayMode == true)
            {
                selectCharacterP1.Draw(spriteBatch);

            }
            #endregion

            #region Multiplayer
            if (GameData.MultiplayerMode == true)
            {
                spriteBatch.Draw(TextureManager.chooseMultiplayer, new Vector2(0f, 0f), Color.White);
                selectCharacterP1.Draw(spriteBatch);
                selectCharacterP2.Draw(spriteBatch);
                selectCharacterP3.Draw(spriteBatch);
                selectCharacterP4.Draw(spriteBatch);
            }
            #endregion

  

            spriteBatch.End();
        }


    }
}
