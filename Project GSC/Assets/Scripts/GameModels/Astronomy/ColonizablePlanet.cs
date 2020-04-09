using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameModels.Astronomy
{
    public class ColonizablePlanet : Planet
    {
        public override bool IsColonizable { get => true; }

        public ColonizablePlanet() : base()
        {

        }
    }
}