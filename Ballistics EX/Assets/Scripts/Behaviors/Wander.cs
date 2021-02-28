using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face
{
    public float wanderOffset;
    public float wanderRadius;
    public float wanderRate;
    public float wanderOrientation;
    public float maxAcceleration;

    public override SteeringOutput getSteering()
    {
        wanderOrientation += Random.Range(-wanderRate, wanderRate);
        float targetOrientation = wanderOrientation + character.transform.eulerAngles.y;

        Vector3 orientationVector = new Vector3(Mathf.Sin(character.transform.eulerAngles.y * Mathf.Deg2Rad), 0, Mathf.Cos(character.transform.eulerAngles.y * Mathf.Deg2Rad));
        Vector3 targetOVector = new Vector3(Mathf.Sin(targetOrientation * Mathf.Deg2Rad), 0, Mathf.Cos(targetOrientation * Mathf.Deg2Rad));

        target.transform.position = character.transform.position + wanderOffset * orientationVector;
        target.transform.position += wanderRadius * targetOVector;

        SteeringOutput result = new SteeringOutput();

        result.angular = getTargetAngle();
        result.linear = maxAcceleration * orientationVector;

        return result;
    }
}
