using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuestManager : MonoBehaviour {

	public int humanPopularity;
	private GameManager gameManager;

	public Guest[] guestList;

	void spawning(int totalMinute) {

		foreach (Guest guest in guestList) {

			if (guest.gameObject.activeInHierarchy) {
				continue;
			}

			if (guest.shouldSpawn(totalMinute)) {
				int r = Random.Range (0, 100);

				if (r <= calculateActualSpawnLikelihood(guest.getSpawningLikelihood())) {
					if ((guest.getEatingTime () + gameManager.getCurrTimeInMinute () + 15) < gameManager.closeTime * 60) {
						guest.gameObject.SetActive (true);
						guest.gameObject.transform.position = gameManager.mapManager.getDoorLocation ();
					}
				}
			}
		}

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
