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
        private Dialog dialog;
        
        List<Player> playerlist = new List<Player>();

        //engineer 
        public List<Player> engineerList = new List<Player>();
        public List<Dispenser> dispensers = new List<Dispenser>();
        public List<Missile> missiles = new List<Missile>();
        public List<EngineerTower> turrets = new List<EngineerTower>();
        public List<Projectile> turretProjectile = new List<Projectile>();
        //
        public List<Enemies> enemyList = new List<Enemies>();
        public List<NPC> npcList = new List<NPC>();

        public GamePlayScreen(Game1 game)
        {
            #region Få ut / sätter in data i GameData.playerlist
            foreach (Player p in GameData.playerList)
            {
                p.SetGPS = this;
            }
            this.game = game;

           foreach(Player p in GameData.playerList)
            {
                if (p is Engineer)
                {
                    engineerList.Add(p);
                }
            }
            #endregion

           enemyList.Add(new Enemies(TextureManager.MonsterTest, new Vector2(100, 150), new Point(50, 50), new Point(4, 2), 1, 1, 1, 1, 125, 250, 50, 1, 1, false, 1));
           npcList.Add(new NPC(TextureManager.NPCTexture, new Vector2(700, 700), 200));


            level1 = new World(game.Content);
            camera = new Camera(game.GraphicsDevice.Viewport);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState ms = new MouseState();
            

            if (ms.RightButton == ButtonState.Pressed)
            {
                enemyList.Add(new Enemies(TextureManager.testTextureArcher, new Vector2(ms.X, ms.Y), new Point(45, 45), new Point(45, 45), 1, 1, 1, 1, 10, 1, 1, 1, 1, false, 1));
            } 

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

            #region Update player/players, Enimes and NPC
            foreach (Player p in GameData.playerList)
            {
                p.Update(gameTime);
                
                #region NPC
                foreach (NPC n in npcList)
                {
                    n.Update(gameTime);

                    if (n.Buyer == null)
                    {
                        n.GetClosestBuyer(GameData.playerList);
                    }
                    if (n.IsInRange(p.pos))
                    {
                        if (p.LBpress)
                        {
                            n.buy = true;
                            p.buying = true;
                        }
                        if (p.Backpress)
                            n.buy = false;
                    }
                }
                #endregion

                #region Enimes
                foreach (Enemies e in enemyList)
                {
                    e.Update(gameTime);

                    if (e.target == null)
                    {
                       // p.color = Color.White;
                        e.GetClosestPlayer(GameData.playerList);
                    }
                    else
                        p.color = Color.Red;
                }
                #endregion
            }
            #endregion

            // jag ändrade så denna bit finns i den ovanför
            #region NPC 
            //foreach (NPC n in npcList)
            //{
            //    n.Update(gameTime);

            //    foreach (Player p in GameData.playerList)
            //    {
            //        if (n.Buyer == null)
            //        {
            //            n.GetClosestBuyer(GameData.playerList);
            //        }
            //        if (p.LBpress)
            //        {
            //            n.buy = true;
            //            p.buying = true;
            //        }
            //        if (p.Backpress)
            //            n.buy = false;
            //    }

            //}
            #endregion 
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
            foreach (Player e in engineerList)
            {
            foreach (EngineerTower t in turrets)
            {
                    t.UpdateTrue(gameTime, e);

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
                    t.rotation = e.angle;

                    if (t.TowerRemove == true)
                    {
                        turrets.Remove(t);
                        break;
                    }
                }
            }
            #endregion

            #region missile
            foreach (Missile m in missiles)
            {
                m.Update(gameTime);

                foreach (Enemies e in enemyList)
                {
                    if (m.Target == null)
                    {
                        m.GetClosestEnemy(enemyList);
                    }
                }

                if(m.missileRemove == true)
                {
                    missiles.Remove(m);
                    break;
                }
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

            #region Enginers things
            foreach (Projectile tp in turretProjectile)
            {
                tp.Draw(spriteBatch);
            }
            foreach (Dispenser d in dispensers)
            {
                d.Draw(spriteBatch);
            }
            foreach (EngineerTower t in turrets)
            {
                t.Draw(spriteBatch);
            }
            foreach (Missile m in missiles)
            {
                m.Draw(spriteBatch);
            }
            foreach(NPC n in npcList)
            {
                n.Draw(spriteBatch);
            }
            #endregion
            #region Enemies


            foreach (Enemies e in enemyList)
            {
                e.Draw(spriteBatch);
            }
            foreach (Player p in GameData.playerList)
            {
                p.Draw(spriteBatch);
            }
            #endregion
            spriteBatch.End();
        }
    }
}
