using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject arrow;
    private Transform player;
    private float difX;
    private float difY;
    void Start()
    {
        difX = -8;
        difY = 3;
        player = GameObject.Find("ob_Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0;i<16;i++)
        {
            Instantiate(arrow,new Vector3(player.transform.position.x - difX, player.transform.position.y+difY) ,Quaternion.identity);
            difX+=2;
        }
        Destroy(this.gameObject);
    }
}
