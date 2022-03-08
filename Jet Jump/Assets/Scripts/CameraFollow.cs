﻿using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

    private void Update()
    {
		if(target == null)
		{
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		}
	}


	

	
    void FixedUpdate()
	{
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;
	}

}