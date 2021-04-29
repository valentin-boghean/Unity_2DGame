using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour
{
    // Start is called before the first frame update
    private float length, startpos;     //lungimea si pozitia de start a imaginii
    public GameObject cam; //camera
    public float parallaxefffect; // pentru determinarea efectului Parralax
    void Start()
    {
        startpos = transform.position.x; // pozitia de start
        length = GetComponent<SpriteRenderer>().bounds.size.x; // lungimea pe x a imaginii
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxefffect)); // cu cat ar trebui sa se mute relativ la camera
        float dist = (cam.transform.position.x * parallaxefffect);  // pentru calculare cu cat ar trebui sa se mute pe x in functie de cat parallax effect aplicam
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);  //mutam obiecutl

        if (temp > startpos + length)
            startpos += length;
        else if (temp < startpos - length) 
            startpos -= length;
    }
}
