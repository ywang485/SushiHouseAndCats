using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyItemButton : MonoBehaviour {

	public int price;
	public string itemName;

	void Awake(){
		Text priceText = gameObject.GetComponentInChildren<Text> ();
		priceText.text = price.ToString () + "G";

		//if (GameManager.getGameManager ().numGold < price || PlayerDataManager.playerData.itemsOwned.Contains(itemName)) {
		//	gameObject.GetComponent<Button> ().interactable = false;
		//}

	}

	public void selfDisable() {
		gameObject.GetComponent<Button> ().interactable = false;
	}
}
