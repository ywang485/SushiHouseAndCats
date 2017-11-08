using UnityEngine;
using System.Collections;

public class Weapon : Item {

	public GameObject target;


	// Use this for initialization
	protected void Start () {
		Destroy(gameObject,1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}
