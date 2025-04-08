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

    int aimCt = 0;
    int shootCt = 0;
    int aimLen = 40;
    int shootLen = 10;
    Color aimColor;
    Color shootColor;

    private void Start()
    {
        transform.DetachChildren();
        SetPt();
        state = State.Idle;
        player = GameObject.FindWithTag("Player");
        lr = GetComponent<LineRenderer>();

        aimColor = new Color(0, 0, 0);
        shootColor = new Color(0, 0, 0);

        lr.enabled = false;
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
                aimCt = 0;
                shootCt = 0;
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
        lr.SetColors(aimColor, aimColor);
        Vector3 aimPt = player.transform.position;
        if (aimCt < aimLen)
        {
            aimPt = player.transform.position;
            lr.SetPosition(1, aimPt);
            aimCt++;
        }
        else
        {
            if (shootCt < shootLen) //idea is that it freezes after aiming; if it happens to hit player, causes damage
            {
                lr.SetColors(shootColor, shootColor);
                RaycastHit hit;
                var ray = new Ray(transform.position, aimPt);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag.Equals("Player"))
                    {
                        //deal damage to player
                    }
                }
                shootCt++;
            }
            else
            {
                state = State.Idle;
            }
        }
    }
}
