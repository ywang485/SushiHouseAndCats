 using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour {

	// Static Parameters
	static public readonly int rent = 10;

	// Daily Profit Count
	static public int netProfit = 0;
	private int initNumGold;

	public MapManager mapManager;
	[HideInInspector] public GameMessagingCenter messagingCenter;

	[HideInInspector] public SushiManager sushiManager;
	[HideInInspector] public IngredientManager ingredientManager;
	[HideInInspector] public CatManager catManager;
	[HideInInspector] public GuestManager guestManager;
	[HideInInspector] public PlayerDataManager playerDataManager;
	[HideInInspector] public LevelManager levelManager;
	[HideInInspector] public WeaponManager weaponManager;

	public const int maxPopularity = 100;

	// In-Game Time Control
	public float gameStartTime;
	private int hour;
	private int minute = 1;

	public float minuteLength = 1;

	float lastChange;
	public static bool clockPaused = false;

	public int openTime = 11;
	public int closeTime = 21;

	//public GameObject sushiIndicator;
	public GameObject gamePausedIndicator;
	public GameObject systemMenu;
	public GameObject weaponStore;
	public GameObject storeUpgradePanel;
	public GameObject catSupplyStore;
	public GameObject catGalGameModePanel;

	// Audio Clips
	public static readonly string posBlipSFX = "SFX/posBlip";
	public static readonly string negBlipSFX = "SFX/negBlip";
	public static readonly string coinSFX = "SFX/coin";
	public static readonly string hurtSFX = "SFX/hurt";
	public static readonly string grumpyMeowSFX = "SFX/grumpyMeow";
	public static readonly string happyMeowSFX = "SFX/happyMeow";

	// UI
	public static readonly string storeUI = "Prefabs/UI/WeaponStoreUI";

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

	// Room items
	public GameObject treadmill;
	public GameObject dartboard;

	public void playSFX(string SFXPath) {
		audioSrc.PlayOneShot(Resources.Load(SFXPath, typeof(AudioClip)) as AudioClip, 1f);
	}

	public int getSushiPlateCount() {
		return GameObject.FindGameObjectsWithTag ("Sushi").Length;
	}

	public int getGuestCount() {
		return GameObject.FindGameObjectsWithTag ("Guest").Length;
	}

	public static GameManager getGameManager() {
		return FindObjectOfType(typeof (GameManager)) as GameManager;
	}

	// initialization
	void Awake () {

		gameStartTime = Time.time;

		audioSrc = GetComponent<AudioSource> ();
		messagingCenter = GetComponent<GameMessagingCenter> ();
		sushiManager = gameObject.GetComponentInChildren<SushiManager> ();
		ingredientManager = gameObject.GetComponentInChildren<IngredientManager> ();
		catManager = gameObject.GetComponentInChildren<CatManager> ();
		guestManager = gameObject.GetComponentInChildren<GuestManager> ();
		levelManager = gameObject.GetComponentInChildren<LevelManager> ();
		weaponManager = gameObject.GetComponentInChildren<WeaponManager> ();

		lastChange = Time.time;
		hour = openTime;

		initNumGold = PlayerDataManager.getPlayerData().numGold;

		updateRoomItems ();
	}

	public int getNumFood() {
		return GameObject.FindGameObjectsWithTag ("Food").Length;
	}

	public int getNumCat() {
		return GameObject.FindGameObjectsWithTag ("Cat").Length;
	}

	public int calculateDamage(int defense) {
		return PlayerDataManager.getPlayerData().basicAttack + weaponManager.getWeaponDamage() - defense;
	}

	public int calculatePettingPower() {
		return PlayerDataManager.getPlayerData().basicAffinity + PlayerDataManager.getPlayerData().itemAffinity;
	}

	public void buyIngredient(string ingredient) {

		bool success = false;

		success = ingredientManager.buyIngredient (ingredient);

		if (!success) {
			showBottomText ("Not Enough Gold");
			playSFX (negBlipSFX);
		} else {
			playSFX (posBlipSFX);
		}

	}

	public void updateRoomItems() {
		if(PlayerDataManager.getPlayerData().inventory.ContainsKey ("treadmill")) {
			treadmill.SetActive (true);
		}
		if(PlayerDataManager.getPlayerData().inventory.ContainsKey ("dartboard")) {
			dartboard.SetActive (true);
		}
	}

	public int getCurrTimeInMinute() {
		return hour * 60 + minute;
	}

	public void makeSushi(string sushi) {

		int counter_loc = mapManager.findAvailableSushiLocOnCounter ();
		//Debug.Log ("Available Sushi Loc on Counter Found: " + counter_loc);
		if (counter_loc < 0) {
			showBottomText ("No Counter Space");
			return;
		}

		bool success = false;

		success = sushiManager.makeSushiCheck (sushi);

		if (!success) {
			showBottomText ("Not Enough Ingredient");
			playSFX (negBlipSFX);
		} else {
			showBottomText ("Sushi Made");
			mapManager.setSushiOnCounter (counter_loc, sushi);
			playSFX (posBlipSFX);
		}
	}

	public void closeAllUI() {
		GameObject[] UIs = GameObject.FindGameObjectsWithTag ("UI2");
		foreach (GameObject UI in UIs) {
			UI.SetActive (false);
		}
	}

	public void enterGalGameMode(int statusCode, string sushiWanted) {
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
		storeUpgradePanel.SetActive (true);
		weaponStore.SetActive (true);
	}

	public void closeWeaponStore() {
		weaponStore.SetActive (false);
		storeUpgradePanel.SetActive (false);
	}

	public void enterCatSupplyStore() {
		catSupplyStore.SetActive (true);
	}

	public void closeCatSupplyStore() {
		catSupplyStore.SetActive (false);
	}

	public void consumeSushi(int targetCounterSapce) {
		if (targetCounterSapce >= 0) {
			mapManager.setSushiOnCounter (targetCounterSapce, "NOTHING");
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

	public void buyItem(string itemName) {
		InventoryItem item = ItemDatabase.getItem (itemName);
		if (item == null) {
			Debug.Log ("Item " + itemName + " does not exist!");
			return;
		}
		if (PlayerDataManager.getPlayerData().numGold > item.getPrice ()) {
			PlayerDataManager.getPlayerData().numGold -= item.getPrice ();
			PlayerDataManager.getPlayerData().inventory.Add (itemName, true);
			messagingCenter.eventHappened (new NewItemObtainedEvent(itemName));
		}

	}

	public void increaseNumGold(int howmuch) {
		PlayerDataManager.getPlayerData().numGold += howmuch;
	}

	public void decreaseNumGold(int howmuch) {
		PlayerDataManager.getPlayerData().numGold -= howmuch;
	}

	public void pauseClock() {
		Time.timeScale = 0;
		clockPaused = true;
		gamePausedIndicator.SetActive (true);
	}

	public void unpauseClock() {
		Time.timeScale = 1.0f;
		clockPaused = false;
		gamePausedIndicator.SetActive (false);
		updateRoomItems ();
	}

	void endTodayAndStartNewDay () {
		pauseClock ();
		netProfit = PlayerDataManager.getPlayerData().numGold - initNumGold;
		endOfDayDisplay.SetActive (true);
		endOfDayMessage.text = "Good Job!\n" + 
			"Today you've earned " + netProfit + "G.\n" +
			rent + "G's rent has been charged";

		// Check victory condition
		if (PlayerDataManager.getPlayerData().catPopularity < -130) {
			ending1.SetActive (true);
		}
		if (PlayerDataManager.getPlayerData().catPopularity > 65) {
			ending2.SetActive (true);
		}

		GameObject saveButtonObj = endOfDayDisplay.gameObject.transform.Find ("SaveButton").gameObject;
		Button saveButton = saveButtonObj.GetComponent<Button>();
		Text saveText = saveButtonObj.GetComponentInChildren<Text> ();
		saveText.text = "Save";
		saveButton.interactable = true;

		hour = openTime;
		minute = 1;

		levelManager.setUpLevel (PlayerDataManager.getPlayerData().dayCount);

		// Show weapon store if cat popularity is less than threshold
		if (PlayerDataManager.getPlayerData().weaponStoreEnabled) {
			GameObject weaponStoreBtn = endOfDayDisplay.transform.Find ("WeaponStoreButton").gameObject;
			weaponStoreBtn.SetActive (true);
		}

		// Show cat supply store if it is enabled
		if (PlayerDataManager.getPlayerData().catStoreEnabled) {
			GameObject CatStoreBtn = endOfDayDisplay.transform.Find ("CatSupplyStoreButton").gameObject;
			CatStoreBtn.SetActive (true);
		}


		// Remove Guests, Cats, and sushi on counter
//		foreach (Guest guest in guestManager.guestList) {
//			guest.gameObject.SetActive (false);
//		}
			
//		foreach (Cat cat in catManager.catList) {
//			cat.gameObject.SetActive (false);
//		}
			
		for (int i = 0; i < mapManager.sushiOnCounterIndicators.Length; i ++) {
			mapManager.setSushiOnCounter (i, "NOTHING");
		}


		for (int i = 0; i < mapManager.tables.Length; i ++) {
			//.Log ("Table " + i + ": " + mapManager.tables [i].ToString());
			//Debug.Log ("SushiPlate " + i + ": " + mapManager.tables [i].sushiPlate.ToString());
			mapManager.tables [i].sushiPlate.gameObject.SetActive (false);
			mapManager.tables [i].availability = true;
		}

	}

	public void startNewDay() {
		unpauseClock ();
		SceneManager.LoadScene ("EndOfDayMenu");

		levelManager.unlockIngredient ();
		levelManager.unlockSushiType ();
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
		PlayerData data = PlayerDataManager.getPlayerData();
		bf.Serialize (file, data);
		file.Close ();

		GameObject saveButtonObj = endOfDayDisplay.gameObject.transform.Find ("SaveButton").gameObject;
		Button saveButton = saveButtonObj.GetComponent<Button>();
		Text saveText = saveButtonObj.GetComponentInChildren<Text> ();
		saveText.text = "Saved";
		saveButton.interactable = false;

	}

	public void gameover() {
		SceneManager.LoadScene ("GameOver");
	}

	public void buyWeapon(string itemID) {
		//if (itemID.Equals ("Broomstick")) {
		//	weaponAttack = 3;
		//} else if (itemID.Equals ("FeatherDuster")) {
		//	weaponAttack = 2;
		//}

		//PlayerDataManager.getPlayerData().itemsOwned.Add (itemID);
	}

	public void buyCatSupply(string itemID) {
		if (itemID.Equals ("ToyMouse")) {
			PlayerDataManager.getPlayerData().itemAffinity = 1;
		} else if (itemID.Equals ("ScratchingPost")) {
			PlayerDataManager.getPlayerData().itemAffinity = 3;
		} else if (itemID.Equals ("CatBell")) {
			PlayerDataManager.getPlayerData().itemAffinity = 2;
		}

		//PlayerDataManager.getPlayerData().itemsOwned.Add (itemID);
	}

	public void openStoreUI() {
		instantiateUI (storeUI);
	}

	static public GameObject instantiateUI(string UIPrefabPath) {
		GameObject canvasNode = GameObject.Find ("Canvas");
		GameObject UIObj = (GameObject)GameObject.Instantiate (Resources.Load(UIPrefabPath, typeof(GameObject)) as GameObject, canvasNode.transform);
		return UIObj;
	}

	// Update is called once per frame
	void Update () {

		int totalGameTime = Mathf.FloorToInt(Time.time - gameStartTime) / 60 + PlayerDataManager.getPlayerData().gameDuration;

		gameplayDataIndicator.text = "Gold: " + PlayerDataManager.getPlayerData().numGold + "\n" +
			"Human Popularity: " + PlayerDataManager.getPlayerData().humanPopularity + "\n" +
			"Cat Popularity: " + PlayerDataManager.getPlayerData().catPopularity + "\n" +
			"Cat Enemy Level: " + PlayerDataManager.getPlayerData().catEnemyLevel + "\n" +
			"Cat Friend Level: " + PlayerDataManager.getPlayerData().catFriendLevel;

		clockInidcator.text = hour.ToString("D2") + ":" + minute.ToString("D2");

		dayIndicator.text = "Day " + PlayerDataManager.getPlayerData().dayCount;

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
				hour = openTime;
				PlayerDataManager.getPlayerData().numGold -= rent;
				// Game Over Check
				if (PlayerDataManager.getPlayerData().numGold <= 0) {
					gameover ();
				} else {
				//	endTodayAndStartNewDay ();
				}
			}
		}

	}
}
