using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    float detectDist = 30f;
    float moveSpd = 8f;
    float contactDist = 0.5f;
    Vector3 targetPt;
    int currPt = 0;

    //bool inKnockback = false;

    private void Start()
    {
        foreach (Transform pt in waypoints)
        {
            pt.parent = null;
        }
        state = State.Idle;
        player = GameObject.FindWithTag("PlayerObj");
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
        else if (Vector3.Distance(transform.position, player.transform.position) >= detectDist + 1f) //so player doesn't escape immediately; more room where it follows than init detect
        {
            state = State.Idle;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPt, moveSpd * Time.deltaTime);
        transform.LookAt(targetPt);
    }

    void ChasePlayer()  //reattach children (if going with option 1)
    {
        targetPt = player.transform.position;
        if (Vector3.Distance(transform.position, player.transform.position) < contactDist)
        {
            //inKnockback = true;
            //rb.AddForce(transform.forward * -1f * moveSpd);
            moveSpd = -moveSpd;
            bool dies = false;
            dies = player.GetComponent<PlayerMovement>().GotHit(false);
            gameObject.SetActive(!dies);
        }
        //else if (inKnockback && Vector3.Distance(transform.position, player.transform.position) < contactDist * 2f)
        //{
        //    return;
        //}
        else if (Vector3.Distance(transform.position, player.transform.position) > detectDist / 2f)
        {
            //inKnockback = false;
            rb.velocity = new Vector3(0f, 0f, 0f);
            if (moveSpd < 1)
            {
                moveSpd = -moveSpd;
            }
        }
    }

    void IdleMove()  //detatch children (if going with option 1)
    {
        if (Vector3.Distance(transform.position, targetPt) <= contactDist) //might need to add collision detection in case it runs into a wall
        {
            SetPt();
        }
    }


    private void OnTriggerEnter(Collider col)
    {
        bool dies = false;
        if (col.gameObject.tag.Equals("PlayerObj"))
        {
            dies = col.gameObject.GetComponent<PlayerMovement>().GotHit(false);
        }
        gameObject.SetActive(!dies);
    }
}
