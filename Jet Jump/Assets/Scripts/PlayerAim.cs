using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAim : MonoBehaviour
{
    public GameObject Player;
    private Transform aimTransform;
    public KeyCode shootKey;
    private Animator aimAnimator;
    private float angle;
    public SpriteRenderer sp;
    public Transform Gun;
    Vector2 direction;


    private void Awake() {
        aimTransform = transform.Find("Aim");
        aimAnimator = aimTransform.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        handleShooting();
        HandleAiming();
    }

    public void HandleAiming()
    {

        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float gunAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, gunAngle);
        Debug.Log(gunAngle);
        angle = gunAngle;
    }


    private void handleShooting() {
        if (Input.GetKeyDown(shootKey)) {
            aimAnimator.SetTrigger("Fire");
        }
    }
}
