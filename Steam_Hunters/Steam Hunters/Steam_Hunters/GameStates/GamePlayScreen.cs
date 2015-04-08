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
        private World level1;//, level2, level3;
        private Game1 game;
        public Camera camera;
        private Vector2 cameraCenter;

        List<Player> playerlist = new List<Player>();

        //engineer 
        public Engineer engineer;
        public List<Dispenser> dispensers = new List<Dispenser>();
        public List<Missile> missiles = new List<Missile>();
        public List<EngineerTower> turrets = new List<EngineerTower>();
        public List<Projectile> turretProjectile = new List<Projectile>();
        //

        public Player e1;
        Player e2;
        Player w;
        Player a1;

        public GamePlayScreen(Game1 game)
        {
            foreach (Player p in GameData.playerList)
            {
                p.SetGPS = this;
            }
            this.game = game;

            //wiz = new Wizard(TextureManager.testTexture, new Vector2(400, 400), game.Window, this,1,1,5, 2);
            //engineer = new Engineer(TextureManager.testTexture, new Vector2(200, 200), game.Window, this,1,1,5,1);

            //Test build det ska sedan funka så här sen //Anton
            //e1 = new Engineer(TextureManager.testTextureEngineer, new Vector2(50, 400), game.Window, this, 1, 1, 5, 1);
            //playerlist.Add(e1);

            //e2 = new Engineer(TextureManager.testTexture, new Vector2(200, 200), game.Window, this, 1, 1, 5, 3);
            //playerlist.Add(e2);
            //w = new Wizard(TextureManager.testTextureEngineer, new Vector2(400, 400), game.Window, this, 1, 1, 5,2);
            //playerlist.Add(w);
            //

            //a1 = new Archer(TextureManager.testTextureArcher, new Vector2(200, 200), game.Window, this, 1, 1, 5, 2);
            //playerlist.Add(a1);

            level1 = new World(game.Content);
            camera = new Camera(game.GraphicsDevice.Viewport);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            #region Set Camera center by how many players
            if (GameData.playerList.Count == 1)
            {
                cameraCenter = GameData.playerList[0].pos / GameData.playerList.Count;
                camera.Update(gameTime, cameraCenter);
            }
            if (GameData.playerList.Count == 2)
            {
                cameraCenter = (GameData.playerList[0].pos + GameData.playerList[1].pos) / GameData.playerList.Count;
                camera.Update(gameTime, cameraCenter);
            }
            if (GameData.playerList.Count == 3)
            {
                cameraCenter = (GameData.playerList[0].pos + GameData.playerList[1].pos + GameData.playerList[2].pos) / GameData.playerList.Count;
                camera.Update(gameTime, cameraCenter);
            }
            if (GameData.playerList.Count == 4)
            {
                cameraCenter = (GameData.playerList[0].pos + GameData.playerList[1].pos + GameData.playerList[2].pos + GameData.playerList[3].pos) / GameData.playerList.Count;
                camera.Update(gameTime, cameraCenter);
            }
            #endregion

            // Test build
            foreach (Player p in GameData.playerList)
            {
                p.Update(gameTime);
            }
            // 

            #region engineerstuff(turret, dispenser etc)

            #region dispenser

            for (int i = 0; i < GameData.playerList.Count; i++)
            {
                if (GameData.playerList[i] is Engineer)
                {

                    foreach (Dispenser d in dispensers)
                    {
                        d.Update(gameTime);

                        if (dispensers.Count > 1)
                        {
                            dispensers.Remove(d);
                            break;
                        }
                        if (d.DispenserRemove == true)
                        {
                            dispensers.Remove(d);
                            break;
                        }

                        #region dispenser heal
                        foreach (Player p in GameData.playerList)
                        {
                            if (d.IsInRange(p.pos))
                            {
                                p.color = Color.Green;
                            }
                            else
                            {
                                p.color = Color.White;
                            }
                        }


                        //måste titta om de finns i spelet först, hur gör det?
                        //if (d.IsInRange(archer.pos))
                        //{
                        //    archer.color = Color.Green;
                        //}
                        //else
                        //{
                        //    archer.color = Color.White;
                        //}
                        //if (d.IsInRange(warrior.pos))
                        //{
                        //    warrior.color = Color.Green;
                        //}
                        //else
                        //{
                        //    warrior.color = Color.White;
                        //}
                        //if (d.IsInRange(wizard.pos))
                        //{
                        //    wizard.color = Color.Green;
                        //}
                        //else
                        //{
                        //    wizard.color = Color.White;
                        //}
                        #endregion

                        //collison spelare alla utom engineer
                        //if (wizard.IsCollidingObject(d))
                        //{
                        //    wizard.HandleCollision();
                        //}
                    }
                }
            }
            #endregion

            #region turret
            foreach (EngineerTower t in turrets)
            {
                t.Update(gameTime);

                if (turrets.Count > 2)
                {
                    turrets.Remove(t);
                    break;
                }
                if (t.towerLife <= 0)
                {
                    turrets.Remove(t);
                    break;
                }
                t.rotation = GameData.playerList[0].angle;

                if (t.TowerRemove == true)
                {
                    turrets.Remove(t);
                    break;
                }

            }
            #endregion

            #region missile
            foreach (Missile m in missiles)
            {
                m.Update(gameTime);
            }
            #endregion

            #region Turret projectile
            foreach (Projectile tp in turretProjectile)
            {
                tp.Update(gameTime);

                if (tp.BulletRemove == true)
                {
                    turretProjectile.Remove(tp);
                    break;
                }
            }
            #endregion

            #endregion

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //camera
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

            level1.Draw(spriteBatch);

            spriteBatch.Draw(TextureManager.testTextureEngineer, new Vector2(0f, 0f), Color.White);

            // Test build
            foreach (Player p in GameData.playerList)
            {
                p.Draw(spriteBatch);

            }
            //
            #region Enginers things
            foreach (Projectile tp in turretProjectile)
            {
                tp.Draw(spriteBatch);
            }
            foreach (Dispenser d in dispensers)
            {
                d.Draw(spriteBatch);
            }
            foreach (Missile m in missiles)
            {
                m.Draw(spriteBatch);
            }
            foreach (EngineerTower t in turrets)
            {
                t.Draw(spriteBatch);
            }
            #endregion

            spriteBatch.End();
        }
    }
}
