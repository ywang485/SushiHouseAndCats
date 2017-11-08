using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {

	float speed = 50f;
	int updateCounter = 0;
	int updateCounterMax = 10;
	bool movingLeft = true;
	Vector2 targetLoc;

	[HideInInspector]public GameObject target;

	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.CompareTag ("Cat")) {
			target = other.gameObject;
		}
		
	}

	void OnTriggerExit2D(Collider2D other)
	{
		target = null;
	}

	// Update is called once per frame
	void Update () {
		if (movingLeft) {
			updateCounter -= 1;
			if (updateCounter < -updateCounterMax) {
				movingLeft = false;
			}
		} else {
			updateCounter += 1;
			if (updateCounter > updateCounterMax) {
				movingLeft = true;
			}
		}
		targetLoc = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		targetLoc = new Vector2 (targetLoc.x + (float)updateCounter/(float)updateCounterMax * 1.5f, targetLoc.y);
		Vector2 currPos = new Vector2 (transform.position.x, transform.position.y);
		transform.position = Vector2.MoveTowards(currPos, targetLoc, speed * Time.deltaTime);
	}
}
