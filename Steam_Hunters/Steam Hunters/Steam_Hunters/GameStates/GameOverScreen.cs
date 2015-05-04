using Microsoft.Xna.Framework;
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
        //private KeyboardState lastState;
        List<Player> playerlist = new List<Player>();

        public GameOverScreen(Game1 game)
        {
            this.game = game;
        }
        public void Update(GameTime gameTime)
        {
            foreach (Player p in GameData.playerList)
            {
                p.Update(gameTime);

                if(p.Startpress)
                {
                    GameData.playerList.Clear();
                    GameData.archerSelect = false;
                    GameData.engineerSelect = false;
                    GameData.warriorSelect = false;
                    GameData.wizardSelect = false;

                    Player.paused = false;
                    game.StartScreen();
                    break;
                }
                if (p.Bpress)
                {
                    game.Exit();
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(TextureManager.gameOverScreen, Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }
}
