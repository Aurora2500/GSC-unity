using Assets.Scripts.GameModels.Colonies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameModels.Astronomy
{
    public class ColonizablePlanet : Planet
    {
        public Colony Colony { get; private set; }
        public ColonizablePlanet() : base()
        {

        }
    }
}