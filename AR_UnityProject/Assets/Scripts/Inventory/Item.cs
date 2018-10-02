using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int maxAmmo = 10;

    [HideInInspector]
    public int currentAmmo;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        UseItem();
        UI_Player.Get().currenWeapon.text = "Weapon: " + gameObject.name;

        if (gameObject.name == "Gun")
            UI_Player.Get().ammo.text = "Ammo: Infinity";
        else
            UI_Player.Get().ammo.text = "Ammo: " + currentAmmo + "/" + maxAmmo;

        if (currentAmmo <= 0)
            currentAmmo = 0;


    }

    public virtual void UseItem()
    {
        // Player's interaction
    }
}
