using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn_zone : MonoBehaviour
{
    public Transform respawn;
    public GameObject Player;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag.Equals("PlayerObj"))
        {
            Vector3 respawnPos = new Vector3(respawn.transform.position.x, respawn.transform.position.y, respawn.transform.position.z);
            Player.transform.position = respawnPos;
        }
    }
}
