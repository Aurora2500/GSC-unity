using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLink : MonoBehaviour
{

    public LineRenderer line;

    public int startIndex;
    public int endIndex;

    public Vector3 Start { get; private set; }
    public Vector3 End { get; private set; }
    public float Distance
    {
        get
        {
            return (End - Start).magnitude;
        }
    }

    public void Setup(Vector3 _start, int _startIndex, Vector3 _end, int _endIndex)
    {
        Start = _start;
        End = _end;

        startIndex = _startIndex;
        endIndex = _endIndex;

        transform.position = Start;
        line.SetPosition(1, End - Start);
    }
}
