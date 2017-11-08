using UnityEngine;
using System.Collections;

public class Treadmill : MonoBehaviour {

	public readonly int counterMax = 50;
	private int counter = 0;
	private ProgressBar progressBar;

	void Awake() {
		progressBar = transform.Find ("ProgressBar").GetComponent<ProgressBar>();
		progressBar.currValue = PlayerDataManager.getPlayerData().movingSpeedExperience;
		progressBar.levelUpValue = LevelManager.getMovingSpeedExperienceLevelUpValue ();
	}

	void Update() {
	}

	void OnTriggerEnter2D(Collider2D other) {
		//progressBar.GetComponent<SpriteRenderer> ().enabled = true;
		if (other.tag == "Player") {
			other.GetComponent<Moving>().setExternalVelocity(new Vector2(-0.7f, 0f));
			counter = 0;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		//progressBar.GetComponent<SpriteRenderer> ().enabled = false;
		if (other.tag == "Player") {
			other.GetComponent<Moving>().setExternalVelocity(new Vector2(0f, 0f));
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		counter += 1;
		if (counter > counterMax) {
			PlayerDataManager.getPlayerData().movingSpeedExperience += 1;
			counter = 0;
			if (PlayerDataManager.getPlayerData().movingSpeedExperience >= LevelManager.getMovingSpeedExperienceLevelUpValue ()) {
				PlayerDataManager.getPlayerData().movingSpeed += 0.1f;
			}
		}
		progressBar.currValue = PlayerDataManager.getPlayerData().movingSpeedExperience;
		progressBar.levelUpValue = LevelManager.getMovingSpeedExperienceLevelUpValue ();

	}
}
