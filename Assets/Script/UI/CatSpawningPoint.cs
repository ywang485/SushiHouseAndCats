using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawningPoint : MonoBehaviour {

	private int counter = 0;
	private readonly int counterMax = 100;
	private readonly int spawningLikelihood = 50;

	// Use this for initialization
	void Start () {
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Cat spawning counter: " + counter);
		if (counter >= counterMax) {
			counter = 0;
			int r = Random.Range (0, 100);
			if (r <= spawningLikelihood) {
				GameObject catsNode = GameObject.Find ("Cats");
				GameObject catObj = (GameObject)GameObject.Instantiate (Resources.Load(CatManager.catPrefab, typeof(GameObject)) as GameObject, transform.position , Quaternion.identity);
				catObj.transform.SetParent (catsNode.transform);
				catObj.GetComponent<Cat> ().lookingForFoodState.nextWayPoint = GetComponent<Location>().reachableLocs [Random.Range (0, GetComponent<Location>().reachableLocs.Length - 1)];
				catObj.GetComponent<Cat> ().currLoc = GetComponent<Location>();
			}
		}
		counter += 1;
	}
}
