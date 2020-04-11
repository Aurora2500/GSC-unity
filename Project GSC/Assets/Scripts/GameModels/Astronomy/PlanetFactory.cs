using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameModels.Astronomy
{
    class PlanetFactory
    {
        static PlanetFactory pf;

        int index = 0;
        int numOfTypes;

        public static PlanetFactory GetFactory()
        {

            if(pf == null)
            {
                pf = new PlanetFactory();
            }

            return pf;
        }

        private PlanetFactory()
        {
            CacheTypeNum();
        }

        public void Reset()
        {
            pf = null;
            index = 0;
        }

        public Planet GetPlanet(SolarSystem ss)
        {
            PlanetType randomType = (PlanetType) UnityEngine.Random.Range(0, (int)numOfTypes);

            return ByType(ss, randomType);
        }

        Planet ByType(SolarSystem ss, PlanetType t)
        {
            switch (t)
            {
                case PlanetType.Artic:
                case PlanetType.Continental:
                case PlanetType.Desert:
                    return new ColonizablePlanet(ss, index++, t);
                case PlanetType.Gas:
                    return new Planet(ss, index++, t);
                default:
                    throw new ArgumentException();
            }
        }

        void CacheTypeNum()
        {
            if (numOfTypes == 0)
            {
                numOfTypes = Enum.GetNames(typeof(PlanetType)).Length;
            }
        }
    }
}
