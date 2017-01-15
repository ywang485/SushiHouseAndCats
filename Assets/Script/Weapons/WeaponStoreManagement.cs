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


	// Use this for initialization
	void Awake () {
		gameManager = GameManager.getGameManager ();
	}
	
	// Update is called once per frame
	void Update () {
		Button throwingSpeedUpgradeBtn = throwingSpeedUpgradeText.GetComponentInParent<Button> ();
		throwingSpeedUpgradeCost = (int)(100 + (gameManager.weaponSpeed - 10.0f) * 1000.0f);
		throwingSpeedUpgradeText.text = "+ Throwing Speed (" + throwingSpeedUpgradeCost + "G)";
		if (gameManager.numGold >= throwingSpeedUpgradeCost) {
			throwingSpeedUpgradeBtn.interactable = true;
		} else {
			throwingSpeedUpgradeBtn.interactable = false;
		}

		Button movingSpeedUpgradeBtn = movingSpeedUpgradeText.GetComponentInParent<Button> ();
		movingSpeedUpgradeCost = (int)(100 + (gameManager.movingSpeed) * 1000.0f);
		movingSpeedUpgradeText.text = "+ Moving Speed (" + movingSpeedUpgradeCost + "G)";
		if (gameManager.numGold >= movingSpeedUpgradeCost) {
			movingSpeedUpgradeBtn.interactable = true;
		} else {
			movingSpeedUpgradeBtn.interactable = false;
		}

		if (gameManager.catManager.catPopularity > 50 && !gameManager.weaponManager.weaponStatus [2] && gameManager.numGold >= 100) {
			catFoodUpgradeBtn.interactable = true;
		} else {
			catFoodUpgradeBtn.interactable = false;
		}

		if (gameManager.catManager.catPopularity > 20 && !gameManager.weaponManager.weaponStatus [1] && gameManager.numGold >= 50) {
			catTreatsUpgradeBtn.interactable = true;
		} else {
			catTreatsUpgradeBtn.interactable = false;
		}
			
	}

	public void throwingSpeedUpgrade() {
		gameManager.weaponSpeed += 0.2f;
		gameManager.numGold -= throwingSpeedUpgradeCost;
		Debug.Log ("Current speed: " + gameManager.weaponSpeed);
	}

	public void movingSpeedUpgrade() {
		gameManager.movingSpeed += 0.2f;
		gameManager.numGold -= movingSpeedUpgradeCost;
		Debug.Log ("Current speed: " + gameManager.movingSpeed);
	}

	public void catFoodUpgrade() {
		gameManager.numGold -= 100;
		gameManager.weaponManager.weaponStatus [2] = true;
		Debug.Log ("Cat food obtained.");
	}

	public void catTreatsUpgrade() {
		gameManager.numGold -= 50;
		gameManager.weaponManager.weaponStatus [1] = true;
		Debug.Log ("Cat treats obtained.");
	}
}
