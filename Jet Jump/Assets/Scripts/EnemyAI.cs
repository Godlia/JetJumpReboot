using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

	[SerializeField]
	GameObject bullet;

	float fireRate;
	float nextFire;
	public float health = 20f;
	public float bulldmg = 5f;

	// Use this for initialization
	void Start()
	{
		fireRate = 1f;
		nextFire = Time.time;
	}

	// Update is called once per frame
	void Update()
	{
		CheckIfTimeToFire();
	}

	void CheckIfTimeToFire()
	{
		if (Time.time > nextFire)
		{
			//Instantiate(bullet, transform.position, Quaternion.identity);
			nextFire = Time.time + fireRate;
		}

	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			Destroy(gameObject);
		}
	}
}