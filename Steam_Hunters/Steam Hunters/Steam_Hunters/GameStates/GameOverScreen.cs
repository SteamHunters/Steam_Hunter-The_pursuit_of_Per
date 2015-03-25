using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class GameOverScreen
    {
        private Game1 game;
        private KeyboardState lastState;

        public GameOverScreen(Game1 game)
        {
            this.game = game;
            TextureManager.LoadTextures(game);
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.End();
        }
    }
}
