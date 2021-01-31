using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolV2 : MonoBehaviour
{
    public Transform target;
    public Transform[] points;
    public float distanceThreshold = 0.5f;
    public float speed = 1;

    Vector3 dir;
    int destPoint = -1;
    float remainingDistance;

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        GotoNextPoint();
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void GotoNextPoint()
    {
        if(points.Length == 0)
            return;
        
        destPoint = (destPoint + 1) % points.Length;
        dir = (points[destPoint].position - transform.position).normalized;
        target.transform.LookAt(points[destPoint].position);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        remainingDistance = Vector3.Distance(transform.position, points[destPoint].position);

        if(remainingDistance > distanceThreshold) {
            transform.Translate(dir * Time.deltaTime * speed * 0.01f);
        }
        else {
            GotoNextPoint();
        }
    }
}
