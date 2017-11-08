using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Element2Unlock {
	public int measure;
	public string elementId;
	public Element2Unlock(string elementId, int measure) {
		this.measure = measure;
		this.elementId = elementId;
	}
}

public class CatEvolutionRecord {
	public List<string> evolvedCats;
	public int evolutionRequirement;
	public CatEvolutionRecord(List<string> evolvedCats, int evolutionRequirement) {
		this.evolvedCats = evolvedCats;
		this.evolutionRequirement = evolutionRequirement;
	}
}

public class LevelManager : MonoBehaviour, SCObserver {

	static public readonly List<Element2Unlock> ingredientUnlock = new List<Element2Unlock>() {
		new Element2Unlock("egg", 25),
		new Element2Unlock("salmon", 50)
	};

	static public readonly List<Element2Unlock> sushiUnlock = new List<Element2Unlock> () {
		new Element2Unlock("tamago-nigiri", 25),
		new Element2Unlock("salmon-nigiri",  50),
	};

	static public readonly List<Element2Unlock> catEnemySushiUnlock = new List<Element2Unlock>() {
		new Element2Unlock("tuna-sashimi", 1),
		new Element2Unlock("peking-duck-nigiri", 2)
	};

	static public readonly List<Element2Unlock> catEnemyIngredientUnlock = new List<Element2Unlock>() {
		new Element2Unlock("none", 0),
		new Element2Unlock("duck-meat", 2)
	};

	static public readonly Dictionary<string, CatEvolutionRecord> catHostileEvolvolution = new Dictionary<string, CatEvolutionRecord>() {
		{"generic-cat", new CatEvolutionRecord(new List<string>{"monster-cat-1", "monster-cat-2"}, 3)}
	};
	static public readonly Dictionary<string, CatEvolutionRecord> catFriendlyEvolvolution = new Dictionary<string, CatEvolutionRecord>() {
		{"generic-cat", new CatEvolutionRecord(new List<string>{"canvas-cat", "book-cat"}, 3)}
	};

	public GameObject intervalGuest;
	public GameObject regularGuest;
	public GameObject cat;
	public GameObject catWithHP;

	[HideInInspector] public GameManager gameManager;

	private string newSushiTypeUnlockedDialog = "Prefabs/UI/NewSushiUnlockedBox";
	private string newIngredientUnlockedDialog = "Prefabs/UI/NewIngredientUnlockedBox";

	void Awake() {
		gameManager = GameManager.getGameManager ();
	}

	void Start() {
		gameManager.messagingCenter.addObserver (this, GameMessagingCenter.evt_catEnemyLevelUpStr);
		gameManager.messagingCenter.addObserver (this, GameMessagingCenter.evt_catFriendLevelUpStr);
	}

	public void OnNotify(SCEvent evt) {
		if (evt.GetType () == typeof(CatEnemyLevelUpEvent)) {
			CatEnemyLevelUpEvent elu = (CatEnemyLevelUpEvent)evt;
			unlockSushiTypeWithEnemyLevel (elu.getCurrEnemyLevel());
			unlockIngredientWithEnemyLevel (elu.getCurrEnemyLevel ());
			adjustCatSpawningPerEnemyLevel (elu.getCurrEnemyLevel ());

		} else if (evt.GetType () == typeof(CatFriendLevelUpEvent)) {

		}
	}

	static public int getLevelUpXp(int level) {
		Debug.Log ("XP needed for level 1: " + Mathf.Pow(0 + 1, 2) * 10);
		return Mathf.CeilToInt(Mathf.Pow(level + 1, 2) * 10);
	}

	static public List<CatComponent> getCatHeads() {
		return CatComponentDatabase.normalHeads;
	}

	static public List<CatComponent> getCatBodies() {
		return CatComponentDatabase.normalBodies;
	}

	static public List<CatComponent> getCatEyes() {
		return CatComponentDatabase.normalEyes ;
	}

	static public List<CatComponent> getCatHeadMarks() {
		return CatComponentDatabase.normalHeadMarks;
	}

	static public List<CatComponent> getCatBackMarks() {
		return CatComponentDatabase.normalBackMarks;
	}

	static public List<CatComponent> getCatBellyMarks() {
		return CatComponentDatabase.normalBellyMarks;
	}

	static public List<CatComponent> getCatTailMarks() {
		return CatComponentDatabase.normalTailMarks;
	}

	static public List<Color> getBodyColors() {
		return CatComponentDatabase.normalBodyColors;
	}

	static public float getBaseSpeed() {

		int catPopularity = PlayerDataManager.getPlayerData().catPopularity;

		if (catPopularity > 0) {
			return 1f;
		} else {
			return 1f + (-catPopularity) * 0.005f;
		}
	}

	static public int getBaseSearchingTime() {
		int catPopularity = PlayerDataManager.getPlayerData().catPopularity;

		return Mathf.RoundToInt (20f + (0.1f * Mathf.Abs (catPopularity)));
	}

	static public int getBaseHP() {
		int catPopularity = PlayerDataManager.getPlayerData().catPopularity;

		if (catPopularity > -30) {
			return 0;
		} else {
			return (-catPopularity-30) * 5;
		}
	}

	static public bool getHasDeath() {
		int catPopularity = PlayerDataManager.getPlayerData().catPopularity;

		if (catPopularity > -100) {
			return false;
		} else {
			return true;
		}

	}

	static public int getAttackingProbability() {
		int catPopularity = PlayerDataManager.getPlayerData().catPopularity;

		if (catPopularity > -100) {
			return 0;
		} else {
			return -catPopularity-100;
		}
	}

	static public int getMovingSpeedExperienceLevelUpValue() {
		return Mathf.RoundToInt(10f * (1f + PlayerDataManager.getPlayerData().movingSpeed * 10)) + Mathf.RoundToInt(Mathf.Pow(10f, PlayerDataManager.getPlayerData().movingSpeed * 10f));
	}

	static public int getWeaponSpeedExperienceLevelUpValue() {
		return Mathf.RoundToInt(10f * (1f + PlayerDataManager.getPlayerData().weaponSpeed * 2)) + Mathf.RoundToInt(Mathf.Pow(10f, PlayerDataManager.getPlayerData().weaponSpeed * 2f));
	}

	private void setUpCPRelatedMechanism(int catPopularity) {
		if (catPopularity <= -100) {
			PlayerDataManager.getPlayerData().weaponStoreEnabled = true;
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
			//GameObject fastCat = (GameObject)Instantiate (cat, new Vector3 (0, 0, 0), Quaternion.identity);
			//fastCat.SetActive (false);
			//FastCat fastCatObj = fastCat.GetComponent<FastCat> ();
			//fastCatObj.speed = 2f;
			//Cat[] originalCatList = gameManager.catManager.catList;
			//gameManager.catManager.catList = addObj2List<Cat> (originalCatList, fastCatObj);
			//GameObject catsNode = GameObject.Find ("Cats");
			//if (catsNode != null) {
			//	fastCat.transform.SetParent (catsNode.transform);
			//}
		}
		if (catPopularity > 40) {
			PlayerDataManager.getPlayerData().galGameModeEnabled = true;
		}
		if (catPopularity > 30) {
			PlayerDataManager.getPlayerData().catStoreEnabled = true;
		}
		if (catPopularity > 20) {
			PlayerDataManager.getPlayerData().catOrderEnabled = true;
		}
		if (catPopularity > 10) {
			PlayerDataManager.getPlayerData().catPettingEnabled = true;
		}
		if (catPopularity > 5) {
			PlayerDataManager.getPlayerData().catMoodIconEnabled = true;
		} 
		
	}

	public void unlockIngredient() {
		while (PlayerDataManager.getPlayerData().nextIngredientToUnlock < ingredientUnlock.Count && PlayerDataManager.getPlayerData().sushiSold >= ingredientUnlock [PlayerDataManager.getPlayerData().nextIngredientToUnlock].measure) {
			PlayerDataManager.getPlayerData().ingredients [ingredientUnlock[PlayerDataManager.getPlayerData().nextIngredientToUnlock].elementId] = 0;
			Debug.Log (ingredientUnlock [PlayerDataManager.getPlayerData().nextIngredientToUnlock].elementId + " unlocked.");
			PlayerDataManager.getPlayerData().nextIngredientToUnlock += 1;
		}
	}

	public void unlockSushiTypeWithEnemyLevel(int level) {
		if (level <= catEnemySushiUnlock.Count) {
			PlayerDataManager.unlockedSushiType.Add (catEnemySushiUnlock [level - 1].elementId);
			Debug.Log (catEnemySushiUnlock [level - 1].elementId + " unlocked.");
			gameManager.messagingCenter.eventHappened (new NewSushiTypeUnlockedEvent(catEnemySushiUnlock [level - 1].elementId));
			NewSushiUnlockedBox dialog = GameManager.instantiateUI (newSushiTypeUnlockedDialog).GetComponent<NewSushiUnlockedBox>();
			dialog.setContent (catEnemySushiUnlock [level - 1].elementId);
		}
	}

	public void unlockIngredientWithEnemyLevel(int level) {
		if (level <= catEnemyIngredientUnlock.Count) {
			if (catEnemyIngredientUnlock [level - 1].elementId != "none") {
				PlayerDataManager.getPlayerData ().ingredients [catEnemyIngredientUnlock [level - 1].elementId] = 0;
				Debug.Log (catEnemyIngredientUnlock [level - 1].elementId + " unlocked.");
				gameManager.messagingCenter.eventHappened (new NewIngredientUnlockedEvent (catEnemyIngredientUnlock [level - 1].elementId));
				NewIngredientUnlockedUI dialog = GameManager.instantiateUI (newIngredientUnlockedDialog).GetComponent<NewIngredientUnlockedUI> ();
				dialog.setContent (catEnemyIngredientUnlock [level - 1].elementId);
			}
		}
	}

	public void adjustCatSpawningPerEnemyLevel(int level) {
		if (level == 1) {
			PlayerDataManager.getPlayerData ().maxNumCats2Spawn = 2;
		}
	}

	public void unlockSushiType() {
		while (PlayerDataManager.getPlayerData().nextSushiTypeToUnlock < sushiUnlock.Count && PlayerDataManager.getPlayerData().sushiSold >= sushiUnlock [PlayerDataManager.getPlayerData().nextSushiTypeToUnlock].measure) {
			PlayerDataManager.unlockedSushiType.Add(sushiUnlock[PlayerDataManager.getPlayerData().nextSushiTypeToUnlock].elementId);
			Debug.Log (sushiUnlock [PlayerDataManager.getPlayerData().nextSushiTypeToUnlock].elementId + " unlocked.");
			PlayerDataManager.getPlayerData().nextSushiTypeToUnlock += 1;
			gameManager.messagingCenter.eventHappened (new NewSushiTypeUnlockedEvent(sushiUnlock[PlayerDataManager.getPlayerData().nextSushiTypeToUnlock].elementId));
			NewSushiUnlockedBox dialog = GameManager.instantiateUI (newSushiTypeUnlockedDialog).GetComponent<NewSushiUnlockedBox>();
			dialog.setContent (sushiUnlock[PlayerDataManager.getPlayerData().nextSushiTypeToUnlock].elementId);
		}
	}

	public void setUpLevel(int level) {

		if (gameManager == null) {
			gameManager = GameManager.getGameManager ();
		}

		setUpCPRelatedMechanism(PlayerDataManager.getPlayerData().catPopularity);

		if (level == 2) {
			Debug.Log ("Level Manager: Day 2 set up!");
			// Add a guest who eats salmon nigiri
			GameObject salmonEatingGuest = (GameObject)Instantiate(intervalGuest, new Vector3(0, 0, 0), Quaternion.identity);
			salmonEatingGuest.SetActive (false);
			IntervalGuest guestObj = salmonEatingGuest.GetComponent<IntervalGuest> ();
			guestObj.sushi2order = new string[1];
			guestObj.sushi2order [0] = "salmon-nigiri";
//			Guest[] originalGuestList = gameManager.guestManager.guestList;
//			gameManager.guestManager.guestList = addObj2List<Guest> (originalGuestList, salmonEatingGuest.GetComponent<Guest>());
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
