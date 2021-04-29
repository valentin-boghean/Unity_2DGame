using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpecialArrowV2 : MonoBehaviour
{
	//public float arrowSpeed = 10;
	public float acurracyAndSpeed = 8f;
	public int damage = 2;


	private Transform player;
	private UnityEngine.Vector2 target;

	private Rigidbody2D body;

	int timeToDie = 2;
	float countDown = 0;



	// Start is called before the first frame update
	void Start()
	{
		UpdateAngle();
		player = GameObject.Find("ob_Player").transform;
		target = new UnityEngine.Vector2(this.transform.position.x, this.transform.position.y);

		body = this.GetComponent<Rigidbody2D>();
		body.velocity = CalculateVelocity(new UnityEngine.Vector3(this.transform.position.x - 1, this.transform.position.y + 10));

		countDown = 0;
	}

	// Update is called once per frame
	void Update()
	{


		if (countDown >= timeToDie)
		{
			Destroy(gameObject);
		}
		countDown += Time.deltaTime;
		//this.transform.position = new UnityEngine.Vector2(this.transform.position.x, this.transform.position.y + 0.5);
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

		transform.eulerAngles = UnityEngine.Vector3.forward * 270;
	}
}
