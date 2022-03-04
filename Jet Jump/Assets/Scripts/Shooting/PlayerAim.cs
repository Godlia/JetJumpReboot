using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public GameObject Player;

    public SpriteRenderer PlayerSprite;

    public SpriteRenderer GunSprite;
    private Transform aimTransform;
    private Animator aimAnimator;
    private float angle;

    public Camera cam;

    private void Start()
    {
        aimTransform = transform.Find("Aim");
        aimAnimator = aimTransform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        RotateSprites();
        if(cam == null) {
        cam = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
        }
    }


    public void HandleAiming()
    {
        Vector2 direction; 
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - (Vector2)this.transform.position;

        Vector2 aimDirection = (mousePosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        float gunAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, gunAngle);
        angle = gunAngle;
        Debug.Log(direction + " " + aimDirection + "" + gunAngle + " " + angle);
    }

    public void RotateSprites()
    {
        if (angle < -90)
        {
            GunSprite.flipY = true;
            PlayerSprite.flipX = true;
        }
        else if (angle > 90)
        {
            GunSprite.flipY = true;
            PlayerSprite.flipX = true;
        }
        else
        {
            GunSprite.flipY = false;
            PlayerSprite.flipX = false;
        }

    }

}
