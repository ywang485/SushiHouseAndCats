using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class PlayerDataManager : MonoBehaviour {

	public static PlayerData getPlayerData() {
		if (playerData == null) {
			initializePlayerData ();

			playerData.playerName = "test";
				
		}

		return playerData;
	} 

	private static PlayerData playerData;

	public static HashSet<string> unlockedSushiType;

	void Awake () {
		DontDestroyOnLoad(transform.gameObject);
	}

	static void initializePlayerData() {
		playerData = new PlayerData ();

		// Player data initialization
		playerData.gameDuration = 0;
		playerData.tutorialCompleted = false;
		playerData.catSpawning = false;
		playerData.guestSpawning = false;

		playerData.catDefeatCombo = 0;
		playerData.catFedCombo = 0;

		playerData.dayCount = 1;
		playerData.numGold = 500;
		playerData.basicAttack = 3;
		playerData.basicAffinity = 1;
		playerData.itemAffinity = 0;
		playerData.weaponSpeed = 5f;
		playerData.cooldown = 1f;
		playerData.movingSpeed = 1F;
		playerData.humanPopularity = 0;
		playerData.catPopularity = 0;

		playerData.catEnemyLevel = 0;
		playerData.catFriendLevel = 0;

		playerData.catSpawningInterval = 5;
		playerData.maxNumCats2Spawn = 1;

		playerData.catMoodIconEnabled = false;
		playerData.catOrderEnabled = false;
		playerData.catPettingEnabled = false;
		playerData.catStoreEnabled = false;
		playerData.weaponStoreEnabled = false;
		playerData.galGameModeEnabled = false;

		playerData.ingredients = new Dictionary<string, int> ();
		playerData.ingredients ["rice"] = 0;
		playerData.ingredients ["tuna"] = 0;
		//playerData.ingredients ["duck-meat"] = 0;
		//playerData.ingredients ["egg"] = 0;
		//playerData.ingredients ["salmon"] = 0;
		//playerData.ingredients ["white-tuna"] = 0;
		//playerData.ingredients ["avocado"] = 0;
		//playerData.ingredients ["cucumber"] = 0;

		unlockedSushiType = new HashSet<string> ();
		unlockedSushiType.Add ("tuna-nigiri");
		//unlockedSushiType.Add ("peking-duck-nigiri");
		//unlockedSushiType.Add ("tuna-sashimi");

		playerData.catTypes = new Dictionary<string, CatRecord> ();
		playerData.catTypes.Add ("generic-cat", new CatRecord (0, 0));

		playerData.catsDefeated = new List<string> ();
		playerData.catsRaised = new List<string> ();

		playerData.nextIngredientToUnlock = 0;
		playerData.nextSushiTypeToUnlock = 0;

		playerData.sushiSold = 0;
		playerData.movingSpeedExperience = 0;
		playerData.affinityExperience = 0;
		playerData.weaponSpeedExperience = 0;

		playerData.inventory = new Dictionary<string, bool> ();
		playerData.unlockedItem = new Dictionary<string, bool> ();

		// Begin:Testing
		playerData.inventory.Add("feather-duster", true);
		//playerData.inventory.Add("bags-of-tomatos", true);
		playerData.unlockedItem.Add("bags-of-tomatos", true);
		//playerData.inventory.Add ("cat-biscuit", true);
		// End:Testing
	}

	public void startNewGame(InputField playerNameInput) {

		initializePlayerData ();

		playerData.playerName = playerNameInput.text;
		if (playerData.playerName == "") {
			playerData.playerName = "SomePlayer";
		}


		//playerData.itemsOwned = new ArrayList ();
		SceneManager.LoadScene ("OpeningStory");
		Debug.Log ("New Game Started as " + playerData.playerName);
	}

	public void loadGame() {
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			playerData = (PlayerData)bf.Deserialize (file);
			file.Close ();

			unlockedSushiType = new HashSet<string> (playerData.unlockedSushiType);

			Debug.Log ("Cat defeated: " + playerData.catTypes["generic-cat"].numDefeated);
			SceneManager.LoadScene ("EndOfDayMenu");
		} else {
			Debug.Log ("No save date found!");
		}
	}

	public void saveGame() {

		playerData.gameDuration += Mathf.FloorToInt (Time.time - GameManager.getGameManager ().gameStartTime) / 60;

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.OpenOrCreate);

		playerData.unlockedSushiType = new string[unlockedSushiType.Count];
		unlockedSushiType.CopyTo(playerData.unlockedSushiType);

		bf.Serialize (file, playerData);
		file.Close ();
	}
}
