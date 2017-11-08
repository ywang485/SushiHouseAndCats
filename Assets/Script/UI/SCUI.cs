using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void closeUI() {
		GameObject.Destroy(gameObject);
	}

	void OnDestroy() {
		GameManager.getGameManager ().unpauseClock ();
	}
}
