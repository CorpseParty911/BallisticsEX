using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticPath : MonoBehaviour
{
    public float velocity;
    public GameObject[] targets;
    bool primed = false;
    bool forward = true;
    int index = 0;

    private void Start()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if ((this.transform.position - targets[i].transform.position).magnitude < (this.transform.position - targets[index].transform.position).magnitude)
            {
                index = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (primed)
        {
            this.GetComponent<Rigidbody>().velocity = calculateFiringSolution(this.transform.position, targets[index], velocity, Physics.gravity) * velocity;
            primed = false;
        }
    }

    Vector3 calculateFiringSolution(Vector3 start, GameObject target, float velocity, Vector3 gravity)
    {
        Vector3 delta = target.transform.position - start;

        float a = gravity.sqrMagnitude;
        float b = -4 * (Vector3.Dot(gravity, delta) + velocity * velocity);
        float c = 4 * delta.sqrMagnitude;

        float root = b * b - 4 * a * c;

        if (root < 0)
            return Vector3.zero;

        float time0 = Mathf.Sqrt((-b + Mathf.Sqrt(root)) / (2 * a));
        float time1 = Mathf.Sqrt((-b - Mathf.Sqrt(root)) / (2 * a));

        float finalTime;

        if (time0 < 0)
        {
            if (time1 < 0)
                return Vector3.zero;
            else
                finalTime = time1;
        }
        else
        {
            if (time1 < 0)
                finalTime = time0;
            else
                finalTime = Mathf.Max(time0, time1);
        }

        return (delta * 2 - gravity * (finalTime * finalTime)) / (2 * velocity * finalTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        primed = true;
        /*if (index < targets.Length - 1 && forward)
        {
            ++index;
            if (index >= targets.Length - 1)
                forward = false;
        }
        else if (index > 0 && !forward)
        {
            --index;
            if (index <= 0)
                forward = true;
        }*/
        index = ++index % targets.Length;
    }
}
