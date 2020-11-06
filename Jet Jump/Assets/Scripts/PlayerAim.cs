using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAim : MonoBehaviour
{
    private Transform aimTransform;
    public KeyCode key;


    private void Awake() {
        aimTransform = transform.Find("Aim");
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

    private void HandleAiming()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        Debug.Log(angle);
    }


    private void handleShooting() {
        //if (Input.GetKeyDown(key)
    }
}
