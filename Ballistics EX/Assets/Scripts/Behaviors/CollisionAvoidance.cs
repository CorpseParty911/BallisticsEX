using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : SteeringBehavior
{
    public Kinematic character;
    public float maxAcceleration = 1f;

    public Kinematic[] targets;

    float radius = 2f;

    public override SteeringOutput getSteering()
    {
        float shortestTime = float.PositiveInfinity;
        Kinematic firstTarget = null;
        float firstMinSeparation = float.PositiveInfinity;
        float firstDistance = float.PositiveInfinity;
        Vector3 firstRelativePos = Vector3.positiveInfinity;
        Vector3 firstRelativeVel = Vector3.zero;

        Vector3 relativePos = Vector3.positiveInfinity;
        foreach (Kinematic target in targets)
        {
            relativePos = target.transform.position - character.transform.position;
            Vector3 relativeVel = character.linearVelocity - target.linearVelocity;
            float relativeSpeed = relativeVel.magnitude;
            float timeToCollision = (Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed));

            float distance = relativePos.magnitude;
            float minSeparation = distance - relativeSpeed * timeToCollision;
            if (minSeparation > 2 * radius)
            {
                continue;
            }
            if (timeToCollision > 0 && timeToCollision < shortestTime)
            {
                shortestTime = timeToCollision;
                firstTarget = target;
                firstMinSeparation = minSeparation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;
            }
        }

        if (firstTarget == null)
        {
            return null;
        }

        SteeringOutput result = new SteeringOutput();

        result.linear = firstTarget.linearVelocity;
        result.angular = 0;
        return result;

        /*
        float dotResult = Vector3.Dot(character.linearVelocity.normalized, firstTarget.linearVelocity.normalized);
        if (dotResult < -0.9)
        {
            result.linear = -firstTarget.transform.right;
        }
        else
        {
            result.linear = -firstTarget.linearVelocity;
        }
        result.linear.Normalize();
        result.linear *= maxAcceleration;
        result.angular = 0;
        return result;
        */
        /*
        if (firstMinSeparation <= 0 || firstDistance < 2*radius)
        {
            relativePos = firstTarget.transform.position - character.transform.position;
        }
        else
        {
            relativePos = firstRelativePos + firstRelativeVel * shortestTime;
        }

        SteeringOutput result = new SteeringOutput();
        relativePos.Normalize();
        result.linear = relativePos * maxAcceleration;
        result.angular = 0;
        return result;
        */
    }
}
