using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadGame : GenericButton {

	void Start() {
		base.clickAct = loadGame;
	}

	void loadGame() {
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close();

			Debug.Log ("cat popularity from file: " + data.catPopularity);
			Debug.Log ("human popularity from file: " + data.humanPopularity);
			Debug.Log ("number of gold popularity from file: " + data.numGold);
			Debug.Log ("dday count from file: " + data.dayCount);
		}
	}
}
