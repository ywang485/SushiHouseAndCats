using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatManager : MonoBehaviour {

	public int spawningInterval = 2;
	public int catPopularity;
	private GameManager gameManager;

	private int prevTime = 0;

	//static public readonly int minCatPopValForPetting = 10;
	//static public readonly int minCatPopValForFeeding = 30;

	public GameObject[] catList;

	int getSpawningLikelihood() {
		return 5 + (int)(3.5f * (float)gameManager.getSushiPlateCount());
	}

	void spawning(int totalMinute) {
		if (totalMinute % spawningInterval == 0) {
			if (prevTime != totalMinute) {
				// Construct active cat list
				List<GameObject> actCatList = new List<GameObject>();
				foreach (GameObject obj in catList) {
					Cat cat = obj.GetComponent<Cat> ();
					if (catPopularity <= cat.getActPopThUp() && catPopularity >= cat.getActPopThDown()) {
						actCatList.Add (obj);
					}
				}
				prevTime = totalMinute;
				int r = Random.Range (0, 100);
				if (r <= getSpawningLikelihood()) {
					int r2 = Random.Range (0, actCatList.Count);
					GameObject catsNode = GameObject.Find ("Cats");
					GameObject catObj = (GameObject)GameObject.Instantiate (actCatList[r2], gameManager.mapManager.findCatSpawningLoc (), Quaternion.identity);
					catObj.transform.SetParent (catsNode.transform);
				}
			}
//			if (cat.gameObject.activeInHierarchy) {
//				continue;
//			}
//
//			if (totalMinute % spawningInterval == 0) {
//				int r = Random.Range (0, 100);
//
//				if (r <= getSpawningLikelihood()) {
//					cat.gameObject.SetActive (true);
//					cat.gameObject.transform.position = gameManager.mapManager.findCatSpawningLoc();
//					cat.init ();
//				}
//			}
		}

	}

	public void increaseCatPopularity(int howmuch) {
		catPopularity += howmuch;
	}

	public void decreaseCatPopularity(int howmuch) {
		catPopularity -= howmuch;
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
