using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Scripts.GameModels.Astronomy;

[Serializable]
public class SolarSystemData
{
    public string name;
    public int index;

    public int[] linkedSystems;

    public float[] position;

    public SolarSystemData(SolarSystem ss)
    {
        name = ss.Name;

        index = ss.Index;

        linkedSystems = new int[ss.linkedSystemIndex.Count];
        for (int i = 0; i < ss.linkedSystemIndex.Count; i++)
        {
            linkedSystems[i] = ss.linkedSystemIndex[i];
        }

        position = new float[3];
        position[0] = ss.Position.x;
        position[1] = ss.Position.y;
        position[2] = ss.Position.z;

    }
}
