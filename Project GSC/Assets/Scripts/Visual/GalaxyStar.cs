using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.GameModels.Astronomy;

public class GalaxyStar : MonoBehaviour
{
    public GameObject canvas;
    Camera mainCamera;
    public TextMeshProUGUI nameText;
    public SolarSystem solarSystem;

    // Start is called before the first frame update
    void Start()
    {
        var canvascomp = canvas.GetComponent<Canvas>();
        mainCamera = Camera.main;
        canvascomp.worldCamera = mainCamera;
        canvas.transform.SetAsLastSibling();
    }

    public void UpdateText()
    {
        nameText.text = name;
    }

    void LateUpdate()
    {
        canvas.transform.LookAt(canvas.transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);
    }
}
