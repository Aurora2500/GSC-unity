using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GSC;

[Serializable]
public class GameData 
{
    public GalaxyData galaxy;

    public GameData(GameInstance game)
    {
        galaxy = new GalaxyData(game.galaxy);
    }
}
