using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Steam_Hunters
{
    enum Screen
    {
        StartScreen,
        GameSelectScreen,
        GamePlayScreen,
        GameOverScreen
    }
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        StartScreen startScreen;
        GameSelectScreen gameSelectScreen;
        GamePlayScreen gamePlayScreen;
        GameOverScreen gameOverScreen;

        Screen currentScreen;

        int height, width;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //window
            width = graphics.PreferredBackBufferWidth = 1280;
            height = graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            graphics.IsFullScreen = false;

            this.Window.Title = "Steam Hunter - The pursuit of Per";

        }

       
        protected override void Initialize()
        {
            IsMouseVisible = true;

            base.Initialize();
        }

       
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.LoadContent(Content);
            FontManager.LoadContent(Content);
            MusicManager.LoadContent(Content);
            SoundEffectManager.LoadContent(Content);

            startScreen = new StartScreen(this);
            currentScreen = Screen.StartScreen;
            MediaPlayer.Play(MusicManager.MenyMusic);
            MediaPlayer.IsRepeating = true;
            
            base.LoadContent();

        }

      
        protected override void UnloadContent()
        {
           
        }

       
        protected override void Update(GameTime gameTime)
        {
            MediaPlayer.Volume = GameData.volym;
            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                if (GameData.playerList[0] != null)
                  GameData.playerList[0].statusWindow.money += 5;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (GameData.playerList[0] != null)
                    GameData.playerList[0].statusWindow.hp -= 10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
            }

            #region Exit game
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            #endregion

            #region Gamestate Update
            switch (currentScreen)
            {
                case Screen.StartScreen:
                    if (startScreen != null)
                        startScreen.Update();
                    break;
                case Screen.GameSelectScreen:
                    if (gameSelectScreen != null)
                        gameSelectScreen.Update(gameTime);
                    break;
                case Screen.GamePlayScreen:
                    if (gamePlayScreen != null)
                        gamePlayScreen.Update(gameTime);
                    break;
                case Screen.GameOverScreen:
                    if (gameOverScreen != null)
                        gameOverScreen.Update(gameTime);
                    break;
            }
            #endregion

            base.Update(gameTime);
        }

      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            #region GameState Draw
            switch (currentScreen)
            {
                case Screen.StartScreen:
                    if (startScreen != null)
                        startScreen.Draw(spriteBatch);
                    break;
                case Screen.GameSelectScreen:
                    if (gameSelectScreen != null)
                        gameSelectScreen.Draw(spriteBatch);
                    break;
                case Screen.GamePlayScreen:
                    if (gamePlayScreen != null)
                        gamePlayScreen.Draw(spriteBatch);
                    break;
                case Screen.GameOverScreen:
                    if (gameOverScreen != null)
                        gameOverScreen.Draw(spriteBatch);
                    break;
            }
            #endregion

            base.Draw(gameTime);
        }

        public void StartGame()
        {
            gamePlayScreen = new GamePlayScreen(this);
            currentScreen = Screen.GamePlayScreen;

            startScreen = null;
            gameOverScreen = null;
        }
        public void SelectScreen()
        {
            gameSelectScreen = new GameSelectScreen(this);
            currentScreen = Screen.GameSelectScreen;

            startScreen = null;
            gameOverScreen = null;
            gamePlayScreen = null;
        }
        public void StartScreen()
        {
            startScreen = new StartScreen(this);
            currentScreen = Screen.StartScreen;

            gameSelectScreen = null;
            gameOverScreen = null;
            gamePlayScreen = null;
        }

        public void EndGame()
        {
            gameOverScreen = new GameOverScreen(this);
            currentScreen = Screen.GameOverScreen;

            gamePlayScreen = null;
        }


    }
}
