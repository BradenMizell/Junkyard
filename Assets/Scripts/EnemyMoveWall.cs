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
    //Color aimColor;
    //Color shootColor;

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

        //aimColor = new Color(255, 255, 255);
        //shootColor = new Color(255, 0, 0);

        lr.enabled = false;
        lr.SetPosition(0, transform.position);

        aimPt = player.transform.position;
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
        //Gradient grad = new Gradient();
        //float alpha = 1f;

        if (aimCt < aimLen)
        {
            shootCt = 0f;
            //grad.SetKeys(
            //    new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            //    new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0f), new GradientAlphaKey(alpha, 1.0f) }
            //);
            aimPt = player.transform.position;
            lr.SetPosition(1, aimPt);
            aimCt += 1 * Time.deltaTime;

            lr.material.SetColor("white", Color.white);

            //lr.colorGradient = grad;
        }
        else if (shootCt < shootLen){
            Debug.Log("running");
            //grad.SetKeys(
            //    new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            //    new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0f), new GradientAlphaKey(alpha, 1.0f) }
            //);
            RaycastHit hit;
            var ray = new Ray(transform.position, aimPt);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag.Equals("Player"))
                {
                    //deal damage to player
                }
            }
            shootCt += 1 * Time.deltaTime;

            lr.material.SetColor("red", Color.red);

            //lr.colorGradient = grad;
        }
        else
        {
            aimCt = 0f;
        }
        //else
        //{
        //    state = State.Idle;
        //}
    }
}
