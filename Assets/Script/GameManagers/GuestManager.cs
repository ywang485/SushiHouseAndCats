using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuestManager : MonoBehaviour {

	public int spawningInterval = 2;
	public int humanPopularity;
	private GameManager gameManager;

	public int minimalWaitTime = 15;

	public GameObject[] guestList;

	private int prevTime = 0;

	int getSpawningLikelihood() {
		int l = 5 + (int)((float)humanPopularity / 3.0f - 2.0f * (float)gameManager.getNumFood() - 2.0f * (float)gameManager.getNumCat());
		if (l < 4) {
			l = 4;
		}
		int guestCount = gameManager.getGuestCount ();
		if (guestCount >= 4) {
			l = l / (2 * (guestCount - 3));
		}
		return l;
	}

	void spawning(int totalMinute) {

		if (totalMinute % spawningInterval == 0) {
			if (prevTime != totalMinute) {
				// Construct active guest list
				List<GameObject> actGuestList = new List<GameObject>();
				foreach (GameObject obj in guestList) {
					Guest guest = obj.GetComponent<Guest> ();
					if (humanPopularity <= guest.getActPopThUp() && humanPopularity >= guest.getActPopThDown()) {
						actGuestList.Add (obj);
					}
				}
				Debug.Log ("Guest Spawning Function Entered");
				prevTime = totalMinute;
				int r = Random.Range (0, 100);
				if (r <= getSpawningLikelihood()) {
					int r2 = Random.Range (0, actGuestList.Count);
					GameObject guestsNode = GameObject.Find ("Guests");
					GameObject guestObj = (GameObject)GameObject.Instantiate (actGuestList[r2], gameManager.mapManager.getDoorLocation (), Quaternion.identity);
					guestObj.transform.SetParent (guestsNode.transform);
				}
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
		if (humanPopularity >= 0) {
			return basicSpawnLikelihood * (humanPopularity + 10) / 100;
		} else {
			return basicSpawnLikelihood;
		}
	}

	public void increaseHumanPopularity(int howmuch) {
		humanPopularity += howmuch;
		if (humanPopularity > GameManager.maxPopularity) {
			humanPopularity = GameManager.maxPopularity;
		}
	}

	public void decreaseHumanPopularity(int howmuch) {
		humanPopularity -= howmuch;
		if (humanPopularity < 0) {
			humanPopularity = 0;
		}
	}

	// Initialization
	void Awake () {
		gameManager = FindObjectOfType(typeof (GameManager)) as GameManager;
	}

	// Update is called once per frame
	void Update () {

		spawning(gameManager.getCurrTimeInMinute());

	}
}
