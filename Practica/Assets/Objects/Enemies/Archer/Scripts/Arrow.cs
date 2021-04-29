using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Arrow : MonoBehaviour
{
	//public float arrowSpeed = 10;
	public float acurracyAndSpeed = 8f;
	public int damage = 2;


	private Transform player;
	private UnityEngine.Vector2 target;

	private Rigidbody2D body;


	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.Find("ob_Player").transform;
		target = new UnityEngine.Vector2(player.position.x, player.position.y);

		body = this.GetComponent<Rigidbody2D>();
		body.velocity = CalculateVelocity(player.position);

	}

	// Update is called once per frame
	void Update()
	{
		UpdateAngle();

		if (transform.position.x == target.x && transform.position.y == target.y)
		{
			Destroy(gameObject);
		}

	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == ("ob_Player"))
		{
			Destroy(gameObject);
		}

	}

	UnityEngine.Vector2 CalculateVelocity(UnityEngine.Vector3 target)
	{
		UnityEngine.Vector3 distance = target - this.transform.position;

		float distanceX = distance.x;
		float distanceY = distance.y;

		//Debug.Log(distanceX);
		float betterTime = (Mathf.Abs(distanceX) - Mathf.Abs(distanceY)) / acurracyAndSpeed;
		float Vx = distanceX / betterTime;
		float Vy = distanceY / betterTime + (0.5f * Mathf.Abs(Physics.gravity.y) * betterTime);

		return new UnityEngine.Vector2(Vx, Vy);
	}

	void UpdateAngle()
	{
		float angle = Mathf.Atan2(body.velocity.y, body.velocity.x) * Mathf.Rad2Deg;
		transform.rotation = UnityEngine.Quaternion.AngleAxis(angle, UnityEngine.Vector3.forward);
	}
}
