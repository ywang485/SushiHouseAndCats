using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Destroy (gameObject, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
