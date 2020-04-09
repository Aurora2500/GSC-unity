using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class DraggableUI : MonoBehaviour, IDragHandler
    {
        [SerializeField] private RectTransform panelParent;

        public void OnDrag(PointerEventData eventData)
        {
            panelParent.anchoredPosition += eventData.delta;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}