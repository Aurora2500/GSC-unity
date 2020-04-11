using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightClickClose : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    GameObject go;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(go == null)
            {
                go = gameObject;
            }
            Destroy(go);
        }
    }
}
