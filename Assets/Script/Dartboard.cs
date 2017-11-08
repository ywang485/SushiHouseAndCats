using UnityEngine;
using System.Collections;

public class Dartboard : MonoBehaviour {

	public readonly int counterMax = 50;
	private int counter = 0;
	private ProgressBar progressBar;

	void Awake() {
		progressBar = transform.Find ("ProgressBar").GetComponent<ProgressBar>();
		progressBar.currValue = PlayerDataManager.getPlayerData().weaponSpeedExperience;
		progressBar.levelUpValue = LevelManager.getWeaponSpeedExperienceLevelUpValue ();
	}

	void Update() {
	}

	void OnTriggerEnter2D(Collider2D other) {
		//progressBar.GetComponent<SpriteRenderer> ().enabled = true;
		if (other.tag == "Player") {
			counter = 0;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		//progressBar.GetComponent<SpriteRenderer> ().enabled = false;
		if (other.tag == "Player") {
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		Debug.Log ("PlayerDataManager.getPlayerData().weaponSpeedExperience: "+ PlayerDataManager.getPlayerData().weaponSpeedExperience);
		Debug.Log ("LevelManager.getWeaponSpeedExperienceLevelUpValue ()" +  LevelManager.getWeaponSpeedExperienceLevelUpValue ());
		counter += 1;
		if (counter > counterMax) {
			PlayerDataManager.getPlayerData().weaponSpeedExperience += 1;
			counter = 0;
			if (PlayerDataManager.getPlayerData().weaponSpeedExperience >= LevelManager.getWeaponSpeedExperienceLevelUpValue ()) {
				PlayerDataManager.getPlayerData().weaponSpeed += 0.1f;
			}
		}
		progressBar.currValue = PlayerDataManager.getPlayerData().weaponSpeedExperience;
		progressBar.levelUpValue = LevelManager.getWeaponSpeedExperienceLevelUpValue ();

	}
}
