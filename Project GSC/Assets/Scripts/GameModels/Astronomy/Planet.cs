using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameModels.Astronomy
{
    public enum PlanetType
    {
        Artic,
        Continental,
        Desert,
        Gas
    }

    public class Planet
    {
        public int Index { get; private set; }
        public SolarSystem System { get; set; }

        public PlanetType Type { get; private set; }

        public virtual bool IsColonizable { get => false; }


        public Planet(SolarSystem ss, int i, PlanetType t)
        {
            System = ss;
            Index = i;
            Type = t;
        }

        public Planet()
        {
        }

        internal void Remove()
        {
            System = null;
        }
    }
}