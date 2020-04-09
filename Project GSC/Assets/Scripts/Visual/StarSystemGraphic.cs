using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Managers;

public class SolarSystemGraphic
{
    public GameObject go;
	public Vector3 Pos { get; set; }
	public SolarSystemGraphic(string name, Vector3 pos)
	{
		Pos = pos;
		go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		go.transform.position = Pos;
		go.name = name;
	}

	public void Destroy()
	{
		Object.Destroy(go);
	}
}
