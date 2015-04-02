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
        Wizard wizard;
        Warrior warrior;
        Archer archer;

        //engineer
        Engineer engineer;
        public List<Dispenser> dispensers = new List<Dispenser>();
        public List<Missile> missiles = new List<Missile>();
        public List<EngineerTower> turrets = new List<EngineerTower>();

        public GamePlayScreen(Game1 game)
        {
            this.game = game;
            TextureManager.LoadContent(game);

            //wiz = new Wizard(TextureManager.testTexture, new Vector2(400, 400), game.Window, this,1,1,5, 2);
            engineer = new Engineer(TextureManager.testTexture, new Vector2(200, 200), game.Window, this,1,1,5,1);

            camera = new Camera(game.GraphicsDevice.Viewport);
        }
        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            engineer.Update(gameTime);
            //wizard.Update(gameTime);
            camera.Update(gameTime, engineer);

#region engineerstuff(turret, dispenser etc)

            #region dispenser
            foreach (Dispenser d in dispensers)
            {
                d.Update(gameTime);

                if(dispensers.Count > 1)
                {
                    dispensers.Remove(d);
                    break;
                }
                if(d.DispenserRemove == true)
                {
                    dispensers.Remove(d);
                    break;
                }

                #region dispenser heal
                if (d.IsInRange(engineer.pos))
                {
                    engineer.color = Color.Green;
                }
                else
                {
                    engineer.color = Color.White;
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
            #endregion

            #region turret
            foreach (EngineerTower t in turrets)
             {
                 t.Update(gameTime);
                
                if (turrets.Count > 3)
                 {
                     turrets.Remove(t);
                     break;
                 }
                 t.rotation = engineer.angle;

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
#endregion

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //camera
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            spriteBatch.Draw(TextureManager.map, new Vector2(-200, -200), Color.White);
            engineer.Draw(spriteBatch);
            //wizard.Draw(spriteBatch);
            //wizard.Draw(spriteBatch);
            spriteBatch.Draw(TextureManager.testTexture, new Vector2(0f, 0f), Color.White);

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
