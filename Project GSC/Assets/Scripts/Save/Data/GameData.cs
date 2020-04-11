using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GSC;

namespace Assets.Scripts.Save.Data
{
    [Serializable]
    public class GameData
    {
        public GalaxyData galaxy;

        public GameData(GameInstance game)
        {
            galaxy = new GalaxyData(game.galaxy);
        }
    }
}