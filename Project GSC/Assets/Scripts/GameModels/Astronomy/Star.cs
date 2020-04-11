using Assets.Scripts.GameModels.Astronomy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star
{

    public SolarSystem System { get;  private set; }

    public Star(SolarSystem ss)
    {
        System = ss;
    }
}
