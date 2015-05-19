using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steam_Hunters
{
    static class GameData
    {
       public static List<Player> playerList = new List<Player>();
       public static bool SinglePlayMode, MultiplayerMode;
       public static bool archerSelect, warriorSelect, wizardSelect, engineerSelect;
       public static int Level = 2;
       public static float volym = .5f;

    }
}
