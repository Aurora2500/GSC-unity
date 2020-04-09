using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameModels.Astronomy;

namespace Assets.Scripts.Managers
{
    public class MouseManager : MonoBehaviour
    {
        public GameController gc;
        [SerializeField]
        private int firstStarIndex;
        [SerializeField]
        private int secondStarIndex;

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
                    firstStarIndex = ss.Index;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    GameObject hitObject = hitInfo.collider.gameObject;
                    SolarSystem ss = hitObject.GetComponentInParent<GalaxyStar>().solarSystem;
                    secondStarIndex = ss.Index;
                    if (Path(firstStarIndex, secondStarIndex, out IEnumerable<int> path))
                    {
                        string pathstr = string.Join(", ", path);
                        Debug.Log($"PATH FOUND // start: {firstStarIndex} end: {secondStarIndex} Path: {pathstr}");
                    }
                    else
                    {
                        Debug.Log($"Path not found between {firstStarIndex} and {secondStarIndex}");
                    }
                }
            }
        }

        bool Path(int start, int end)
        {
            return gc.TryPath(start, end);
        }

        bool Path(int start, int end, out IEnumerable<int> path)
        {
            return gc.TryPath(start, end, out path);
        }
    }
}