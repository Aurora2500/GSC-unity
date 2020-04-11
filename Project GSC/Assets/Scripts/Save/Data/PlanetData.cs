using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.GameModels.Astronomy;

namespace Assets.Scripts.Save.Data
{
    [Serializable]
    public class PlanetData
    {
        public string name;
        public int index;
        public int ssIndex;
        public int planetType;

        public PlanetData(Planet p)
        {
            name = p.Name;
            index = p.Index;
            ssIndex = p.SystemIndex;
            planetType = (int) p.Type;
        }
    }
    [Serializable]
    public class ColonizablePlanetData : PlanetData
    {
        public ColonyData colony;
        public ColonizablePlanetData(ColonizablePlanet cp) : base (cp)
        {
            colony = new ColonyData(cp.Colony);
        }
    }
}