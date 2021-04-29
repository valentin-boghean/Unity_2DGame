using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Entity
{
	IEnumerator Fire()
	{
     yield return new WaitForSeconds(0.5f); 
	 
	}
    // Start is called before the first frame update
	public float fireRange;
    public GameObject arrow;
	public GameObject smoke;
    private Transform player;
    private float tfire;
	public float reload;
    void Start()
    {
         player=GameObject.Find("ob_Player").transform;
		 tfire= vitezaAtac;
    }

    // Update is called once per frame
    void Update()
    {
        if (viata>0)
       {
            for (reload=5;reload>=0;reload-=Time.deltaTime)
			{
			if (Vector2.Distance(player.position, this.transform.position) <= fireRange)
            {
                if (tfire <= 0)
                {
					//for (int i=0;i<5;i++)
					//StartCoroutine(Fire());
					Instantiate(arrow, transform.position, Quaternion.identity);
                    tfire = vitezaAtac;
                }
                else
                    tfire -= Time.deltaTime;
            }
			}

	   }
       else
	   { 
	        Instantiate(smoke,transform.position,Quaternion.identity);
	        Destroy(gameObject);
	   }
    }
}
