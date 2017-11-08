using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuestManager : MonoBehaviour {

	public int spawningInterval = 2;
	private GameManager gameManager;

	public int minimalWaitTime = 15;

	public GameObject[] guestList;

	private int prevTime = 0;

	int getSpawningLikelihood() {
		int l = 5 + (int)((float)PlayerDataManager.getPlayerData().humanPopularity / 3.0f - 2.0f * (float)gameManager.getNumFood() - 2.0f * (float)gameManager.getNumCat());
		if (l < 4) {
			l = 4;
		}
		int guestCount = gameManager.getGuestCount ();
		if (guestCount >= 4) {
			l = l / (2 * (guestCount - 3));
		}
		return l;
	}

	public void spawnOneGuest(int r) {
		Debug.Log ("SpawnOneGuest called");
		// Construct active guest list
		List<GameObject> actGuestList = new List<GameObject>();
		foreach (GameObject obj in guestList) {
			Guest guest = obj.GetComponent<Guest> ();
			if (PlayerDataManager.getPlayerData().humanPopularity <= guest.getActPopThUp() && PlayerDataManager.getPlayerData().humanPopularity >= guest.getActPopThDown()) {
				actGuestList.Add (obj);
			}
		}
		if (r <= getSpawningLikelihood()) {
			int r2 = Random.Range (0, actGuestList.Count);
			GameObject guestsNode = GameObject.Find ("Guests");
			GameObject guestObj = (GameObject)GameObject.Instantiate (actGuestList[r2], gameManager.mapManager.getDoorLocation (), Quaternion.identity);
			guestObj.transform.SetParent (guestsNode.transform);
		}
	}

	void spawning(int totalMinute) {

		if (totalMinute % spawningInterval == 0) {
			if (prevTime != totalMinute) {
				spawnOneGuest (Random.Range (0, 100));
				prevTime = totalMinute;
			}
		}


//		foreach (Guest guest in guestList) {
//
//			if (guest.gameObject.activeInHierarchy) {
//				continue;
//			}
//
//			if (guest.shouldSpawn(totalMinute)) {
//				int r = Random.Range (0, 100);
//
//				if (r <= calculateActualSpawnLikelihood(guest.getSpawningLikelihood())) {
//					if ((guest.getEatingTime () + gameManager.getCurrTimeInMinute () + 15) < gameManager.closeTime * 60) {
//						guest.gameObject.SetActive (true);
//						guest.gameObject.transform.position = gameManager.mapManager.getDoorLocation ();
//					}
//				}
//			}
//		}

	}

	int calculateActualSpawnLikelihood(int basicSpawnLikelihood) {
		if (PlayerDataManager.getPlayerData().humanPopularity >= 0) {
			return basicSpawnLikelihood * (PlayerDataManager.getPlayerData().humanPopularity + 10) / 100;
		} else {
			return basicSpawnLikelihood;
		}
	}

	public void increaseHumanPopularity(int howmuch) {
		PlayerDataManager.getPlayerData().humanPopularity += howmuch;
		if (PlayerDataManager.getPlayerData().humanPopularity > GameManager.maxPopularity) {
			PlayerDataManager.getPlayerData().humanPopularity = GameManager.maxPopularity;
		}
	}

	public void decreaseHumanPopularity(int howmuch) {
		PlayerDataManager.getPlayerData().humanPopularity -= howmuch;
		if (PlayerDataManager.getPlayerData().humanPopularity < 0) {
			PlayerDataManager.getPlayerData().humanPopularity = 0;
		}
	}

	// Initialization
	void Awake () {
		gameManager = FindObjectOfType(typeof (GameManager)) as GameManager;
	}

	// Update is called once per frame
	void Update () {

		if (PlayerDataManager.getPlayerData ().guestSpawning) {
			spawning (gameManager.getCurrTimeInMinute ());
		}

	}
}
