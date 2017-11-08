using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;

public class CatManager : MonoBehaviour, SCObserver {

	private GameManager gameManager;

	private int prevTime = 0;

	static public readonly string catPrefab = "Prefabs/Cat";

	public Color[] body_colors;
	public Color[] eye_colors;

	static public string path2WarningPrefab = "Prefabs/Meow";

	//public GameObject[] catList;

	int getSpawningLikelihood() {
		return 5 + (int)(3.5f * (float)gameManager.getSushiPlateCount());
	}

	public void spawnOneCat() {
		Location spawnLoc = gameManager.mapManager.findCatSpawningLoc ();
		GameObject.Instantiate (Resources.Load(path2WarningPrefab, typeof(GameObject)) as GameObject, spawnLoc.transform.position , Quaternion.identity);
		GameObject catsNode = GameObject.Find ("Cats");
		GameObject catObj = (GameObject)GameObject.Instantiate (Resources.Load(catPrefab, typeof(GameObject)) as GameObject, spawnLoc.transform.position , Quaternion.identity);
		catObj.transform.SetParent (catsNode.transform);
		catObj.GetComponent<Cat> ().lookingForFoodState.nextWayPoint = spawnLoc.reachableLocs [Random.Range (0, spawnLoc.reachableLocs.Length - 1)];
		catObj.GetComponent<Cat> ().currLoc = spawnLoc;
	}

	IEnumerator spawning(int totalMinute) {
		if (totalMinute % PlayerDataManager.getPlayerData ().catSpawningInterval == 0) {
			for (int i = 0; i < PlayerDataManager.getPlayerData ().maxNumCats2Spawn; i++) {
				Debug.Log ("Cat Spawning Time: " + i);
				if (prevTime != totalMinute) {
					prevTime = totalMinute;
					int r = Random.Range (0, 100);
					if (r <= getSpawningLikelihood ()) {
						spawnOneCat ();
					}
				}
				yield return new WaitForSeconds (0.5f);
			}
		}
	}

	public void increaseCatPopularity(int amt, string catTypeId, int friendlinessInc) {
		PlayerDataManager.getPlayerData().catPopularity += amt;
		if (catTypeId != "" && PlayerDataManager.getPlayerData().catTypes.ContainsKey(catTypeId)) {
			PlayerDataManager.getPlayerData().catTypes [catTypeId].numFed += friendlinessInc;
			if (LevelManager.catFriendlyEvolvolution.ContainsKey(catTypeId) && PlayerDataManager.getPlayerData().catTypes [catTypeId].numFed >= LevelManager.catFriendlyEvolvolution [catTypeId].evolutionRequirement) {
				PlayerDataManager.getPlayerData().catTypes.Remove (catTypeId);
				foreach (string catType in LevelManager.catFriendlyEvolvolution [catTypeId].evolvedCats) {
					PlayerDataManager.getPlayerData().catTypes [catType] = new CatRecord (0, 0);
				}
			}
		}
	}

	public void decreaseCatPopularity(int amt, string catTypeId, int hostilityInc) {
		PlayerDataManager.getPlayerData().catPopularity -= amt;
		if (catTypeId != "" && PlayerDataManager.getPlayerData().catTypes.ContainsKey(catTypeId)) {
			PlayerDataManager.getPlayerData().catTypes [catTypeId].numDefeated += hostilityInc;
			if (LevelManager.catHostileEvolvolution.ContainsKey(catTypeId) && PlayerDataManager.getPlayerData().catTypes [catTypeId].numDefeated >= LevelManager.catHostileEvolvolution [catTypeId].evolutionRequirement) {
				PlayerDataManager.getPlayerData().catTypes.Remove (catTypeId);
				foreach (string catType in LevelManager.catHostileEvolvolution [catTypeId].evolvedCats) {
					PlayerDataManager.getPlayerData().catTypes [catType] = new CatRecord (0, 0);
				}
			}
		}
	}

	// Initialization
	void Start () {
		gameManager = FindObjectOfType(typeof (GameManager)) as GameManager;
		Debug.Log (gameManager);
		Debug.Log (gameManager.messagingCenter);
		gameManager.messagingCenter.addObserver (this, GameMessagingCenter.evt_catDefeatedStr);
		gameManager.messagingCenter.addObserver (this, GameMessagingCenter.evt_catFedStr);
	}
	
	// Update is called once per frame
	void Update () {

		if (PlayerDataManager.getPlayerData ().catSpawning) {
			StartCoroutine(spawning (gameManager.getCurrTimeInMinute ()));
		}

	}

	public void OnNotify(SCEvent evt) {
		if (evt.GetType () == typeof(CatDefeatedEvent)) {
			PlayerDataManager.getPlayerData ().catDefeatCombo += 1;
			int prevCatFedCombo = PlayerDataManager.getPlayerData ().catFedCombo;
			PlayerDataManager.getPlayerData ().catFedCombo = 0;
			if (prevCatFedCombo > 0) {
				GameManager.getGameManager ().messagingCenter.eventHappened (new CatFedComboStopEvent());
			}
			if (PlayerDataManager.getPlayerData ().catDefeatCombo == 2) {
				GameManager.getGameManager ().messagingCenter.eventHappened (new CatDefeatedComboStartEvent());
			}
			if (-PlayerDataManager.getPlayerData ().catPopularity >= LevelManager.getLevelUpXp (PlayerDataManager.getPlayerData ().catEnemyLevel)) {
				PlayerDataManager.getPlayerData ().catEnemyLevel += 1;
				GameManager.getGameManager ().messagingCenter.eventHappened (new CatEnemyLevelUpEvent(PlayerDataManager.getPlayerData ().catEnemyLevel));
			}
		} else if (evt.GetType () == typeof(CatFedEvent)) {
			CatFedEvent catFed = (CatFedEvent)evt;
			GameManager.getGameManager ().catManager.increaseCatPopularity (catFed.getCatPopInc(), catFed.getCatTypeId(), 1);
			PlayerDataManager.getPlayerData ().catFedCombo += 1;
			int prevCatDefeatCombo = PlayerDataManager.getPlayerData ().catDefeatCombo;
			PlayerDataManager.getPlayerData ().catDefeatCombo = 0;
			if (prevCatDefeatCombo > 0) {
				GameManager.getGameManager ().messagingCenter.eventHappened (new CatDefeatedComboStopEvent());
			}
			if (PlayerDataManager.getPlayerData ().catFedCombo == 2) {
				GameManager.getGameManager ().messagingCenter.eventHappened (new CatFedComboStartEvent());
			}
		}
	}
}
