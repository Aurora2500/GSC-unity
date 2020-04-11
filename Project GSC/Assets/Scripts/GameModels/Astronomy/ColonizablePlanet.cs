using Assets.Scripts.GameModels.Colonies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameModels.Astronomy
{
    public class ColonizablePlanet : Planet
    {
        public Colony Colony { get; private set; }

        public ColonizablePlanet(SolarSystem ss, int i, PlanetType t) : base(ss, i, t)
        {
            Colony = new Colony();
        }
        public ColonizablePlanet(Save.Data.ColonizablePlanetData pd, SolarSystem ss) : base(pd, ss)
        {
            Colony = new Colony(pd.colony);
        }
    }
}