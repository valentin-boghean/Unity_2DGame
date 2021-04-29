using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : Entity
{
	public float speed=2;
	public int attack = 2;
	private float cooldown = 0;
	private Transform player;
	public GameObject smoke;

	public Animator animator;

	//bool move=false;
	bool close = false;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("ob_Player").transform;


	}

    // Update is called once per frame
    void Update()
    {
		CheckPlayer();
		if (viata>0)
		{

			float dist = Vector2.Distance(player.transform.position, this.transform.position);
			if (dist<14f && close == false)
            {
				transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
				animator.SetBool("move", true);
				//move = true;
			}
			else
            {
				animator.SetBool("move", false);
				//move = false;
				
            }
			if (dist < 14f && player.GetComponent<Player>().shield == false)
			{
				close = false;
			}

			CheckDir();
		}
		else
		{ 
			Instantiate(smoke,transform.position,Quaternion.identity);
			Destroy(gameObject);
		}
    }
    private void OnCollisionStay2D(Collision2D other)
    {
		if (other.gameObject.name == ("ob_Player"))
		{
			Attack(other.gameObject);
		}
	}
	bool CheckPlayer()
    {
		Vector2 offset = new Vector2(-0.5f, 0);
		//float offset = 2f;
		BoxCollider2D collider = this.gameObject.GetComponent<BoxCollider2D>();
		RaycastHit2D colR = Physics2D.Raycast(collider.bounds.center + new Vector3(0.3f, 0, 0), UnityEngine.Vector2.right);
		Debug.DrawRay(collider.bounds.center + new Vector3(0.3f, 0, 0), UnityEngine.Vector2.right);
		RaycastHit2D colL = Physics2D.Raycast(collider.bounds.center+ new Vector3(-0.3f,0,0), UnityEngine.Vector2.left);
		Debug.DrawRay(collider.bounds.center + new Vector3(-0.3f, 0, 0), UnityEngine.Vector2.left);

		if (colR.transform!=null)
        {
			if (colR.transform.gameObject.name =="ob_Player")
            {
				Attack(colR.transform.gameObject);
				return true;
            }
        }
		if (colL.transform != null)
        {
			if (colL.transform.gameObject.name == "ob_Player")
			{
				Attack(colL.transform.gameObject);
				return true;
			}
		}
		return false;
	}

    private void OnCollisionExit2D(Collision2D other)
	{
		//float dist = Vector2.Distance(player.transform.position, this.transform.position);


		if (other.gameObject.name == ("ob_Player"))
		{
			//cooldown = 0;
			close = false;
		}
	}

	void Attack(GameObject other)
    {
		float dist = Vector2.Distance(player.transform.position, this.transform.position);
		if (dist<1.6f)
        {
			if (cooldown < 0 && other.gameObject.GetComponent<Player>().shield == false)
			{
				cooldown = vitezaAtac;
				other.gameObject.GetComponent<Player>().viata -= attack;
				other.gameObject.GetComponent<Heathbar>().SetHealthBar(other.gameObject.GetComponent<Player>().viata);
				//StartCoroutine(other.gameObject.GetComponent<Heathbar>().Flick());
				other.gameObject.GetComponent<Heathbar>().StartFlick();
				StartCoroutine(Camera.main.GetComponentInChildren<CameraShake>().Shake(0.5f, 0.25f));

				animator.SetBool("attack", true);
			}
			else if (cooldown < 0 && other.gameObject.GetComponent<Player>().shield == true)
			{
				cooldown = vitezaAtac;
				//other.gameObject.GetComponent<Player>().viata -= attack;
				//other.gameObject.GetComponent<Heathbar>().SetHealthBar(other.gameObject.GetComponent<Player>().viata);
				//StartCoroutine(other.gameObject.GetComponent<Heathbar>().Flick());
				//other.gameObject.GetComponent<Heathbar>().StartFlick();
				//StartCoroutine(Camera.main.GetComponentInChildren<CameraShake>().Shake(0.5f, 0.25f));

				animator.SetBool("attack", true);
			}
			else if (cooldown < 0 && CheckPlayer() == true)
			{
				cooldown = vitezaAtac;
				//other.gameObject.GetComponent<Player>().viata -= attack;
				//other.gameObject.GetComponent<Heathbar>().SetHealthBar(other.gameObject.GetComponent<Player>().viata);
				//StartCoroutine(other.gameObject.GetComponent<Heathbar>().Flick());
				//other.gameObject.GetComponent<Heathbar>().StartFlick();
				//StartCoroutine(Camera.main.GetComponentInChildren<CameraShake>().Shake(0.5f, 0.25f));

				animator.SetBool("attack", true);
			}
			else
			{
				animator.SetBool("attack", false);
			}

			if (other.gameObject.GetComponent<Player>().shield == true)
			{
				close = true;
			}
			else
			{
				close = false;
			}
		}
		
		cooldown -= Time.deltaTime;
	}
}