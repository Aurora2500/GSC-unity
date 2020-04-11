using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

using Assets.Scripts.GameModels.Astronomy;

namespace Assets.Scripts.UI.GamePanels
{
    public class PlanetSlot : MonoBehaviour, IPointerClickHandler 
    {
        Planet planet;
        [SerializeField] TextMeshProUGUI nameText;

        PlanetType planetType;
        [SerializeField]TextMeshProUGUI typeText;

        public void Setup(Planet p)
        {
            planet = p;

            nameText.SetText(p.Name);
            typeText.SetText(p.Type.ToString());
        }

        public void OpenPlanetPanel()
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OpenPlanetPanel();
        }
    }
}