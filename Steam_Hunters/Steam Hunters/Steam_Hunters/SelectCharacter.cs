using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    class SelectCharacter
    {
        private enum Character
        {
            Archer,
            Warrior,
            Engineer,
            Wizard
        }
        private Character SelectedCharacter;
        private GamePlayScreen gps;
        private Player p;
        private PlayerIndex playerIndex;
        public SelectCharacter(PlayerIndex playerIndex)
        {
            this.playerIndex = playerIndex;
           
        }

        public void Update()
        {
            //oldState = GamePad.GetState(playerIndex);
        }



    }
}
