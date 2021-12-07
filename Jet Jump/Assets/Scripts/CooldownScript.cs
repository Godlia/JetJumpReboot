using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownScript : MonoBehaviour
{
    [SerializeField]
    private Image imageCooldown;
    [SerializeField]
    private Text textCooldown;
    [SerializeField]
    private Image imageEdge;

    private bool isCooldown = false;
    [SerializeField]
    private float coolDownTime = 0.0f;

    private float coolDownTimer = 0.0f;





    // Update is called once per frame
    void Update()
    {
    }

    void ApplyCoolDown()
    {
        coolDownTimer -= Time.deltaTime;

        if (coolDownTimer < 0.0f)
        {
            isCooldown = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 1.0f;
        }
        else
        {
            textCooldown.text = Mathf.RoundToInt(coolDownTimer).ToString();
            imageCooldown.fillAmount = 1 - coolDownTimer / coolDownTime;
        }
    }

    public void useSpell()
    {
        if (isCooldown)
        {
            //still cooling
        }
        else
        {
            isCooldown = true;
            textCooldown.gameObject.SetActive(true);
            coolDownTimer = coolDownTime;
        }
    }
}

