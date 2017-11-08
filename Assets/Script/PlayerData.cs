using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class PlayerData {

	public bool tutorialCompleted;
	public int gameDuration;

	public bool catSpawning;
	public bool guestSpawning;

	public int numGold;
	public int humanPopularity;
	public int catPopularity;
	public int dayCount;
	public string playerName;
	public int catEnemyLevel;
	public int catFriendLevel;

	public int catSpawningInterval;
	public int maxNumCats2Spawn;

	public int basicAttack;

	public int basicAffinity;
	public int itemAffinity;

	public float movingSpeed;

	public int catDefeatCombo;
	public int catFedCombo;

	//public ArrayList itemsOwned;

	public bool catMoodIconEnabled;
	public bool catPettingEnabled;
	public bool catOrderEnabled;
	public bool weaponStoreEnabled;
	public bool catStoreEnabled;
	public bool galGameModeEnabled;

	// Ingredient
	public Dictionary<string, int> ingredients;
	public string[] unlockedSushiType;

	// Cat pool
	public Dictionary<string, CatRecord> catTypes;
	public List<string> catsDefeated;
	public List<string> catsRaised;
	//public Dictionary<string, int> numCatDefeated;
	//public Dictionary<string, int> numFeeding;

	// Weapon related
	public float weaponSpeed;
	public float cooldown;
	public bool[] weaponStatus;

	// Experience
	public int sushiSold;
	public int movingSpeedExperience;
	public int affinityExperience;
	public int weaponSpeedExperience;

	public int nextIngredientToUnlock;
	public int nextSushiTypeToUnlock;

	// Inventory
	public Dictionary<string, bool> inventory;
	public Dictionary<string, bool> unlockedItem;

	//public int numSushiSold;

}
