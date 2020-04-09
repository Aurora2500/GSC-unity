using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameModels.Astronomy
{
    public class Link
    {

        public int startIndex;
        public int endIndex;

        public Vector3 Start { get; private set; }
        public Vector3 End { get; private set; }
        public float Distance
        {
            get
            {
                return (End - Start).magnitude;
            }
        }

        public GameObject linkRender;
        public Link(Vector3 _start, int _startIndex, Vector3 _end, int _endIndex, GameObject prefab)
        {
            Start = _start;
            End = _end;

            startIndex = _startIndex;
            endIndex = _endIndex;

            linkRender = Object.Instantiate(prefab);

            var linkScript = linkRender.GetComponent<StarLink>();
            linkScript.Setup(Start, startIndex, End, endIndex);
        }

        public void Destroy()
        {
            Object.Destroy(linkRender);
        }
    }
}
