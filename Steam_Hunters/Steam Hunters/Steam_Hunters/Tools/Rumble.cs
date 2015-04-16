using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Steam_Hunters
{
    public class Rumble
    {
        private float rumbleTimer;
        bool enableRumble;
        PlayerIndex index;

        public Rumble(PlayerIndex index)
        {
            enableRumble = false;
            this.index = index;
        }

        public void Update(float elapsed, PlayerIndex playerIndex)
        {
            if (enableRumble)
            {
                if (rumbleTimer > 0f)
                {
                    rumbleTimer -= elapsed;
                }
                else
                {
                    enableRumble = false;
                    rumbleTimer = 0f;
                    GamePad.SetVibration(playerIndex, 0, 0);
                }
            }
        }

        public void Vibrate(float time, float strength)
        {
            rumbleTimer = time;
            enableRumble = true;
            GamePad.SetVibration(index, strength, strength);
        }
    }
}
