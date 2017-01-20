using UnityEngine;
using System.Collections;

public class Toy : Item {

	public int catPopInc;
	public int playTime;
	public Sprite finishedSprite;
	public bool finished = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}
