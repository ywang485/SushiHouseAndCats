using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatEnemyLevelIndicator : MonoBehaviour {

	private GameObject barContent;
	private Text LvIndicator;
	private float scalingFactor = 5.5f;

	// Use this for initialization
	void Awake () {
		barContent = GameObject.Find ("BarContent");
		LvIndicator = GameObject.Find ("LvIndicator").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		barContent.transform.localScale = new Vector2(scalingFactor * (Mathf.Max(-(float)PlayerDataManager.getPlayerData ().catPopularity, 0f) / (float)LevelManager.getLevelUpXp (PlayerDataManager.getPlayerData ().catEnemyLevel)), scalingFactor);
		LvIndicator.text = "Lv " + PlayerDataManager.getPlayerData ().catEnemyLevel;
	}
}
