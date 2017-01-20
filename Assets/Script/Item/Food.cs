using UnityEngine;
using System.Collections;

public class Food : Item {

	public Sprite finishedSprite;
	public bool finished = false;

	public int catPopInc;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	protected void Update () {
		base.Update ();
	}
}
