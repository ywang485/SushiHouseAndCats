using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerDataManager : MonoBehaviour {

	public static PlayerData playerData;
	public bool isLoadedGame;


	void Awake () {
		DontDestroyOnLoad(transform.gameObject);
	}

	public void startNewGame(InputField playerNameInput) {
		playerData = new PlayerData ();
		playerData.playerName = playerNameInput.text;
		if (playerData.playerName == "") {
			playerData.playerName = "SomePlayer";
		}
		playerData.itemsOwned = new ArrayList ();
		SceneManager.LoadScene ("OpeningStory");
		Debug.Log ("New Game Started as " + playerData.playerName);
	}

	public void loadGame() {
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			playerData = (PlayerData)bf.Deserialize (file);
			file.Close ();

			isLoadedGame = true;
			SceneManager.LoadScene ("GamePlay");
		} else {
			Debug.Log ("No save date found!");
		}
	}
}
