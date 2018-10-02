using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriers : Item
{
    public GameObject barrierPrefab;

    private GameManager gm;
    private Player player;

    public override void Start()
    {
        base.Start();

        gm = GameManager.GetInstance();
        player = gm.GetPlayer();
    }

    public override void UseItem()
    {
        base.UseItem();

        if (Input.GetKeyDown(KeyCode.Space))
            PlaceBarrier();
    }

    private void PlaceBarrier()
    {
        if (player.canPlaceItem && currentAmmo > 0)
        {
            Vector3 barrierPos = player.transform.position + player.transform.forward;

            barrierPos.x = Mathf.RoundToInt(barrierPos.x);
            barrierPos.z = Mathf.RoundToInt(barrierPos.z);
            barrierPos.y = transform.localScale.y / 2;

            Instantiate(barrierPrefab, barrierPos, Quaternion.identity);
            currentAmmo--;
        }
    }
}
