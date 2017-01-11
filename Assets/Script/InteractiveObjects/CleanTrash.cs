using UnityEngine;
using System.Collections;

public class CleanTrash : MonoBehaviour {

	void OnTriggerStay2D(Collider2D other) {

		if (Input.GetButtonDown ("Fire1")) {

			if (other.gameObject.tag == "Player") {

				Food food = transform.parent.gameObject.GetComponent<Food> ();
				if (food.active) {
					GameObject.Destroy (food.gameObject);
				}

			}

		}

	}

}
