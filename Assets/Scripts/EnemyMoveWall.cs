using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GAME OBJECT NOTES:
    //* collider is trigger
    //* 2 empty objects as waypoints; both children of a wall object

public class EnemyMoveWall : MonoBehaviour
{
    enum State{Idle, Attack};
    State state;
    public List<Transform> waypoints;
    GameObject player;
    LineRenderer lr;

    [SerializeField] float detectDist = 10f;
    [SerializeField] float moveSpd = 2f;
    float contactDist = 0.2f;
    Vector3 targetPt;
    int currPt = 0;

    float aimCt = 0;
    float shootCt = 0;
    float aimLen = 1;
    float shootLen = 0.5f;
    Vector3 aimPt;
    [SerializeField] LayerMask lm;

    private void Start()
    {
        foreach (Transform pt in waypoints)
        {
            pt.parent = null;
        }
        SetPt();
        state = State.Idle;
        player = GameObject.FindWithTag("Player");
        lr = GetComponent<LineRenderer>();

        lr.enabled = false;
        lr.SetPosition(0, transform.position);

        aimPt = player.transform.position;
        //lm = LayerMask.GetMask("PlayerRead");
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
            case State.Attack:
                AttackPlayer();
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
            state = State.Attack;
        }
        else
        {
            state = State.Idle;
        }
    }

    void IdleMove()  //detatch children (if going with option 1)
    {
        aimCt = 0;
        shootCt = 0;
        lr.enabled = false;
        if (Vector3.Distance(transform.position, targetPt) < contactDist) //might need to add collision detection in case it runs into a wall
        {
            SetPt();
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPt, moveSpd * Time.deltaTime);
        transform.LookAt(targetPt);
    }

    void AttackPlayer()
    {
        //shoot player; line renderer lazer? change color dep on whether it's prepping or shooting
        lr.enabled = true;
        lr.SetPosition(0, transform.position);

        if (aimCt < aimLen)
        {
            lr.material.SetColor("_Color", Color.white);

            shootCt = 0f;
            aimPt = player.transform.position;
            lr.SetPosition(1, aimPt);
            aimCt += 1 * Time.deltaTime;
        }
        else if (shootCt < shootLen){
            lr.material.SetColor("_Color", Color.red);

            //having trouble getting it to read player object
            var ray = new Ray(transform.position, aimPt);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, lm))
            {
                Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.gameObject == player)
                {
                    Debug.Log("hit!");
                    //deal damage to player
                }
            }
            shootCt += 1 * Time.deltaTime;
        }
        else
        {
            aimCt = 0f;
        }
    }
}
