using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public GameObject intervalGuest;
	public GameObject regularGuest;
	public GameObject cat;
	public GameObject catWithHP;

	[HideInInspector] public GameManager gameManager;

	void Awake() {
		gameManager = GameManager.getGameManager ();
	}

	private void setUpCPRelatedMechanism(int catPopularity) {
		if (catPopularity <= -100) {
			PlayerDataManager.playerData.weaponStoreEnabled = true;
		} else if (catPopularity < -60) {
			// Introduce tanky cats
			//GameObject tankyCat = (GameObject)Instantiate (catWithHP, new Vector3 (0, 0, 0), Quaternion.identity);
			//tankyCat.SetActive (false);
			//Cat tankyCatObj = tankyCat.GetComponent<Cat> ();
			//Cat[] originalCatList = gameManager.catManager.catList;
			//gameManager.catManager.catList = addObj2List<Cat> (originalCatList, tankyCatObj);
			//GameObject catsNode = GameObject.Find ("Cats");
			//if (catsNode != null) {
			//	tankyCat.transform.SetParent (catsNode.transform);
			//}
		} else if (catPopularity < -30) {
			// Introduce fast cats
			GameObject fastCat = (GameObject)Instantiate (cat, new Vector3 (0, 0, 0), Quaternion.identity);
			fastCat.SetActive (false);
			FastCat fastCatObj = fastCat.GetComponent<FastCat> ();
			fastCatObj.speed = 2f;
			//Cat[] originalCatList = gameManager.catManager.catList;
			//gameManager.catManager.catList = addObj2List<Cat> (originalCatList, fastCatObj);
			//GameObject catsNode = GameObject.Find ("Cats");
			//if (catsNode != null) {
			//	fastCat.transform.SetParent (catsNode.transform);
			//}
		}
		if (catPopularity > 40) {
			PlayerDataManager.playerData.galGameModeEnabled = true;
		}
		if (catPopularity > 30) {
			PlayerDataManager.playerData.catStoreEnabled = true;
		}
		if (catPopularity > 20) {
			PlayerDataManager.playerData.catOrderEnabled = true;
		}
		if (catPopularity > 10) {
			PlayerDataManager.playerData.catPettingEnabled = true;
		}
		if (catPopularity > 5) {
			PlayerDataManager.playerData.catMoodIconEnabled = true;
		} 
		
	}

	public void setUpLevel(int level) {

		if (gameManager == null) {
			gameManager = GameManager.getGameManager ();
		}

		setUpCPRelatedMechanism(gameManager.catManager.catPopularity);

		if (level == 2) {
			Debug.Log ("Level Manager: Day 2 set up!");
			// Add a guest who eats salmon nigiri
			GameObject salmonEatingGuest = (GameObject)Instantiate(intervalGuest, new Vector3(0, 0, 0), Quaternion.identity);
			salmonEatingGuest.SetActive (false);
			IntervalGuest guestObj = salmonEatingGuest.GetComponent<IntervalGuest> ();
			guestObj.sushi2order = new SushiManager.Sushi[1];
			guestObj.sushi2order [0] = SushiManager.Sushi.SalmonNigiri;
			Guest[] originalGuestList = gameManager.guestManager.guestList;
			gameManager.guestManager.guestList = addObj2List<Guest> (originalGuestList, salmonEatingGuest.GetComponent<Guest>());
			GameObject guestsNode = GameObject.Find ("Guests");
			if (guestsNode != null) {
				salmonEatingGuest.transform.SetParent (guestsNode.transform);
			}
		}

	}

	private void addCat2SpawnList() {
	}

	private void addGuest2SpawnList() {
	}

	private T[] addObj2List<T>(T[] list, T newObj) {
		T[] newList = new T[list.Length + 1];
		for (int i = 0; i < list.Length; i++) {
			newList [i] = list [i];
		}
		newList [list.Length] = newObj;

		return newList;
	}
}
