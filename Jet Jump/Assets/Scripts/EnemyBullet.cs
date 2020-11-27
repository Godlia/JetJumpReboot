using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

	float moveSpeed = 7f;

	Rigidbody2D rb;

	Transform target;
	public GameObject Player;
	Vector2 moveDirection;
	Transform rbtarget;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		target = Player.transform;
		moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
		rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
		Destroy(gameObject, 3);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name.Equals("Player"))
		{
			Debug.Log("Hit!");
			Destroy(gameObject);
		}
	}
}
