using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCaseCam : MonoBehaviour
{

    float spd = 10f;
    private void Update()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        float hor = 0f;
        float forw = 0f;
        float ver = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("running");
            hor = spd;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            hor = -spd;
        }
        if (Input.GetKey(KeyCode.W))
        {
            forw = spd;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            forw = -spd;
        }
        if (Input.GetKey(KeyCode.I))
        {
            ver = spd;
        }
        else if (Input.GetKey(KeyCode.K))
        {
            ver = -spd;
        }
        transform.right += new Vector3(hor, 0f, 0f);
        transform.forward += new Vector3(0f, 0f, forw);
        transform.right += new Vector3(0f, ver, 0f);
    }

    void Turn()
    {
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * spd, Space.World);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * spd, Space.World);
        }
        if (Input.GetKey(KeyCode.U))
        {
            transform.Rotate(new Vector3(1, 0, 0) * Time.deltaTime * spd, Space.World);
        }
        else if (Input.GetKey(KeyCode.J))
        {
            transform.Rotate(new Vector3(-1, 0, 0) * Time.deltaTime * spd, Space.World);
        }
    }
}
