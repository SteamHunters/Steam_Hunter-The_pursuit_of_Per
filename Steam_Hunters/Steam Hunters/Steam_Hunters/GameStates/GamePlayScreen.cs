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
        private World level1;
        private Game1 game;
        public Camera camera;
        private Vector2 cameraCenter;

        List<Player> playerlist = new List<Player>();

        //engineer 
        public Engineer engineer;
        //Allt ska inte vara i engineerklassen, det är inte objektorienterat även om det ser ut så, kan förklara på skype om du svarar:P 
        public List<Dispenser> dispensers = new List<Dispenser>();
        public List<Missile> missiles = new List<Missile>();
        public List<EngineerTower> turrets = new List<EngineerTower>();
        public List<Projectile> turretProjectile = new List<Projectile>();


        public Player e1;
        Player e2;
        Player w;

        public GamePlayScreen(Game1 game)
        {
            this.game = game;

            //wiz = new Wizard(TextureManager.testTexture, new Vector2(400, 400), game.Window, this,1,1,5, 2);
            //engineer = new Engineer(TextureManager.testTexture, new Vector2(200, 200), game.Window, this,1,1,5,1);

            //Test build det ska sedan funka så här sen //Anton
            e1 = new Engineer(TextureManager.testTexture, new Vector2(50, 400), game.Window, this, 1, 1, 5, 1);
            playerlist.Add(e1);
            e2 = new Engineer(TextureManager.testTexture, new Vector2(200, 200), game.Window, this, 1, 1, 5, 3);
            playerlist.Add(e2);
            w = new Wizard(TextureManager.testTexture, new Vector2(400, 400), game.Window, this, 1, 1, 5,2);
            playerlist.Add(w);
            //
            level1 = new World(game.Content);
            camera = new Camera(game.GraphicsDevice.Viewport);
        }
        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            //engineer.Update(gameTime);
            //wizard.Update(gameTime);

            #region Set Camera center
            if (playerlist.Count == 1)
            {
                cameraCenter = playerlist[0].pos / playerlist.Count;
                camera.Update(gameTime, cameraCenter);
            }
            if (playerlist.Count == 2)
            {
                cameraCenter = (playerlist[0].pos + playerlist[1].pos) / playerlist.Count;
                camera.Update(gameTime, cameraCenter);
            }
            if (playerlist.Count == 3)
            {
                cameraCenter = (playerlist[0].pos + playerlist[1].pos + playerlist[2].pos) / playerlist.Count;
                camera.Update(gameTime, cameraCenter);
            }
            if (playerlist.Count == 4)
            {
                cameraCenter = (playerlist[0].pos + playerlist[1].pos + playerlist[2].pos + playerlist[3].pos) / playerlist.Count;
                camera.Update(gameTime, cameraCenter);
            }
            #endregion

            // Test build
            foreach (Player p in playerlist)
            {
                p.Update(gameTime);
            }
            // 


            #region engineerstuff(turret, dispenser etc)

            #region dispenser

            for (int i = 0; i < playerlist.Count; i++)
                {
                    if (playerlist[i] is Engineer)
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
                            if (d.IsInRange(e1.pos))
                            {
                                e1.color = Color.Green;
                            }
                            else
                            {
                                e1.color = Color.White;
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
                if(t.towerLife <= 0)
                {
                    turrets.Remove(t);
                    break;
                }
                 t.rotation = e1.angle;

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


            foreach (Projectile tp in turretProjectile)
            {
                tp.Update(gameTime);

                if(tp.BulletRemove == true)
                {
                    turretProjectile.Remove(tp);
                    break;
                }
            }

            #endregion

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //camera
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

            level1.Draw(spriteBatch);

            spriteBatch.Draw(TextureManager.testTexture, new Vector2(0f, 0f), Color.White);

            // Test build
            foreach (Player p in playerlist)
            {
                p.Draw(spriteBatch);
            }
            //
            foreach (Projectile tp in turretProjectile)
            {
                tp.Draw(spriteBatch);
            }
            foreach(Dispenser d in dispensers)
            {
                d.Draw(spriteBatch);
            }
            foreach(Missile m in missiles)
            {
                m.Draw(spriteBatch);
            }
            foreach(EngineerTower t in turrets)
            {
                t.Draw(spriteBatch);
            }


            spriteBatch.End();
        }
    }
}
