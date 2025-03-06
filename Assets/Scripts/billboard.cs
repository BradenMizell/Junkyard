using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    public Camera mainCamera;

    private void LateUpdate()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.y = transform.position.y;
        transform.LookAt(cameraPosition);
        transform.Rotate(0f, 180f, 0f);

    }
}
