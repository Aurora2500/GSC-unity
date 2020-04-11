using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Assets.Scripts.GameModels.Astronomy;

namespace Assets.Scripts.UI.GamePanels
{
    public class PlanetSlot : MonoBehaviour
    {
        string planetName;
        [SerializeField] TextMeshProUGUI nameText;

        PlanetType planetType;
        [SerializeField]TextMeshProUGUI typeText;

        public void Setup(Planet p)
        {
            planetName = p.Name;
            nameText.SetText(p.Name);

            planetType = p.Type;
            typeText.SetText(p.Type.ToString());
        }

        public void OpenPlanetPanel()
        {

        }
    }
}