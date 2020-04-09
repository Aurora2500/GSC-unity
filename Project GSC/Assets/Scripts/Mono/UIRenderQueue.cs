using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRenderQueue : MonoBehaviour
{

    public UnityEngine.Rendering.CompareFunction comparison = UnityEngine.Rendering.CompareFunction.Always;

    public bool apply = true;


    // Update is called once per frame
    void Update()
    {
        if(apply)
        {
            apply = false;
            Debug.Log("Updated material val");
            Graphic graphic = GetComponent<Graphic>();
            Material existingGlobalMat = graphic.materialForRendering;
            Material updatedMaterial = new Material(existingGlobalMat);
            updatedMaterial.SetInt("unity_GUIZTestMode", (int)comparison);
            graphic.material = updatedMaterial;

        }
    }
}
