using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//GAME OBJECT NOTES:
    //* collider is trigger
    //* 2 empty objects as waypoints; both children of a wall object

public class EnemyMoveWall : MonoBehaviour
{
    enum State { Idle, Explode };

    State state;
    public List<Transform> waypoints;

    [SerializeField] float resize = 3f;
    int counter = 0;
    int explodeTime = 10;
    GameObject player;
    [SerializeField] float detectDist = 10f;
    [SerializeField] float moveSpd = 2f;
    float contactDist = 0.2f;
    Vector3 targetPt;
    int currPt = 0;

    private void Start()
    {
        state = State.Idle;
        player = GameObject.FindWithTag("Player");
        this.GetComponent<BoxCollider>().size = new Vector3(0f, 0f, 0f);
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
            case State.Explode:
                Explode();
                break;
            default:
                IdleMove();
                break;
        }
    }

    private void Update()
    {
        EnemyState();
        if (Vector3.Distance(transform.position, player.transform.position) < detectDist)
        {
            state = State.Explode;
            counter++;
        }
        else
        {
            state = State.Idle;
        }
    }

    void IdleMove()  //detatch children (if going with option 1)
    {
        if (Vector3.Distance(transform.position, targetPt) < contactDist) //might need to add collision detection in case it runs into a wall
        {
            SetPt();
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPt, moveSpd * Time.deltaTime);
        transform.LookAt(targetPt);
        this.GetComponent<BoxCollider>().size = new Vector3(0f, 0f, 0f);
    }

    void Explode()
    {
        if (counter < explodeTime)
        {
            this.GetComponent<BoxCollider>().size = new Vector3(resize, resize, resize);
        }
        else
        {
            counter = 0;
            state = State.Idle;
        }
    }
}
