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
        Vector2 scorePos, highscorePos, backPos, nrONE, nrTWO, nrTHREE, nrFOUR, nrFIVE;

        public GameOverScreen(Game1 game)
        {
            this.game = game;
            nrONE = new Vector2(950, 100);
            nrTWO = new Vector2(950, 200);
            nrTHREE = new Vector2(950, 300);
            nrFOUR = new Vector2(950, 400);
            nrFIVE = new Vector2(950, 500);
            backPos = new Vector2(950, 600);
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
                    GamePlayScreen.score = 0;
                    GamePlayScreen.boolHighScoresRun = false;

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

            spriteBatch.DrawString(FontManager.pauseFont, GamePlayScreen.textHighScores_02[0], nrONE, Color.White);
            spriteBatch.DrawString(FontManager.pauseFont, GamePlayScreen.textHighScores_02[1], nrTWO, Color.White);
            spriteBatch.DrawString(FontManager.pauseFont, GamePlayScreen.textHighScores_02[2], nrTHREE, Color.White);
            spriteBatch.DrawString(FontManager.pauseFont, GamePlayScreen.textHighScores_02[3], nrFOUR, Color.White);
            spriteBatch.DrawString(FontManager.pauseFont, GamePlayScreen.textHighScores_02[4], nrFIVE, Color.White);
            spriteBatch.End();
        }
    }
}
