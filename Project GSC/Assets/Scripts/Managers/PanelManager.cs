using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.GameModels.Astronomy;
using Assets.Scripts.UI.GamePanels;

public class PanelManager : MonoBehaviour
{
    [SerializeField]
    GameObject SSPanelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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

    public void OpenStarPanel(SolarSystem ss)
    {
        var ssPanel = Instantiate(SSPanelPrefab, transform);

        var ssps = ssPanel.GetComponent<SolarSystemPanel>();

        ssps.SetupPlanets(ss);
    }
}
