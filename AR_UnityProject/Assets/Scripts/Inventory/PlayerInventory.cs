using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public List<GameObject> playerItems;

	private const int previousItemIndex = -1;
	private const int nextItemIndex = 1;

	private GameManager gm;
    private int currentItemIndex;
    private int currentWaveNumber;

    void Start ()
	{
		for (int i = 0; i < playerItems.Count; i++)
			playerItems[i].SetActive(false);

		currentItemIndex = 0;
		playerItems[currentItemIndex].SetActive(true);
		gm = GameManager.GetInstance();
        currentWaveNumber = gm.GetWaveNumber();
    }
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.X))
			ChangeItem(nextItemIndex);
		else if (Input.GetKeyDown(KeyCode.Z))
			ChangeItem(previousItemIndex);

        // Reloads all weapons every NEW WAVE
        if (currentWaveNumber < gm.GetWaveNumber())
        {
            currentWaveNumber = gm.GetWaveNumber();
            ReloadAmmo();
        }
    }

	void ChangeItem(int nextOrPrevious)
	{
		// Dont change item, if there is only 1 possible item to choose
		if (gm.unlockedItems > 1)
		{
			// Deactivate current item
			playerItems[currentItemIndex].SetActive(false);

			// Change current item to new one
			currentItemIndex += nextOrPrevious;

			// Goes around the list when index goes off limits
			if (currentItemIndex > gm.unlockedItems - 1)
				currentItemIndex = 0;
			else if (currentItemIndex < 0)
				currentItemIndex = gm.unlockedItems - 1;

			// Activate new item
			playerItems[currentItemIndex].SetActive(true);
		}
	}

    void ReloadAmmo()
    {
        for (int i = 0; i < playerItems.Count; i++)
        {
            Item item = playerItems[i].GetComponent<Item>();
			item.maxAmmo += gm.extraAmmoEarnedPerWave;
            item.currentAmmo = item.maxAmmo;
        }
    }
}
