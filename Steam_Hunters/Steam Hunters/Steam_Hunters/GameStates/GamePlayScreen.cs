using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griddy2D;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace Steam_Hunters
{
    class GamePlayScreen
    {
        private World level1;//, level2, level3;
        private Game1 game;
        public Camera camera;
        private Vector2 cameraCenter;
        private Dialog dialog;
        private List <CloudAnimation> cloudAnimation;

        //Highscore kod
        FileStream theFileRead, theFileWrite;
        StreamWriter theScoreWrite;
        StreamReader theScoreRead;
        List<Player> playerlist = new List<Player>();
        public static String[] textHighScores_01;
        public static String[] textHighScores_02;
        int maxHighScores = 5;
        public static Boolean boolHighScoresRun = false;
        public static int score;

        //engineer 
        public List<Player> engineerList = new List<Player>();
         public List<Player> WizardList = new List<Player>();
        public List<Dispenser> dispensers = new List<Dispenser>();
        public List<Missile> missiles = new List<Missile>();
        public List<EngineerTower> turrets = new List<EngineerTower>();
        public List<Projectile> turretProjectile = new List<Projectile>();
        //
        public List<Enemies> enemyList = new List<Enemies>();
        public List<NPC> npcList = new List<NPC>();

        public GamePlayScreen(Game1 game)
        {
            this.game = game;
            GameData.volym = 0.1f;
            if (GameData.Level == 1)
                MediaPlayer.Play(MusicManager.Level1Music);

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
                if (p is Wizard)
                {
                    WizardList.Add(p);
                }
            }
            #endregion

            enemyList.Add(new Enemies(TextureManager.MonsterTest, new Vector2(100, 150), new Point(50, 50), new Point(4, 2), 1, 1, 1, 1, 125, 250, 110, 1, 1, false, 1));
            npcList.Add(new NPC(TextureManager.NPCTexture, new Vector2(2105, 2645), 200));
            npcList.Add(new NPC(TextureManager.NPCTexture, new Vector2(3850, 3575), 200));

            cloudAnimation = new List<CloudAnimation>();
            //cloudAnimation.Add(new CloudAnimation(game.graphics, TextureManager.cloud1Texture, 0.0f));
            cloudAnimation.Add(new CloudAnimation(game.graphics, TextureManager.cloud2Texture, 10.0f));
            //cloudAnimation.Add(new CloudAnimation(game.graphics, TextureManager.cloud3Texture, 50.0f)); 

            level1 = new World(game.Content);
            camera = new Camera(game.GraphicsDevice.Viewport);
            
            textHighScores_01 = new String[maxHighScores];
            textHighScores_02 = new String[maxHighScores];
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

            #region Update player/players
            foreach (Player p in GameData.playerList)
            {
                p.Update(gameTime);

                #region Check collision whit tiles
                foreach (EngineerTower t in turrets)
                {
                    if (p.IsCollidingObject(t))
                    {
                        p.HandleCollision();
                    }
                }

                foreach (Tile h in level1.hitboxList)
                {
                    Rectangle rect = new Rectangle((int)h.Position.X, (int)h.Position.Y, 50, 50);
                    if (p.hitBox.Intersects(rect))
                    {
                        p.HandleCollision();
                    }
                    #region remove Wizards spells if hits hitboxes
                    foreach (Wizard w in GameData.playerList)
                    {
                        foreach (Projectile f in w.FireBallList)
                        {
                            if (f.hitBox.Intersects(rect))
                                w.FireBallList.Remove(f);
                            break;
                        }
                        foreach (Projectile wa in w.WaterBallList)
                        {
                            if (wa.hitBox.Intersects(rect))
                                w.WaterBallList.Remove(wa);
                            break;
                        }
                        foreach (Projectile b in w.BoulderList)
                        {
                            if (b.hitBox.Intersects(rect))
                                w.boulderOn = false;
                            break;
                        }

                      
                    }
                    #endregion
                }
                #endregion

                #region Paus game
                if (Player.paused)
                {
                    if (p.Apress)
                        GameData.volym = 0f;
                    if (p.Bpress)
                        game.EndGame();
                }
                #endregion

                #region The game ends if all player i in ghost mode in multiplayer
                if (GameData.MultiplayerMode == true)
                {
                    if (GameData.playerList.Count == 1)
                    {
                        if (GameData.playerList[0].ghostMode == true)
                        {
                            game.EndGame();
                        }
                    }
                    if (GameData.playerList.Count == 2)
                    {
                        if (GameData.playerList[0].ghostMode == true && GameData.playerList[1].ghostMode == true)
                        {
                            game.EndGame();
                        }
                    }
                    if (GameData.playerList.Count == 3)
                    {
                        if (GameData.playerList[0].ghostMode == true && GameData.playerList[1].ghostMode == true && GameData.playerList[2].ghostMode == true)
                        {
                            game.EndGame();
                        }
                    }
                    if (GameData.playerList.Count == 4)
                    {
                        if (GameData.playerList[0].ghostMode == true && GameData.playerList[1].ghostMode == true && GameData.playerList[2].ghostMode == true && GameData.playerList[3].ghostMode == true)
                        {
                            game.EndGame();
                        }
                    }
                }
                #endregion

                #region Eniemes
                foreach (Enemies e in enemyList)
                {
                    if (e.target == null )
                    {
                        e.GetClosestPlayer(GameData.playerList);
                    }

                    if(p.ghostMode == true)
                    {
                        if(p.hitBox.Intersects(e.hitBox))
                        {
                            e.MovementSpeed = 50;
                        }
                        else
                        {
                            e.MovementSpeed = 110;
                        }
                    }
                    if(p.isHurt == false)
                    {
                        //Ett test bara ska inte vara så för alla enemies
                        if(p.IsCollidingEntity(e))
                        {
                            p.statusWindow.hp -= 50;
                            p.isHurt = true;
                        }
                    }
                }
                #endregion

                if (p.isDead == true)
                    game.EndGame();
            }
            #endregion

            if (Player.paused == false)
            {
                #region Eniemes
                foreach (Enemies e in enemyList)
                {
                    e.Update(gameTime);
                    if (e.target != null)
                    {
                        if (e.target.ghostMode == true)
                        {
                            e.Aggro = false;
                            e.target = null;
                        }
                    }
                    foreach (Projectile p in turretProjectile)
                    {
                        if (p.IsCollidingEntity(e))
                        {
                            //e.life = -10;
                            turretProjectile.Remove(p);
                            break;
                        }
                    }
                    foreach (Missile m in missiles)
                    {
                        if (m.IsCollidingEntity(e))
                        {
                            //e.life = -10;
                            missiles.Remove(m);
                            break;
                        }
                    }

                }
                #endregion
                #region NPC
                foreach (NPC n in npcList)
                {
                    n.Update(gameTime);

                    foreach (Player p in GameData.playerList)
                    {
                        if (n.Buyer == null)
                        {
                            n.GetClosestBuyer(GameData.playerList);
                        }
                        if (p.LBpress)
                        {
                            if (n.IsInRange(p.center) == true)
                            {
                                n.buy = true;
                                p.buying = true;
                            }
                            //else
                            //{
                            //    n.buy = false;
                            //    p.buying = false;
                            //}
                        }
                        if (p.Backpress)
                            n.buy = false;
                    }

                }
                #endregion
                #region Clouds
                foreach (CloudAnimation cA in cloudAnimation)
                {
                    cA.MoveDown(gameTime);

                    foreach (Player p in GameData.playerList)
                    {
                        if (p.newState.ThumbSticks.Left.Y != 0)
                            cA.speed = p.newState.ThumbSticks.Left.Y * 50;
                        else
                            cA.speed = 15;
                    }
                }
                #endregion
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
                                    p.statusWindow.hp += 0.075f;
                                    if (p.statusWindow.hp > p.statusWindow.maxHp)
                                        p.statusWindow.hp = p.statusWindow.maxHp;
                                }
                                else
                                {
                                    p.color = Color.White;
                                }
                                if(p.IsCollidingObject(d))
                                    p.HandleCollision();
                                    
                            }
                            #endregion
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

                    if (m.missileRemove == true)
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {     
            //camera
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            
            level1.DrawLayerBase(spriteBatch);
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
            #region Draw PLayer and Enimes
            foreach (Enemies e in enemyList)
            {
                e.Draw(spriteBatch);
            }
            foreach (Player p in GameData.playerList)
            {
                p.Draw(spriteBatch);
            }
            #endregion
            level1.DrawLayerTop(spriteBatch);
            //level1.DrawLayerHitbox(spriteBatch);
            #region Draw Interface
            foreach (Player p in GameData.playerList)
            {
                p.DrawInterface(spriteBatch);
            }
            #endregion

            spriteBatch.End();

            //clouds
            spriteBatch.Begin();
            foreach (CloudAnimation cA in cloudAnimation)
            {
                cA.Draw(spriteBatch);
            };
            spriteBatch.End();

            //static
            spriteBatch.Begin();
            #region Set HUD per player
            if (GameData.playerList.Count == 1)
            {
                spriteBatch.Draw(TextureManager.player1HUD, Vector2.Zero, Color.White);
                spriteBatch.Draw(GameData.playerList[0].HUDPic, new Vector2(15, 9), Color.White);
                GenerateHealthBar(GameData.playerList[0].statusWindow.hp, GameData.playerList[0].statusWindow.maxHp, new Vector2(90, 8), spriteBatch);
                GenerateManaBar(GameData.playerList[0].statusWindow.mana, GameData.playerList[0].statusWindow.maxMana, new Vector2(90, 22), spriteBatch);
                GameData.playerList[0].DrawPotion(new Vector2(81, 42), spriteBatch);

            }
            if (GameData.playerList.Count == 2)
            {
                spriteBatch.Draw(TextureManager.player1HUD, Vector2.Zero, Color.White);
                spriteBatch.Draw(GameData.playerList[0].HUDPic, new Vector2(15, 9), Color.White);
                GenerateHealthBar(GameData.playerList[0].statusWindow.hp, GameData.playerList[0].statusWindow.maxHp, new Vector2(90, 8), spriteBatch);
                GenerateManaBar(GameData.playerList[0].statusWindow.mana, GameData.playerList[0].statusWindow.maxMana, new Vector2(90, 22), spriteBatch);
                GameData.playerList[0].DrawPotion(new Vector2(81, 42), spriteBatch);

                spriteBatch.Draw(TextureManager.player2HUD, new Vector2(1280 - TextureManager.player2HUD.Width, 0), Color.White);
                spriteBatch.Draw(GameData.playerList[1].HUDPic, new Vector2(1280 - 15 - GameData.playerList[1].HUDPic.Width , 9), Color.White);
                GenerateHealthBar(GameData.playerList[1].statusWindow.hp, GameData.playerList[1].statusWindow.maxHp, new Vector2(1280 - 90 - 150, 8), spriteBatch);
                GenerateManaBar(GameData.playerList[1].statusWindow.mana, GameData.playerList[1].statusWindow.maxMana, new Vector2(1280 - 90 - 150, 22), spriteBatch);
            }
            if (GameData.playerList.Count == 3)
            {
                spriteBatch.Draw(TextureManager.player1HUD, Vector2.Zero, Color.White);
                spriteBatch.Draw(GameData.playerList[0].HUDPic, new Vector2(15, 9), Color.White);
                GenerateHealthBar(GameData.playerList[0].statusWindow.hp, GameData.playerList[0].statusWindow.maxHp, new Vector2(90, 8), spriteBatch);
                GenerateManaBar(GameData.playerList[0].statusWindow.mana, GameData.playerList[0].statusWindow.maxMana, new Vector2(90, 22), spriteBatch);
                GameData.playerList[0].DrawPotion(new Vector2(81, 42), spriteBatch);

                spriteBatch.Draw(TextureManager.player2HUD, new Vector2(1280 - TextureManager.player2HUD.Width, 0), Color.White);
                spriteBatch.Draw(GameData.playerList[1].HUDPic, new Vector2(1280 - 15 - GameData.playerList[1].HUDPic.Width, 9), Color.White);
                GenerateHealthBar(GameData.playerList[1].statusWindow.hp, GameData.playerList[1].statusWindow.maxHp, new Vector2(1280 - 90 - 150, 8), spriteBatch);
                GenerateManaBar(GameData.playerList[1].statusWindow.mana, GameData.playerList[1].statusWindow.maxMana, new Vector2(1280 - 90 - 150, 22), spriteBatch);

                spriteBatch.Draw(TextureManager.player3HUD, new Vector2(0, 720 - TextureManager.player2HUD.Height), Color.White);
                spriteBatch.Draw(GameData.playerList[2].HUDPic, new Vector2(15, 720 - TextureManager.player2HUD.Height + 6), Color.White);
                GenerateHealthBar(GameData.playerList[2].statusWindow.hp, GameData.playerList[2].statusWindow.maxHp, new Vector2( 90,720 - 8 - 10), spriteBatch);
                GenerateManaBar(GameData.playerList[2].statusWindow.mana, GameData.playerList[2].statusWindow.maxMana, new Vector2(90, 720 - 22 - 10), spriteBatch);

            }
            if (GameData.playerList.Count == 4)
            {
                spriteBatch.Draw(TextureManager.player1HUD, Vector2.Zero, Color.White);
                spriteBatch.Draw(GameData.playerList[0].HUDPic, new Vector2(15, 9), Color.White);
                GenerateHealthBar(GameData.playerList[0].statusWindow.hp, GameData.playerList[0].statusWindow.maxHp, new Vector2(90, 8), spriteBatch);
                GenerateManaBar(GameData.playerList[0].statusWindow.mana, GameData.playerList[0].statusWindow.maxMana, new Vector2(90, 22), spriteBatch);
                GameData.playerList[0].DrawPotion(new Vector2(81, 42), spriteBatch);

                spriteBatch.Draw(TextureManager.player2HUD, new Vector2(1280 - TextureManager.player2HUD.Width, 0), Color.White);
                spriteBatch.Draw(GameData.playerList[1].HUDPic, new Vector2(1280 - 15 - GameData.playerList[1].HUDPic.Width, 9), Color.White);
                GenerateHealthBar(GameData.playerList[1].statusWindow.hp, GameData.playerList[1].statusWindow.maxHp, new Vector2(1280 - 90 - 150, 8), spriteBatch);
                GenerateManaBar(GameData.playerList[1].statusWindow.mana, GameData.playerList[1].statusWindow.maxMana, new Vector2(1280 - 90 - 150, 22), spriteBatch);

                spriteBatch.Draw(TextureManager.player3HUD, new Vector2(0, 720 - TextureManager.player2HUD.Height), Color.White);
                spriteBatch.Draw(GameData.playerList[2].HUDPic, new Vector2(15, 720 - TextureManager.player2HUD.Height + 6), Color.White);
                GenerateHealthBar(GameData.playerList[2].statusWindow.hp, GameData.playerList[2].statusWindow.maxHp, new Vector2(90, 720 - 8 - 10), spriteBatch);
                GenerateManaBar(GameData.playerList[2].statusWindow.mana, GameData.playerList[2].statusWindow.maxMana, new Vector2(90, 720 - 22 - 10), spriteBatch);

                spriteBatch.Draw(TextureManager.player4HUD, new Vector2(1280 - TextureManager.player2HUD.Width, 720 - TextureManager.player2HUD.Height), Color.White);
                spriteBatch.Draw(GameData.playerList[3].HUDPic, new Vector2(1280 - 15 - GameData.playerList[3].HUDPic.Width, 720 - TextureManager.player2HUD.Height + 6), Color.White);
                GenerateHealthBar(GameData.playerList[3].statusWindow.hp, GameData.playerList[3].statusWindow.maxHp, new Vector2(1280 - 90 - 150, 720 - 8 - 10), spriteBatch);
                GenerateManaBar(GameData.playerList[3].statusWindow.mana, GameData.playerList[3].statusWindow.maxMana, new Vector2(1280 - 90 - 150, 720 - 22 - 10), spriteBatch);
            }
            #endregion

            if (Player.paused == true)
            {
                spriteBatch.DrawString(FontManager.pauseFont, "Paused", new Vector2(570, 200), Color.White);
                spriteBatch.DrawString(FontManager.SteamFont, "Press back to unpause\nPress A to mute\nPress B to End game", new Vector2(580, 300), Color.White);
            }

            spriteBatch.End();
        }


        public void GenerateHealthBar(double CurrentHp, double MaxHp, Vector2 pos, SpriteBatch spriteBatch)
        {
            Double Percent = (Double)CurrentHp / MaxHp;
            spriteBatch.Draw(TextureManager.hpTexture, new Vector2(pos.X, pos.Y), new Rectangle(0, 0, (int)(Percent * 150), 10), Color.White);
        }
        public void GenerateManaBar(double CurrentMana, double MaxMana,Vector2 pos, SpriteBatch spriteBatch)
        {
            Double Percent = (Double)CurrentMana / MaxMana;
            spriteBatch.Draw(TextureManager.manaTexture, new Vector2(pos.X, pos.Y), new Rectangle(0, 0, (int)(Percent * 150), 10), Color.White);
        }
    }
}
