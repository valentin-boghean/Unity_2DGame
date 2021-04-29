using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Entity
{

    public float fireRange;
    public GameObject arrow;
	public GameObject smoke;
    private Transform player;
    public Animator animator;
    private Vector3 zonaFaraAtac;
    void Start()
    {
        player=GameObject.Find("ob_Player").transform;

        animator.SetFloat("AttackSpeed", vitezaAtac);

        //Cerc Under
        zonaFaraAtac = new Vector3(transform.position.x + 2, transform.position.y - 10, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
       if (viata>0)
       {
            if (Vector2.Distance(player.position, this.transform.position) <= fireRange && CheckArea()!=3)
            {
                animator.SetBool("inRange", true);
            }
            else
            {
                animator.SetBool("inRange", false);
            }



            CheckDir();
        }
       else
	   { 
	        Instantiate(smoke,transform.position,Quaternion.identity);
	        Destroy(gameObject);
	   }
    }
    public void Shot()
    {
                Instantiate(arrow, transform.position, Quaternion.identity);
    }
  
}
