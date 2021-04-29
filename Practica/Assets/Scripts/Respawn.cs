using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Transform player;
    [SerializeField] public Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        player.transform.position = respawnPoint.transform.position;
            Physics2D.SyncTransforms();
        
    }

}
