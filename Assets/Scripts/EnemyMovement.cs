using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//source: Roundbeargames on YouTube; #12 Bezier Curve - Unity Tutorial - Devlog
//https://denisrizov.com/2016/06/02/bezier-curves-unity-package-included/
public class EnemyMovement : MonoBehaviour
{
    public Transform[] points;
    Vector3 a, b;

    [SerializeField] float moveSpd = 0.3f;
    float timeCt = 0f;

    bool forward = true;

    private void Start()
    {
        foreach (Transform pt in points)
        {
            pt.parent = null;
        }

        a = points[0].position;
        b = points[1].position;

        transform.position = a;
    }

    Vector3 GetCurve(Vector3 p0, Vector3 p1, Vector3 p2, float time)
    {
        float tt = time * time;

        float u = 1f - time;
        float uu = u * u;

        //calculates the point the obj will be at; does not work with Time.deltaTime, must be a regular float. For 4+ points at a time, needs modified formula and more parameters
        Vector3 result = (uu * p0) + (2f * u * time * p1) + (tt * p2);

        DrawLines(p0, p1, p2, time);

        return result;
    }

    void DrawLines(Vector3 p0, Vector3 p1, Vector3 p2, float time)
    {
        Debug.DrawLine(p0, p1, Color.green);
        Debug.DrawLine(p1, p2, Color.green);

        a = Vector3.Lerp(p0, p1, time);
        b = Vector3.Lerp(p1, p2, time);

        Debug.DrawLine(a, b, Color.yellow);
    }


    private void Update()
    {
        if (timeCt < 1f)
        {
            if (forward)
            {
                transform.LookAt(points[2].position);
                transform.position = GetCurve(points[0].position, points[1].position, points[2].position, timeCt);
            }
            else
            {
                transform.LookAt(points[0].position);
                transform.position = GetCurve(points[2].position, points[1].position, points[0].position, timeCt);
            }
            timeCt += moveSpd * Time.deltaTime;
        }
        else
        {
            forward = (forward) ? false : true;
            timeCt = 0;
        }
    }
}