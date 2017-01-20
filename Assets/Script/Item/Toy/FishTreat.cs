using UnityEngine;
using System.Collections;

public class FishTreat : MonoBehaviour {

	private Toy toy;

	// Use this for initialization
	void Awake () {
		toy = gameObject.GetComponent<Toy> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (toy.finished) {
			GameObject.Destroy (gameObject);
		}
	}
}
