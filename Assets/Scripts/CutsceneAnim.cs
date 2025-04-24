using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutsceneAnim : MonoBehaviour
{
    public Transform endPt;
    public TMP_Text winTxt;
    float rotSpd = 30f;
    float alpha = 0f;

    private void Update()
    {
        transform.Rotate(transform.right, Time.deltaTime * rotSpd);
        transform.position = Vector3.MoveTowards(transform.position, endPt.position, Time.deltaTime * rotSpd / 5);
        winTxt.color = new Color(1f, 1f, 1f, alpha);
        alpha += 0.001f;
    }
}
