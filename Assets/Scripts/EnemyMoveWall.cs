using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GAME OBJECT NOTES:
    //* collider is trigger
    //* 2 empty objects as waypoints; both children of a wall object

public class EnemyMoveWall : MonoBehaviour
{
    public List<Transform> waypoints;

    [SerializeField] float moveSpd = 2f;
    float contactDist = 0.2f;
    Vector3 targetPt;
    int currPt = 0;

    private void Start()
    {
        SetPt();
    }

    void SetPt()
    {
        targetPt = waypoints[currPt].transform.position;
        currPt = (currPt == 0) ? 1 : 0;
    }

    private void Update()
    {
        IdleMove();
        transform.position = Vector3.MoveTowards(transform.position, targetPt, moveSpd * Time.deltaTime);
        transform.LookAt(targetPt);
    }

    void IdleMove()  //detatch children (if going with option 1)
    {
        if (Vector3.Distance(transform.position, targetPt) < contactDist) //might need to add collision detection in case it runs into a wall
        {
            SetPt();
        }
    }
}
