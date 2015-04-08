using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    public class StartScreen
    {
        private Game1 game;
        Button singleplayerStart, multiplayerStart, instruction, exit;

        public GamePadState gamePadState, oldgamePadState = GamePad.GetState(PlayerIndex.One);
        bool showInstruction, showCharacterSelectSingle, showCharacterSelectMulti;

        public StartScreen(Game1 game)
        {
            this.game = game;

            singleplayerStart = new Button(TextureManager.singleplayerButton, new Vector2(500, 300));
            multiplayerStart = new Button(TextureManager.multiplayerButton, new Vector2(500, 500));
            instruction = new Button(TextureManager.instructionButton, new Vector2(170, 400));
            exit = new Button(TextureManager.exitButton, new Vector2(720, 400));
        }
        public void Update()
        {
            oldgamePadState = gamePadState;
            gamePadState = GamePad.GetState(PlayerIndex.One);
            

            #region moveStartMenu
            if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released && showCharacterSelectSingle == false && showCharacterSelectMulti == false)
            {
                multiplayerStart.selected = true;
                singleplayerStart.selected = false;
                exit.selected = false;
                instruction.selected = false;
            }
            if (gamePadState.DPad.Up == ButtonState.Pressed && oldgamePadState.DPad.Up == ButtonState.Released && showCharacterSelectSingle == false && showCharacterSelectMulti == false)
            {
                singleplayerStart.selected = true;
                multiplayerStart.selected = false;
                exit.selected = false;
                instruction.selected = false;
            }
            if (gamePadState.DPad.Left == ButtonState.Pressed && oldgamePadState.DPad.Left == ButtonState.Released && showCharacterSelectSingle == false && showCharacterSelectMulti == false)
            {
                instruction.selected = true;
                multiplayerStart.selected = false;
                singleplayerStart.selected = false;
                exit.selected = false;
            }
            if (gamePadState.DPad.Right == ButtonState.Pressed && oldgamePadState.DPad.Right == ButtonState.Released && showCharacterSelectSingle == false && showCharacterSelectMulti == false)
            {
                instruction.selected = false;
                multiplayerStart.selected = false;
                singleplayerStart.selected = false;
                exit.selected = true;
            }
            #endregion

            #region SinglePlayer
            if (gamePadState.Buttons.A == ButtonState.Pressed && oldgamePadState.Buttons.A == ButtonState.Released && singleplayerStart.selected == true && showInstruction == false)
            {
                GameData.SinglePlayMode = true;
                game.SelectScreen();
            }

            //if (gamePadState.Buttons.B == ButtonState.Pressed && oldgamePadState.Buttons.B == ButtonState.Released && showCharacterSelectSingle == true && showInstruction == false && showCharacterSelectMulti == false)
            //{
            //    showCharacterSelectSingle = false;
            //}

            //if (gamePadState.Buttons.Start == ButtonState.Pressed && oldgamePadState.Buttons.Start == ButtonState.Released && showCharacterSelectSingle == true)
            //{
            //    game.StartGame();
            //}

            #endregion


            if (gamePadState.Buttons.A == ButtonState.Pressed && oldgamePadState.Buttons.A == ButtonState.Released && multiplayerStart.selected == true && showCharacterSelectSingle == false && showCharacterSelectMulti == false && showInstruction == false)
            {
                showCharacterSelectMulti = true;
            }
            if (gamePadState.Buttons.B == ButtonState.Pressed && oldgamePadState.Buttons.B == ButtonState.Released && showCharacterSelectSingle == false && showInstruction == false && showCharacterSelectMulti == true)
            {
                showCharacterSelectMulti = false;
            }
            if (gamePadState.Buttons.A == ButtonState.Pressed && exit.selected == true && showCharacterSelectSingle == false && showCharacterSelectMulti == false && showInstruction == false)
            {
                game.Exit();
            }
            if (gamePadState.Buttons.A == ButtonState.Pressed && instruction.selected == true && showCharacterSelectSingle == false && showCharacterSelectMulti == false)
            {
                showInstruction = true;
            }
            if (gamePadState.Buttons.B == ButtonState.Pressed && oldgamePadState.Buttons.B == ButtonState.Released && showInstruction == true && showCharacterSelectSingle == false && showCharacterSelectMulti == false)
            {
                showInstruction = false;
            }


        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(TextureManager.startBackground, new Vector2(0f, 0f), Color.White);
            singleplayerStart.Draw(spriteBatch);
            multiplayerStart.Draw(spriteBatch);
            instruction.Draw(spriteBatch);
            exit.Draw(spriteBatch);

            if (showInstruction == true)
                spriteBatch.Draw(TextureManager.instructionScreen, new Vector2(0f, 0f), Color.White);

            //if(showCharacterSelectSingle == true)
            //{
            //    spriteBatch.Draw(TextureManager.chooseSingleplayer, new Vector2(0f, 0f), Color.White);
            //}
            if (showCharacterSelectMulti == true)
            {
                spriteBatch.Draw(TextureManager.chooseMultiplayer, new Vector2(0f, 0f), Color.White);
            }

            spriteBatch.End();
        }
    }
}
