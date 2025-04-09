using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GAME OBJECT NOTES:
    //* collider is trigger
    //* has rigidbody (gravity off)
    //* 2 empty objects as waypoints
    //* must have obj in scene tagged as Player


public class EnemyMoveChase : MonoBehaviour
{
    enum State { Idle, Chase };
    State state;
    GameObject player;
    public List<Transform> waypoints;
    Rigidbody rb;

    [SerializeField] float detectDist = 10f;
    [SerializeField] float moveSpd = 3f;
    float contactDist = 1f;
    Vector3 targetPt;
    int currPt = 0;

    private void Start()
    {
        foreach (Transform pt in waypoints)
        {
            pt.parent = null;
        }
        state = State.Idle;
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        SetPt();
    }

    void SetPt()
    {
        targetPt = waypoints[currPt].transform.position;
        currPt = (currPt == 0) ? 1 : 0;
    }

    void EnemyState()
    {
        switch (state)
        {
            case State.Chase:
                ChasePlayer();
                break;
            default:
                IdleMove();
                break;
        }
    }

    private void Update()
    {
        EnemyState();
        if (Vector3.Distance(transform.position, player.transform.position) <= detectDist){
            state = State.Chase;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) >= detectDist * 1.5) //so player doesn't escape immediately; more room where it follows than init detect
        {
            state = State.Idle;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPt, moveSpd * Time.deltaTime);
        transform.LookAt(targetPt);
        Debug.Log(currPt);
    }

    void ChasePlayer()  //reattach children (if going with option 1)
    {
        targetPt = player.transform.position;
        if (Vector3.Distance(transform.position, player.transform.position) < contactDist)
        {
            rb.AddForce(transform.forward * -1 * moveSpd);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > detectDist / 2f)
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
        }
    }

    void IdleMove()  //detatch children (if going with option 1)
    {
        if (Vector3.Distance(transform.position, targetPt) <= contactDist) //might need to add collision detection in case it runs into a wall
        {
            SetPt();
        }
    }
}
