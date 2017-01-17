using System;
using System.Collections;

[Serializable]
public class PlayerData {

	public int numGold;
	public int humanPopularity;
	public int catPopularity;
	public int dayCount;
	public string playerName;

	public int basicAttack;

	public int basicCatPettingPower;
	public int itemCatPettingPower;

	public float movingSpeed;

	//public ArrayList itemsOwned;

	public bool catMoodIconEnabled;
	public bool catPettingEnabled;
	public bool catOrderEnabled;
	public bool weaponStoreEnabled;
	public bool catStoreEnabled;
	public bool galGameModeEnabled;

	// Ingredient
	public int numRice;
	public int numTuna;
	public int numEgg;
	public int numSalmon;
	public int numWhiteFish;
	public int numCucumber;
	public int numAvocado;


	// Weapon related
	public float weaponSpeed;
	public bool[] weaponStatus;

}
