using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviourPun
{
	private GameObject pTarget;
	private PhotonView pView;
	public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

    private void Start()
    {
		pTarget = GameObject.FindGameObjectWithTag("Player");
		pView = pTarget.GetComponent<PhotonView>();
		findPlayer();


	}



	void findPlayer()
    {
		if (pView.IsMine)
		{
			target = pTarget.GetComponent<Transform>();
		}
		else
        {
			pTarget = GameObject.FindGameObjectWithTag("Player");
			pView = pTarget.GetComponent<PhotonView>();
			findPlayer();
		}

	}

    void FixedUpdate()
	{
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;
	
	}

}