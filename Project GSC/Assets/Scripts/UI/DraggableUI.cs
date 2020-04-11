using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class DraggableUI : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        [SerializeField] private RectTransform panelParent;

        public void OnDrag(PointerEventData eventData)
        {
            panelParent.anchoredPosition += eventData.delta;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            panelParent.SetAsLastSibling();
        }
    }
}