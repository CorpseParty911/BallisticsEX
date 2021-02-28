using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : Kinematic
{
    Seek myMoveType;
    Wander myRotateType;
    public float wanderOffset = 5f;
    public float wanderRadius = 2f;
    public float wanderRate = 1f;
    public float wanderOrientation = 0f;
    public float maxAcceleration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        myRotateType = new Wander();
        myRotateType.character = this;
        myRotateType.target = new GameObject();
        myRotateType.wanderOffset = wanderOffset;
        myRotateType.wanderRadius = wanderRadius;
        myRotateType.wanderRate = wanderRate;
        myRotateType.wanderOrientation = wanderOrientation;
        myRotateType.maxAcceleration = maxAcceleration;

        myMoveType = new Seek();
        myMoveType.character = this;
        myMoveType.target = myRotateType.target; ;
        myMoveType.flee = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }
}
