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
        bool showInstruction;

        public StartScreen(Game1 game)
        {
            this.game = game;
            TextureManager.LoadContent(game);

            singleplayerStart = new Button(TextureManager.testTexture, new Vector2(300, 300));
            multiplayerStart = new Button(TextureManager.testTexture, new Vector2(300, 400));
            instruction = new Button(TextureManager.testTexture, new Vector2(100, 400));
            exit = new Button(TextureManager.testTexture, new Vector2(500, 400));
        }
        public void Update()
        {
            oldgamePadState = gamePadState;
            gamePadState = GamePad.GetState(PlayerIndex.One);

            if (gamePadState.DPad.Down == ButtonState.Pressed && oldgamePadState.DPad.Down == ButtonState.Released)
            {
                multiplayerStart.selected = true;
                singleplayerStart.selected = false;
                exit.selected = false;
                instruction.selected = false;
            }
            if (gamePadState.DPad.Up == ButtonState.Pressed && oldgamePadState.DPad.Up == ButtonState.Released)
            {
                singleplayerStart.selected = true;
                multiplayerStart.selected = false;
                exit.selected = false;
                instruction.selected = false;
            }
            if (gamePadState.DPad.Left == ButtonState.Pressed && oldgamePadState.DPad.Left == ButtonState.Released)
            {
                instruction.selected = true;
                multiplayerStart.selected = false;
                singleplayerStart.selected = false;
                exit.selected = false;
            }
            if (gamePadState.DPad.Right == ButtonState.Pressed && oldgamePadState.DPad.Right == ButtonState.Released)
            {
                instruction.selected = false;
                multiplayerStart.selected = false;
                singleplayerStart.selected = false;
                exit.selected = true;
            }

            if (gamePadState.Buttons.A == ButtonState.Pressed && singleplayerStart.selected == true)
            {
                game.StartGame();
                ChooseCharacterSinglePlayer();
            }
            if (gamePadState.Buttons.A == ButtonState.Pressed && multiplayerStart.selected == true)
            {
                //game.StartGame();
                ChooseCharacterMultiplayer();
            }

            if (gamePadState.Buttons.A == ButtonState.Pressed && exit.selected == true)
            {
                game.Exit();
            }
            if (gamePadState.Buttons.A == ButtonState.Pressed && instruction.selected == true)
            {
                showInstruction = true;
            }
            if (gamePadState.Buttons.B == ButtonState.Pressed && oldgamePadState.Buttons.B == ButtonState.Released && showInstruction == true)
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
                spriteBatch.Draw(TextureManager.testTexture, new Vector2(400, 400), Color.White);

            spriteBatch.End();
        }

        public void ChooseCharacterSinglePlayer()
        {
            //bla bla
        }

        public void ChooseCharacterMultiplayer()
        {
            //bla bla 
        }
    }
}
