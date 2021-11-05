using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private GameObject HUD;
    // Start is called before the first frame update
    void Start()
    {
        HUD = GameObject.FindGameObjectWithTag("HUD");
        HUD.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
