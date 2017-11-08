using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Store : SCUI {

	static private readonly string buyItemBtnPrefab = "Prefabs/UI/BuyItemButton";
	private GameManager gameManager;
	//private EndOfDayMenu endOfDayMenu;

	// Use this for initialization
	void Start () {
		int posXoffset = 100;
		int count = 0;
		gameManager = GameManager.getGameManager ();
		//endOfDayMenu = transform.GetComponentInParent<EndOfDayMenu> ();
		foreach (string iid in PlayerDataManager.getPlayerData().unlockedItem.Keys) {
			GameObject buyItemBtn = GameObject.Instantiate (Resources.Load (buyItemBtnPrefab, typeof(GameObject)) as GameObject, transform.position, Quaternion.identity, transform);
			buyItemBtn.transform.SetParent (transform, false);
			buyItemBtn.GetComponentInChildren<Text> ().text = iid + "\n" + ItemDatabase.getItem(iid).getPrice() + "G";
			buyItemBtn.GetComponent<Button> ().GetComponent<Image>().sprite = Resources.Load (ItemDatabase.getItem(iid).getSpritePath(), typeof(Sprite)) as Sprite;
			buyItemBtn.GetComponent<Button> ().onClick.AddListener (() => {gameManager.buyItem (iid); buyItemBtn.GetComponent<Button> ().interactable = false;});
			buyItemBtn.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (50 + posXoffset * count, -50f);

			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerEnter;
			entry.callback.AddListener((data) => { showDescription(iid); });
			buyItemBtn.GetComponent<EventTrigger> ().triggers.Add(entry);

			EventTrigger.Entry entry2 = new EventTrigger.Entry ();
			entry2.eventID = EventTriggerType.PointerExit;
			entry2.callback.AddListener ((data) => {hideDescription();});
			buyItemBtn.GetComponent<EventTrigger>().triggers.Add(entry2);

			if (PlayerDataManager.getPlayerData().numGold < ItemDatabase.getItem (iid).getPrice () || PlayerDataManager.getPlayerData().inventory.ContainsKey(iid)) {
				buyItemBtn.GetComponent<Button> ().interactable = false;
			}

			count += 1;
		}
	}

	void showDescription(string itemId) {
		
		GetComponentInChildren<Text> ().text = ItemDatabase.getItem(itemId).getDescription();
	}

	void hideDescription() {
		GetComponentInChildren<Text> ().text = "";
	}


	// Update is called once per frame
	void Update () {
		
	}
}
