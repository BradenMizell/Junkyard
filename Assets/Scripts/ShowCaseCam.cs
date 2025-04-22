using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCaseCam : MonoBehaviour
{

    [SerializeField] float spd = 10f;
    private void Update()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.forward * -spd * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(transform.forward * spd * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(transform.right * spd * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(transform.right * -spd * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.I))
        {
            transform.Translate(transform.up * spd * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.J))
        {
            transform.Translate(transform.up * -spd * Time.deltaTime, Space.World);
        }
        
    }

    void Turn()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * spd * 5, Space.World);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * spd * 5, Space.World);
        }
    }
}
