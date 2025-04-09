using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCaseCam : MonoBehaviour
{

    float spd = 50f;
    Rigidbody rb;

    private void Start()
    {
        rb= GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Move();
        Turn();
    }

    private void Move()
    {

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("running");
            rb.velocity = spd * transform.right;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -spd * transform.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = spd * transform.forward;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = -spd * transform.forward;
        }
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
    }
}
