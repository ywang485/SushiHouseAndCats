using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfDayMenu : MonoBehaviour {

	PlayerDataManager pdm;
	Text goldDisplay;

	// Initialization
	public void Awake() {

		pdm = GameObject.Find ("PlayerDataManager").GetComponent<PlayerDataManager>();
		goldDisplay = GameObject.Find ("GoldDisplay").GetComponent<Text>();
	}

	public void saveGame() {
		pdm.saveGame ();
		GameObject saveButtonObj = GameObject.Find ("SaveButton");
		Button saveButton = saveButtonObj.GetComponent<Button>();
		Text saveText = saveButtonObj.GetComponentInChildren<Text> ();
		saveText.text = "Saved";
		saveButton.interactable = false;
	}

	public void startNewDay() {
		PlayerDataManager.getPlayerData().dayCount += 1;
		SceneManager.LoadScene ("GamePlay");
	}

	public void openMenu(string menu) {
		transform.Find (menu).gameObject.SetActive (true);
	}

	public void closeMenu(string menu) {
		transform.Find (menu).gameObject.SetActive (false);
	}

	public void buyItem(string itemName) {
		InventoryItem item = ItemDatabase.getItem (itemName);
		if (item == null) {
			Debug.Log ("Item " + itemName + " does not exist!");
			return;
		}
		if (PlayerDataManager.getPlayerData().numGold > item.getPrice ()) {
			PlayerDataManager.getPlayerData().numGold -= item.getPrice ();
			PlayerDataManager.getPlayerData().inventory.Add (itemName, true);
		}

	}

	void Update() {
		goldDisplay.text = "Gold: " + PlayerDataManager.getPlayerData().numGold + "G";
	}
}
