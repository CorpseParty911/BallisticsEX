using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : Seek
{
    public GameObject[] path;

    public int currentPathIndex = 0;

    float targetRadius = 0.5f;

    public override SteeringOutput getSteering()
    {
        if ((target.transform.position - character.transform.position).magnitude < targetRadius)
        {
            currentPathIndex++;
            currentPathIndex %= path.Length;
            target = path[currentPathIndex];
        }

        return base.getSteering();
    }
}
