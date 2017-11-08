using UnityEngine;
using System.Collections;

public class Shield : Item {

	public int power;
	public int duration;
	public Sprite decayedSprite;
	public int aliveTime;
	public bool active;

	// Use this for initialization
	void Start () {
		aliveTime = GameManager.getGameManager ().getCurrTimeInMinute ();
		active = true;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		if (GameManager.getGameManager ().getCurrTimeInMinute () - aliveTime >= duration) {
			SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
			sprite.sprite = decayedSprite;
			active = false;
		}
	}
}
