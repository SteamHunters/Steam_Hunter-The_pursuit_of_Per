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
        private Game1 game;
        private GamePadState oldgamePadStateP1 = GamePad.GetState(PlayerIndex.One);

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
                selectCharacterP1.DrawPic(spriteBatch);
                spriteBatch.Draw(TextureManager.chooseSingleplayer, new Vector2(0f, 0f), Color.White);
                selectCharacterP1.DrawText(spriteBatch);

            }
            #endregion

            #region Multiplayer
            if (GameData.MultiplayerMode == true)
            {
                selectCharacterP1.DrawPic(spriteBatch);
                selectCharacterP2.DrawPic(spriteBatch);
                selectCharacterP3.DrawPic(spriteBatch);
                selectCharacterP4.DrawPic(spriteBatch);
                spriteBatch.Draw(TextureManager.chooseMultiplayer, new Vector2(0f, 0f), Color.White);
                selectCharacterP1.DrawText(spriteBatch);
                selectCharacterP2.DrawText(spriteBatch);
                selectCharacterP3.DrawText(spriteBatch);
                selectCharacterP4.DrawText(spriteBatch);

            }
            #endregion

  

            spriteBatch.End();
        }


    }
}
