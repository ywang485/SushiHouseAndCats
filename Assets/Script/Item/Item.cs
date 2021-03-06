﻿using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	[HideInInspector] public Vector2 targetLoc;
	[HideInInspector] public GameManager gameManager;
	[HideInInspector] public float speed;
	public string itemId;

	public virtual string getItemId() {
		return itemId;
	}

	void Awake() {
		gameManager = GameManager.getGameManager ();
		speed = PlayerDataManager.getPlayerData().weaponSpeed;
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	protected virtual void Update () {
		Vector2 currPos = new Vector2 (transform.position.x, transform.position.y);
		transform.position = Vector2.MoveTowards(currPos, targetLoc, speed * Time.deltaTime);
	}
}
