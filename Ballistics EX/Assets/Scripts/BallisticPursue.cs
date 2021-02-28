using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticPursue : MonoBehaviour
{
    public GameObject shotTarget;
    public GameObject target;
    public bool shooting;
    public float velocity = 10f;
    public float shotTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (shooting)
            {
                Invoke("HitTarget", 0.5f);
            }
            else
            {
                this.GetComponent<Rigidbody>().velocity = calculateFiringSolution(this.transform.position, shotTarget, velocity, Physics.gravity) * velocity;
                shooting = !shooting;
            }
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

    void HitTarget()
    {
        Vector3 targetPos = target.transform.position + target.GetComponent<Rigidbody>().velocity * shotTime + Physics.gravity * shotTime * shotTime / 2;
        this.GetComponent<Rigidbody>().velocity = (targetPos - this.transform.position - Physics.gravity * shotTime * shotTime / 2) / shotTime;

        shooting = !shooting;
    }
}
