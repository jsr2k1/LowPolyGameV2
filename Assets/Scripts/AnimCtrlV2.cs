using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimCtrlV2 : MonoBehaviour
{
    public enum States
    {
        IDLE,
        WALK
    }
    public Transform target;
    public float distanceThreshold = 0.01f;
    public float speed = 1;
    
    public States currentState;
    public Animator animator;
    public string sleep = "isSleeping";
    public string walk = "isWalking";

    public float time_sleep = 4f;
    public float time_stand = 12f;
    public float time_walk_1 = 4f;
    public float time_walk_2 = 0.25f;

    public Transform[] points;

    Vector3 dir;
    int destPoint = 0;
    float remainingDistance;

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        currentState = States.IDLE;
        StartCoroutine(GoToSleep());
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if(currentState == States.WALK)
        { 
            remainingDistance = Vector3.Distance(transform.position, points[destPoint].position);
            if(remainingDistance < distanceThreshold) {
                animator.SetBool(walk, false);
                currentState = States.IDLE;
                StartCoroutine(GoToSleep());
            }
            else {
                transform.Translate(dir * Time.deltaTime * speed * 0.01f);
            }
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator GoToSleep()
    {
        yield return new WaitForSeconds(time_sleep);
        animator.SetBool(sleep, true);
        StartCoroutine(GoToStandUp());
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator GoToStandUp()
    {
        yield return new WaitForSeconds(time_stand);
        animator.SetBool(sleep, false);
        StartCoroutine(GoToWalk());
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator GoToWalk()
    {
        yield return new WaitForSeconds(time_walk_1);
        animator.SetBool(walk, true);
        yield return new WaitForSeconds(time_walk_2);
        currentState = States.WALK;
        GotoNextPoint();
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void GotoNextPoint()
    {
        if(points.Length == 0) {
            return;
        }
        destPoint = (destPoint + 1) % points.Length;
        dir = (points[destPoint].position - transform.position).normalized;
        target.transform.LookAt(points[destPoint].position);
    }
}
