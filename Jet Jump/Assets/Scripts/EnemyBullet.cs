using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

	float moveSpeed = 10f;

	Rigidbody2D rb;

	Transform target;
	Vector2 moveDirection;

	// Use this for initialization
	void Awake() {
		target = GameObject.Find("Player").transform;
		rb = GetComponent<Rigidbody2D>();
		moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
		rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
		Destroy(gameObject, 3);
	}        
	void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag.Equals("Player")) {
            Destroy(this.gameObject);
        } else if (collision.gameObject.tag.Equals("Ground")) {
            Destroy(this.gameObject);
        } 
    }
}