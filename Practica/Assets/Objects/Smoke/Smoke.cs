using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Die(){
     //play your sound
     yield return new WaitForSeconds(0.5f); //waits 3 seconds
     Destroy(gameObject); //this will work after 3 seconds.
 }
	void Start()
    {
	StartCoroutine(Die());
    }
	
	
    // Update is called once per frame
    void Update()
    {
	
        
	

    }
}
