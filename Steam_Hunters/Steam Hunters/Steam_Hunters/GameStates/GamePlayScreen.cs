using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class GamePlayScreen
    {
        private Game1 game;
        public Camera camera;
        Engineer engineer;
        Wizard wiz;

        public GamePlayScreen(Game1 game)
        {
            this.game = game;
            TextureManager.LoadContent(game);

            wiz = new Wizard(TextureManager.testTexture, new Vector2(400, 400), game.Window, this);
            engineer = new Engineer(TextureManager.testTexture, new Vector2(200, 200), game.Window, this);
            camera = new Camera(game.GraphicsDevice.Viewport);
        }
        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            engineer.Update(gameTime);
            camera.Update(gameTime, engineer);
            //wiz.Update(gameTime);
            //if (playerLife == 0)
            //{
            //    game.EndGame();
            //    GetHighscores();
            //}
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //camera
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            spriteBatch.Draw(TextureManager.map, new Vector2(-200, -200), Color.White);
            engineer.Draw(spriteBatch);
            //wiz.Draw(spriteBatch);
            spriteBatch.Draw(TextureManager.testTexture, new Vector2(0f, 0f), Color.White);

            spriteBatch.End();

            ////static 
            //spriteBatch.Begin();
            //spriteBatch.Draw(TextureManager.testTexture, new Vector2(0f, 0f), Color.White);
            //spriteBatch.End();
        }
    }
}
