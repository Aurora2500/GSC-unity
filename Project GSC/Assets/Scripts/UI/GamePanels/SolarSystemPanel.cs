using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.GameModels.Astronomy;

namespace Assets.Scripts.UI.GamePanels
{
    public class SolarSystemPanel : MonoBehaviour
    {
        public Transform container;

        public GameObject planetPanelPrefab;

        public void SetupPlanets(SolarSystem ss)
        {
            foreach (Planet planet in ss.planets)
            {
                var planetPanel = Instantiate(planetPanelPrefab, container);

                var pps = planetPanel.GetComponent<PlanetSlot>();

                pps.Setup(planet);
            }
        }
    }
}