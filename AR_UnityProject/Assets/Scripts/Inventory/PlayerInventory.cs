using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public List<GameObject> playerItems;
	public Transform itemPosition;

	private GameObject currentItem;
	private int currentItemIndex;

	private const int previousItemIndex = -1;
	private const int nextItemIndex = 1;


	void Start ()
	{
		currentItemIndex = 0;
		currentItem = Instantiate(playerItems[currentItemIndex], itemPosition.position, itemPosition.rotation);
		currentItem.transform.parent = itemPosition.transform.parent;
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
		// Dont allow to change weapon, if there is only 1 in the list
		if (playerItems.Count > 1)
		{
			currentItemIndex += nextOrPrevious;

			if (currentItemIndex > playerItems.Count - 1)
				currentItemIndex = 0;
			else if (currentItemIndex < 0)
				currentItemIndex = playerItems.Count - 1;

			GameObject previousItem = currentItem;
			Destroy(previousItem);

			currentItem = Instantiate(playerItems[currentItemIndex], itemPosition.position, itemPosition.rotation);
			currentItem.transform.parent = itemPosition.transform.parent;
		}
	}
}
