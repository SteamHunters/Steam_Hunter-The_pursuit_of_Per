using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class Warrior : Player
    {
        public Warrior(Texture2D tex, Texture2D HUDPic, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, int damage, PlayerIndex playerIndex)
            : base(tex, HUDPic, pos, window, gps, hp, mana, speed, damage, playerIndex)
        {
            //                                                      name, int, str, agil, vit, luck, hp, mp, lvl 
            statusWindow = new StatusWindow(TextureManager.turretBullet, pos, "hej", 0, 0, 0, 0, 0, hp, mana, 1, playerIndex);
            frameSize = new Point(50, 50);
        }


        public override void Update(GameTime gameTime)
        {
            statusWindow.SetPos = pos;
            statusWindow.Update(gameTime);




            if (statusWindow.hp < statusWindow.maxHp)
            {
                if(isDead == false)
                statusWindow.hp += 5 * ((1 + (statusWindow.vitality / 20)) * time / 2);
            }

            base.Update(gameTime);
            //ShootRightThumbStick(newState, gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
