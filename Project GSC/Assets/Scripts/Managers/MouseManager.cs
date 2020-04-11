using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.GameModels.Astronomy;
using Assets.Scripts.UI;

namespace Assets.Scripts.Managers
{
    public class MouseManager : MonoBehaviour
    {
        public GameController gc;
        [SerializeField]
        private int firstStarIndex;
        [SerializeField]
        private int secondStarIndex;
        [SerializeField]
        public PanelManager panelManager;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    GameObject hitObject = hitInfo.collider.gameObject;
                    SolarSystem ss = hitObject.GetComponentInParent<GalaxyStar>().solarSystem;

                    panelManager.OpenStarPanel(ss);
                }
            }
        }
    }
}