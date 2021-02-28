using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : Kinematic
{
    PathFollow myMoveType;
    Face myRotateType;

    public GameObject[] path;

    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new PathFollow();
        myMoveType.character = this;
        myMoveType.path = path;
        myMoveType.target = path[0];
        for (int i = 1; i < path.Length; i++)
        {
            if ((this.transform.position - path[i].transform.position).magnitude < (this.transform.position - myMoveType.target.transform.position).magnitude)
            {
                myMoveType.target = path[i];
                myMoveType.currentPathIndex = i;
            }
        }

        myRotateType = new Face();
        myRotateType.character = this;
        myRotateType.target = myTarget;
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
