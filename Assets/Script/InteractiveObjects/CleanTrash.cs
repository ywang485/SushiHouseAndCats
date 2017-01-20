using UnityEngine;
using System.Collections;

public class CleanTrash : MonoBehaviour {

	void OnTriggerStay2D(Collider2D other) {

		if (Input.GetButtonDown ("Fire1")) {

			if (other.gameObject.tag == "Player") {

				Item item = transform.parent.gameObject.GetComponent<Item> ();
				GameObject.Destroy (item.gameObject);

			}

		}

	}

}
