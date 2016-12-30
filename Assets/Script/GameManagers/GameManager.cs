using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour {

	// Static Parameters
	static public readonly int rent = 10;

	// Day Count
	static public int dayCount = 1;

	// Daily Profit Count
	static public int netProfit = 0;
	private int initNumGold;

	// Gameplay data
	public int numGold;
	public int basicAttack = 3;
	public int weaponAttack = 0;
	public int basicCatPettingPower = 1;
	public int itemCatPettingPower = 0;

	public MapManager mapManager;

	[HideInInspector] public SushiManager sushiManager;
	[HideInInspector] public IngredientManager ingredientManager;
	[HideInInspector] public CatManager catManager;
	[HideInInspector] public GuestManager guestManager;
	[HideInInspector] public PlayerDataManager playerDataManager;
	[HideInInspector] public LevelManager levelManager;

	public const int maxPopularity = 100;

	// In-Game Time Control
	private int hour;
	private int minute = 1;

	public float minuteLength = 1;

	float lastChange;
	public static bool clockPaused = false;

	public int openTime = 9;
	public int closeTime = 21;

	//public GameObject sushiIndicator;
	public GameObject gamePausedIndicator;
	public GameObject systemMenu;
	public GameObject weaponStore;
	public GameObject catSupplyStore;
	public GameObject catGalGameModePanel;

	// Audio Clips
	public AudioClip posBlipSFX;
	public AudioClip negBlipSFX;
	public AudioClip coinSFX;
	public AudioClip hurtSFX;
	public AudioClip grumpyMeowSFX;
	public AudioClip happyMeowSFX;

	// Temp
	public GameObject ending1;
	public GameObject ending2;

	public Text gameplayDataIndicator;
	public Text clockInidcator;
	public Text dayIndicator;
	public GameObject bottomTextPanel;
	public Text bottomText;

	public GameObject endOfDayDisplay;
	public Text endOfDayMessage;

	public Sprite[] moodIcons;

	private AudioSource audioSrc;

	public void playBlipSFX(bool positive) {
		if(positive) {
			audioSrc.PlayOneShot(posBlipSFX, 1f);
		} else {
			audioSrc.PlayOneShot(negBlipSFX, 1f);
		}
	}

	public void playCoinSFX() {
		audioSrc.PlayOneShot(coinSFX, 1f);
	}

	public void playHurtSFX() {
		audioSrc.PlayOneShot(hurtSFX, 1f);
	}

	public int getSushiPlateCount() {
		return GameObject.FindGameObjectsWithTag ("Sushi").Length;
	}

	public void playMeowSFX(bool happy) {
		if (happy) {
			audioSrc.PlayOneShot (happyMeowSFX, 1f);
		} else {
			audioSrc.PlayOneShot (grumpyMeowSFX, 1f);
		}
	}

	public static GameManager getGameManager() {
		return FindObjectOfType(typeof (GameManager)) as GameManager;
	}

	// initialization
	void Awake () {

		audioSrc = GetComponent<AudioSource> ();

		sushiManager = gameObject.GetComponentInChildren<SushiManager> ();
		ingredientManager = gameObject.GetComponentInChildren<IngredientManager> ();
		catManager = gameObject.GetComponentInChildren<CatManager> ();
		guestManager = gameObject.GetComponentInChildren<GuestManager> ();
		levelManager = gameObject.GetComponentInChildren<LevelManager> ();

		GameObject playerDataManagerObj = GameObject.Find ("PlayerDataManager");

		lastChange = Time.time;
		hour = openTime;

		initNumGold = numGold;

		// Load Game
		if (playerDataManagerObj != null) {
			playerDataManager = playerDataManagerObj.GetComponent<PlayerDataManager>();
			if (playerDataManager.isLoadedGame) {
				Debug.Log ("Loading Saved Game");
				PlayerData data = PlayerDataManager.playerData;
				catManager.catPopularity = data.catPopularity;
				guestManager.humanPopularity = data.humanPopularity;
				numGold = data.numGold;
				dayCount = data.dayCount;
				playerDataManager.isLoadedGame = false;
				endTodayAndStartNewDay ();

				// Load number of each ingredient
				ingredientManager.numRice = data.numRice;
				ingredientManager.numSalmon = data.numSalmon;
				ingredientManager.numAvocado = data.numAvocado;
				ingredientManager.numCucumber = data.numCucumber;
				ingredientManager.numEgg = data.numEgg;
				ingredientManager.numTuna = data.numTuna;
				ingredientManager.numWhiteFish = data.numWhiteFish;

				basicAttack = data.basicAttack;
				weaponAttack = data.weaponAttack;

				basicCatPettingPower = data.basicCatPettingPower;
				itemCatPettingPower = data.itemCatPettingPower;
			}
		}
	}

	public int calculateDamage(int defense) {
		return basicAttack + weaponAttack - defense;
	}

	public int calculatePettingPower() {
		return basicCatPettingPower + itemCatPettingPower;
	}

	public void buyIngredient(int ingredientNo) {

		IngredientManager.Ingredient ingredient;

		if (ingredientNo == 1) {
			ingredient = IngredientManager.Ingredient.Tuna;
		} else if (ingredientNo == 2) {
			ingredient = IngredientManager.Ingredient.Salmon;
		} else if (ingredientNo == 3) {
			ingredient = IngredientManager.Ingredient.WhiteTuna;
		} else if (ingredientNo == 4) {
			ingredient = IngredientManager.Ingredient.Egg;
		} else if (ingredientNo == 5) {
			ingredient = IngredientManager.Ingredient.Cucumber;
		} else if (ingredientNo == 6) {
			ingredient = IngredientManager.Ingredient.Avocado;
		} else {
			return;
		}

		bool success = false;

		if (ingredient == IngredientManager.Ingredient.Tuna) {
			success = ingredientManager.buyTuna ();
		} else if (ingredient == IngredientManager.Ingredient.Salmon) {
			success = ingredientManager.buySalmon ();
		} else if (ingredient == IngredientManager.Ingredient.WhiteTuna) {
			success = ingredientManager.buyWhiteFish ();
		} else if (ingredient == IngredientManager.Ingredient.Egg) {
			success = ingredientManager.buyEgg ();
		} else if (ingredient == IngredientManager.Ingredient.Cucumber) {
			success = ingredientManager.buyCucumber ();
		} else if (ingredient == IngredientManager.Ingredient.Avocado) {
			success = ingredientManager.buyAvocado ();
		}

		if (!success) {
			showBottomText ("Not Enough Gold");
			playBlipSFX (false);
		} else {
			playBlipSFX (true);
		}

	}

	public int getCurrTimeInMinute() {
		return hour * 60 + minute;
	}

	public void makeSushi(int sushiNo) {

		SushiManager.Sushi sushi;

		if (sushiNo == 1) {
			sushi = SushiManager.Sushi.TunaNigiri;
		} else if (sushiNo == 2) {
			sushi = SushiManager.Sushi.CaliforniaRoll;
		} else if (sushiNo == 3) {
			sushi = SushiManager.Sushi.SalmonNigiri;
		} else if (sushiNo == 4) {
			sushi = SushiManager.Sushi.WhiteTunaNigiri;
		} else if (sushiNo == 5) {
			sushi = SushiManager.Sushi.TamagoNigiri;
		} else if (sushiNo == 6) {
			sushi = SushiManager.Sushi.SalmonRoll;
		} else {
			return;
		}

		int counter_loc = mapManager.findAvailableSushiLocOnCounter ();
		//Debug.Log ("Available Sushi Loc on Counter Found: " + counter_loc);
		if (counter_loc < 0) {
			showBottomText ("No Counter Space");
			return;
		}

		bool success = false;

		if (sushi == SushiManager.Sushi.TunaNigiri) {
			success = sushiManager.makeTunaNigiri (ingredientManager);
		} else if (sushi == SushiManager.Sushi.SalmonNigiri) {
			success = sushiManager.makeSalmonNigiri (ingredientManager);
		} else if (sushi == SushiManager.Sushi.WhiteTunaNigiri) {
			success = sushiManager.makeWhiteTunaNigiri (ingredientManager);
		} else if (sushi == SushiManager.Sushi.TamagoNigiri) {
			success = sushiManager.makeTamagoNigiri (ingredientManager);
		} else if (sushi == SushiManager.Sushi.CaliforniaRoll) {
			success = sushiManager.makeCaliforniaRoll (ingredientManager);
		} else if (sushi == SushiManager.Sushi.SalmonRoll) {
			success = sushiManager.makeSalmonRoll (ingredientManager);
		}

		if (!success) {
			showBottomText ("Not Enough Ingredient");
			playBlipSFX (false);
		} else {
			showBottomText ("Sushi Made");
			mapManager.setSushiOnCounter (counter_loc, sushi);
			playBlipSFX (true);
		}
	}

	public void closeAllUI() {
		GameObject[] UIs = GameObject.FindGameObjectsWithTag ("UI2");
		foreach (GameObject UI in UIs) {
			UI.SetActive (false);
		}
	}

	public void enterGalGameMode(int statusCode, SushiManager.Sushi sushiWanted) {
		GalGameMode galGameMode = catGalGameModePanel.GetComponent<GalGameMode> ();
		galGameMode.statusCode = statusCode;
		galGameMode.sushiWanted = sushiWanted;
		galGameMode.named = false;
		galGameMode.updateInitialText ();
		Time.timeScale = 0.0f;
		catGalGameModePanel.SetActive (true);
	}

	public void quitGalGameMode() {
		Time.timeScale = 1.0f;
		catGalGameModePanel.SetActive (false);
	}

	public void enterWeaponStore() {
		weaponStore.SetActive (true);
	}

	public void closeWeaponStore() {
		weaponStore.SetActive (false);
	}

	public void enterCatSupplyStore() {
		catSupplyStore.SetActive (true);
	}

	public void closeCatSupplyStore() {
		catSupplyStore.SetActive (false);
	}

	public void consumeSushi(int targetCounterSapce) {
		if (targetCounterSapce >= 0) {
			mapManager.setSushiOnCounter (targetCounterSapce, SushiManager.Sushi.NOTHING);
		} 
	}

	public void showBottomText(string text) {

		bottomTextPanel.SetActive (true);
		bottomText.text = text;

	}

	public void hideBottomText() {

		bottomText.text = "";
		bottomTextPanel.SetActive (false);

	}

	public void increaseNumGold(int howmuch) {
		numGold += howmuch;
	}

	public void decreaseNumGold(int howmuch) {
		numGold -= howmuch;
	}

	void pauseClock() {
		Time.timeScale = 0;
		clockPaused = true;
		gamePausedIndicator.SetActive (true);
	}

	void unpauseClock() {
		Time.timeScale = 1.0f;
		clockPaused = false;
		gamePausedIndicator.SetActive (false);
	}

	void endTodayAndStartNewDay () {
		Time.timeScale = 0;
		netProfit = numGold - initNumGold;
		endOfDayDisplay.SetActive (true);
		endOfDayMessage.text = "Good Job!\n" + 
			"Today you've earned " + netProfit + "G.\n" +
			rent + "G's rent has been charged";

		// Check victory condition
		if (catManager.catPopularity < -130) {
			ending1.SetActive (true);
		}
		if (catManager.catPopularity > 65) {
			ending2.SetActive (true);
		}

		GameObject saveButtonObj = endOfDayDisplay.gameObject.transform.FindChild ("SaveButton").gameObject;
		Button saveButton = saveButtonObj.GetComponent<Button>();
		Text saveText = saveButtonObj.GetComponentInChildren<Text> ();
		saveText.text = "Save";
		saveButton.interactable = true;

		hour = openTime;
		minute = 1;

		levelManager.setUpLevel (dayCount);

		// Show weapon store if cat popularity is less than threshold
		if (PlayerDataManager.playerData.weaponStoreEnabled) {
			GameObject weaponStoreBtn = endOfDayDisplay.transform.FindChild ("WeaponStoreButton").gameObject;
			weaponStoreBtn.SetActive (true);
		}

		// Show cat supply store if it is enabled
		if (PlayerDataManager.playerData.catStoreEnabled) {
			GameObject CatStoreBtn = endOfDayDisplay.transform.FindChild ("CatSupplyStoreButton").gameObject;
			CatStoreBtn.SetActive (true);
		}


		// Remove Guests, Cats, and sushi on counter
		foreach (Guest guest in guestManager.guestList) {
			guest.gameObject.SetActive (false);
		}
			
//		foreach (Cat cat in catManager.catList) {
//			cat.gameObject.SetActive (false);
//		}
			
		for (int i = 0; i < mapManager.sushiOnCounterIndicators.Length; i ++) {
			mapManager.setSushiOnCounter (i, SushiManager.Sushi.NOTHING);
		}


		for (int i = 0; i < mapManager.tables.Length; i ++) {
			//.Log ("Table " + i + ": " + mapManager.tables [i].ToString());
			//Debug.Log ("SushiPlate " + i + ": " + mapManager.tables [i].sushiPlate.ToString());
			mapManager.tables [i].sushiPlate.gameObject.SetActive (false);
			mapManager.tables [i].availability = true;
		}

	}

	public void startNewDay() {
		endOfDayDisplay.SetActive (false);
		Time.timeScale = 1.0f;

		dayCount = dayCount + 1;

	}



	public void quitGame() {
		Application.Quit ();
	}

	public void openSystemMenu() {
		pauseClock ();
		systemMenu.SetActive (true);
	}

	public void closeSystemMenu() {
		unpauseClock ();
		systemMenu.SetActive (false);
	}

	public void saveGame() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.OpenOrCreate);

		PlayerData data = PlayerDataManager.playerData;
		data.catPopularity = catManager.catPopularity;
		data.humanPopularity = guestManager.humanPopularity;
		data.numGold = numGold;
		data.dayCount = dayCount;

		data.numRice = ingredientManager.numRice;
		data.numSalmon = ingredientManager.numSalmon;
		data.numAvocado = ingredientManager.numAvocado;
		data.numCucumber = ingredientManager.numCucumber;
		data.numEgg = ingredientManager.numEgg;
		data.numTuna = ingredientManager.numTuna;
		data.numWhiteFish = ingredientManager.numWhiteFish;

		data.basicAttack = basicAttack;
		data.weaponAttack = weaponAttack;

		data.basicCatPettingPower = basicCatPettingPower;
		data.itemCatPettingPower = itemCatPettingPower;

		bf.Serialize (file, data);
		file.Close ();

		GameObject saveButtonObj = endOfDayDisplay.gameObject.transform.FindChild ("SaveButton").gameObject;
		Button saveButton = saveButtonObj.GetComponent<Button>();
		Text saveText = saveButtonObj.GetComponentInChildren<Text> ();
		saveText.text = "Saved";
		saveButton.interactable = false;

	}

	void gameover() {
		SceneManager.LoadScene ("GameOver");
	}

	public void buyWeapon(string itemID) {
		if (itemID.Equals ("Broomstick")) {
			weaponAttack = 3;
		} else if (itemID.Equals ("FeatherDuster")) {
			weaponAttack = 2;
		}

		PlayerDataManager.playerData.itemsOwned.Add (itemID);
	}

	public void buyCatSupply(string itemID) {
		if (itemID.Equals ("ToyMouse")) {
			itemCatPettingPower = 1;
		} else if (itemID.Equals ("ScratchingPost")) {
			itemCatPettingPower = 3;
		} else if (itemID.Equals ("CatBell")) {
			itemCatPettingPower = 2;
		}

		PlayerDataManager.playerData.itemsOwned.Add (itemID);
	}

	// Update is called once per frame
	void Update () {

		gameplayDataIndicator.text = "Gold: " + numGold + "\n" +
		"Human Popularity: " + guestManager.humanPopularity + "\n" +
		"Cat Popularity: " + catManager.catPopularity;

		clockInidcator.text = hour.ToString("D2") + ":" + minute.ToString("D2");

		dayIndicator.text = "Day " + dayCount;

		if (Input.GetButtonDown ("Submit")) {
			if (!clockPaused) {
				pauseClock ();
			} else {
				unpauseClock ();
			}
		}

		if (!clockPaused) {
			if (Time.time - lastChange > minuteLength) {
				lastChange = Time.time;
				minute++;
				//int totalMinute = minute + (hour - openTime) * 60;
			}
			if (minute >= 60) {
				hour++;
				minute = 0;
			}
			if (hour >= closeTime) {
				numGold -= rent;
				// Game Over Check
				if (numGold <= 0) {
					gameover ();
				} else {
					endTodayAndStartNewDay ();
				}
			}
		}

	}
}
