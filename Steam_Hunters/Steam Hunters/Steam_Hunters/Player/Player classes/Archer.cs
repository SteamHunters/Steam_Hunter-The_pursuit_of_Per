using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class Archer : Player
    {
        public Archer(Texture2D tex, Vector2 pos, GameWindow window, GamePlayScreen gps, int hp, int mana, int speed, int playerIndex)
            : base(tex, pos, window, gps, hp, mana, speed, playerIndex)
        {

            //Kod för att leapa, inte riktigt färdig! Men kan byggas på för att få rätt resultat
            //if (prevThumbStickRightValue.X != 0 || prevThumbStickRightValue.Y != 0)
            //{
            //    jump += prevThumbStickRightValue;
            //    jump.Normalize();
            //    pos += jump * 100;
            //}
        }


    }
}
