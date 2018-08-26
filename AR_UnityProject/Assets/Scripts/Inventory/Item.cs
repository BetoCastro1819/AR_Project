using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int maxAmmo = 10;

    protected int currentAmmo;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        UseItem();
    }

    public virtual void UseItem()
    {
        // Player's interaction
    }
}
