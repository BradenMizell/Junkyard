using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://gist.github.com/KarlRamstedt/407d50725c7b6abeaf43aee802fdd88e

public class PlayerScript : MonoBehaviour
{
	public Graphic crosshair;
	Color baseColor;
	Color aimColor;
	Vector3 nextPt;
	GameObject mainCam;

	public float sensitivity;
	float yRotationLimit = 90f;

	Vector2 rotation = Vector2.zero;
	string mouseX = "Mouse X";
	string mouseY = "Mouse Y";

	KeyCode grappleKey;
	bool grappling = false;
	bool canGrapple = false;
	public float grappleSpd;

	void Start()
    {
		baseColor = crosshair.color;
		aimColor = new Color(0f, 1f, 0f, 0.5f);

		mainCam = transform.GetChild(0).gameObject;

		grappleKey = KeyCode.Mouse0;
    }

	void Update()
	{
		if (!grappling)
        {
			CameraRotate();
			CheckAim();
			if (canGrapple && Input.GetKeyDown(grappleKey))
            {
				grappling = true;
            }
		}
        else
        {
			Jump();
        }
		
	}

	void CameraRotate() //got help online for this one
    {
		rotation.x += Input.GetAxis(mouseX) * sensitivity;
		rotation.y += Input.GetAxis(mouseY) * sensitivity;
		rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit); //value, min, max
		var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up); //sets angle around an axis
		var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

		mainCam.transform.localRotation = xQuat * yQuat;
	}

	void CheckAim()
    {
		RaycastHit hit;
		if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, Mathf.Infinity))
        {
			if (hit.transform.gameObject.tag.Equals("Plat"))
            {
				crosshair.color = aimColor;
				nextPt = hit.transform.position;
				canGrapple = true;
            }
            else
            {
				crosshair.color = baseColor;
				canGrapple = false;
            }
        }
        else
		{
			crosshair.color = baseColor;
			canGrapple = false;
		}
    }

	void Jump()
    {
		Debug.Log("jUMP");
		transform.position = Vector3.MoveTowards(transform.position, nextPt, Time.deltaTime * grappleSpd);
		if (Vector3.Distance(transform.position, nextPt) < 0.2f)
        {
			grappling = false;
        }
    }
}
