using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSettings : MonoBehaviour
{
    [SerializeField] GameObject settingsCanvas;
    public bool doCRT;
    // Start is called before the first frame update
    [SerializeField] private CRTEffect crtEffect;
    private void Start() {
        settingsCanvas = GameObject.FindGameObjectWithTag("PauseMenu");
        settingsCanvas.SetActive(false);
        crtEffect = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CRTEffect>();
        //get playerprefs
        SetCRT(System.Convert.ToBoolean(PlayerPrefs.GetInt("CRT")));
        
    }
    void Update()
    {
        if(crtEffect == null) {
            crtEffect = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CRTEffect>();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (settingsCanvas.activeSelf)
            {
                settingsCanvas.SetActive(false);
                Debug.Log("Settings Closed");
            }
            else
            {
                settingsCanvas.gameObject.SetActive(true);
                Debug.Log("Settings Opened");
            }
        }
        Debug.Log(doCRT + " , " + PlayerPrefs.GetInt("CRT"));
    }

    public void SetCRT(bool value) {
        doCRT = value;
        
        HandleCRT();
    }

    public void HandleCRT()
    {
        if(doCRT)
        {
            crtEffect.enabled = true;
            GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>().Render();
            PlayerPrefs.SetInt("CRT", 1);
        }
        else if(!doCRT)
        {
            crtEffect.enabled = false;
            GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>().Render();
            PlayerPrefs.SetInt("CRT", 0);
        }
    }
}
