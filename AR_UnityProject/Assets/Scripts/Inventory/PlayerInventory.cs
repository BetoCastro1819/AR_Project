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

	void Start ()
	{
		currentItemIndex = 0;
		playerItems[currentItemIndex].SetActive(true);
		gm = GameManager.GetInstance();
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.X))
			ChangeItem(nextItemIndex);
		else if (Input.GetKeyDown(KeyCode.Z))
			ChangeItem(previousItemIndex);
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
}
