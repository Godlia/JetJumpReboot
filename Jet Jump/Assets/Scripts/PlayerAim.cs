using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Photon.Pun;

public class PlayerAim : MonoBehaviourPun
{
    public GameObject Player;

    public SpriteRenderer PlayerSprite;

    public SpriteRenderer GunSprite;
    private Transform aimTransform;
    private Animator aimAnimator;
    private float angle;
    public SpriteRenderer sp;
    PhotonView view;


    private void Awake() {
        aimTransform = transform.Find("Aim");
        aimAnimator = aimTransform.GetComponent<Animator>();
        view = GetComponentInParent<PhotonView>();
    }
    /*
    private void Start()
    {
        GameObject[] Cameras = GameObject.FindGameObjectsWithTag("PlayerCamera");
        foreach (GameObject camera in Cameras)
        {
            if (camera.GetComponent<PhotonView>().IsMine)
            {
                Cam = camera.GetComponent<Camera>();
                break;
            }
        }
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            HandleAiming();
            RotateSprites();
        }
    }


    public void HandleAiming()
    {

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.x -= Screen.width / 2;
        mousePosition.y -= Screen.height / 2;

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float gunAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, gunAngle);
        angle = gunAngle;
    }

    public void RotateSprites() {
    if (angle < -90) {
        GunSprite.flipY = true;
        PlayerSprite.flipX = true;
    } else if(angle > 90) {
        GunSprite.flipY = true;
        PlayerSprite.flipX = true;
    } else {
        GunSprite.flipY = false;
        PlayerSprite.flipX = false;
    }

    }

}
