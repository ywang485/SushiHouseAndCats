using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyItemFromStoreButton : MonoBehaviour {

	public string itemID;
	private EndOfDayMenu menu;
	private Button button;
	private InventoryItem item;

	// Use this for initialization
	void Start () {
		item = ItemDatabase.getItem (itemID);
		menu = GameObject.Find ("EndOfDayMenus").GetComponent<EndOfDayMenu> ();
		button = GetComponent<Button> ();
		button.onClick.AddListener(onClick);
		button.image.sprite = Resources.Load (item.getSpritePath (), typeof(Sprite)) as Sprite;
		updateInteractable ();

	}

	void onClick() {
		menu.buyItem (itemID);
		updateInteractable ();
	}

	void updateInteractable () {
		if (PlayerDataManager.getPlayerData().inventory.ContainsKey (itemID)) {
			button.interactable = false;
		}
	}
}
