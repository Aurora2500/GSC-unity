using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            KillAllPanels();
        }
    }

    public void KillAllPanels()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
