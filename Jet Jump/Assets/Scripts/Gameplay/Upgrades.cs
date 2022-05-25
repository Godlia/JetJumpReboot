using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    /* 
    # more damage
    # More speed
    # projectile speed
    # Shotgun spread and bullets
    # more health
    # More fuel
    # Faster fuel regain    
    */

    [SerializeField] private float speedMultiplier = 1.0f;
    [SerializeField] private float damageMultiplier = 1.0f;
    [SerializeField] private float projectileSpeedMultiplier = 1.0f;
    [SerializeField] private float fireRateMultiplier = 1.0f;
    [SerializeField] private float spreadMultiplier = 1.0f;
    [SerializeField] private int bulletsMultiplier = 1;
    [SerializeField] private float healthMultiplier = 1.0f;
    [SerializeField] private float fuelMultiplier = 1.0f;
    [SerializeField] private float fuelRegainMultiplier = 1.0f;

    public bool update;



    //basically en API for å oppgradere spilleren
    public float getMarkiplier(string multiplierName) {
        switch(multiplierName) {
            case "speed": //implemented
                return speedMultiplier;
            case "damage": //implemented
                return damageMultiplier;
            case "projectileSpeed": //implemented
                return projectileSpeedMultiplier;
            case "fireRate": //implemented
                return fireRateMultiplier;
            case "spread": //implemented
                return spreadMultiplier;
            case "bullets": //implemented
                return bulletsMultiplier;
            case "health": //implemented
                return healthMultiplier;
            case "fuel": //implemented
                return fuelMultiplier;
            case "fuelRegain": //implemented
                return fuelRegainMultiplier;
            default:
                return 1.0f;
        }
    }
}
