using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponStoreManagement : MonoBehaviour {

	private GameManager gameManager;

	public Text throwingSpeedUpgradeText;
	public int throwingSpeedUpgradeCost;

	public Text movingSpeedUpgradeText;
	public int movingSpeedUpgradeCost;

	public Button catFoodUpgradeBtn;
	public Button catTreatsUpgradeBtn;
	public Button featherDusterUpgradeBtn;


	// Use this for initialization
	void Awake () {
		gameManager = GameManager.getGameManager ();
	}
	
	// Update is called once per frame
	void Update () {
		Button throwingSpeedUpgradeBtn = throwingSpeedUpgradeText.GetComponentInParent<Button> ();
		throwingSpeedUpgradeCost = (int)(100 + (PlayerDataManager.getPlayerData().weaponSpeed - 10.0f) * 1000.0f);
		throwingSpeedUpgradeText.text = "+ Throwing Speed (" + throwingSpeedUpgradeCost + "G)";
		if (PlayerDataManager.getPlayerData().numGold >= throwingSpeedUpgradeCost) {
			throwingSpeedUpgradeBtn.interactable = true;
		} else {
			throwingSpeedUpgradeBtn.interactable = false;
		}

		Button movingSpeedUpgradeBtn = movingSpeedUpgradeText.GetComponentInParent<Button> ();
		movingSpeedUpgradeCost = (int)(100 + (PlayerDataManager.getPlayerData().movingSpeed) * 1000.0f);
		movingSpeedUpgradeText.text = "+ Moving Speed (" + movingSpeedUpgradeCost + "G)";
		if (PlayerDataManager.getPlayerData().numGold >= movingSpeedUpgradeCost) {
			movingSpeedUpgradeBtn.interactable = true;
		} else {
			movingSpeedUpgradeBtn.interactable = false;
		}

		if (PlayerDataManager.getPlayerData().catPopularity > 50 && !PlayerDataManager.getPlayerData().weaponStatus [2] && PlayerDataManager.getPlayerData().numGold >= 100) {
			catFoodUpgradeBtn.interactable = true;
		} else {
			catFoodUpgradeBtn.interactable = false;
		}

		if (PlayerDataManager.getPlayerData().catPopularity > 20 && !PlayerDataManager.getPlayerData().weaponStatus [1] && PlayerDataManager.getPlayerData().numGold >= 50) {
			catTreatsUpgradeBtn.interactable = true;
		} else {
			catTreatsUpgradeBtn.interactable = false;
		}

		if (PlayerDataManager.getPlayerData().catPopularity <= -20 && !PlayerDataManager.getPlayerData().weaponStatus [3] && PlayerDataManager.getPlayerData().numGold >= 200) {
			featherDusterUpgradeBtn.interactable = true;
		} else {
			featherDusterUpgradeBtn.interactable = false;
		}
			
	}

	public void throwingSpeedUpgrade() {
		PlayerDataManager.getPlayerData().weaponSpeed += 0.2f;
		PlayerDataManager.getPlayerData().numGold -= throwingSpeedUpgradeCost;
		Debug.Log ("Current speed: " + PlayerDataManager.getPlayerData().weaponSpeed);
	}

	public void movingSpeedUpgrade() {
		PlayerDataManager.getPlayerData().movingSpeed += 0.2f;
		PlayerDataManager.getPlayerData().numGold -= movingSpeedUpgradeCost;
		Debug.Log ("Current speed: " + PlayerDataManager.getPlayerData().movingSpeed);
	}

	public void catFoodUpgrade() {
		PlayerDataManager.getPlayerData().numGold -= 100;
		PlayerDataManager.getPlayerData().weaponStatus [2] = true;
		Debug.Log ("Cat food obtained.");
	}

	public void catTreatsUpgrade() {
		PlayerDataManager.getPlayerData().numGold -= 50;
		PlayerDataManager.getPlayerData().weaponStatus [1] = true;
		Debug.Log ("Cat treats obtained.");
	}

	public void featherDusterUpgrade() {
		PlayerDataManager.getPlayerData().numGold -= 200;
		PlayerDataManager.getPlayerData().weaponStatus [3] = true;
		Debug.Log ("Featherduster obtained.");
	}
}
