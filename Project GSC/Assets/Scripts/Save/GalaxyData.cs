using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameModels.Astronomy;

[Serializable]
public class GalaxyData
{
    public SolarSystemData[] solarSystems;

    public GalaxyData(Galaxy g)
    {
        solarSystems = new SolarSystemData[g.solarSystems.Count];
        for (int i = 0; i < g.solarSystems.Count; i++)
        {
            solarSystems[i] = new SolarSystemData(g.solarSystems[i]);
        }
    }
}
