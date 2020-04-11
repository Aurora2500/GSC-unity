using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Save.Data;

namespace Assets.Scripts.GameModels.Astronomy
{
    public class Galaxy
    {
        public List<SolarSystem> solarSystems;
        public int Size { get { return solarSystems.Count; } }

        public Galaxy()
        {
            solarSystems = new List<SolarSystem>();
        }

        public Galaxy(GalaxyData data)
        {
            solarSystems = new List<SolarSystem>();
            foreach (var ssData in data.solarSystems)
            {
                var ss = new SolarSystem(ssData, this);
                AddSolarSystem(ss);
            }

            foreach (SolarSystem ss in solarSystems)
            {
                ss.LoadLinks();
            }
        }

        public void AddSolarSystem(SolarSystem ss)
        {
            solarSystems.Add(ss);
        }

        #region saves

        public void Destroy()
        {
            foreach (SolarSystem ss in solarSystems)
            {
                ss.Destroy();
            }
        }
        #endregion
    }
}