using UnityEngine;
using System.Collections;

public class CatLeavingState : CatState {

	private Vector2 doorLoc;

	public CatLeavingState(Cat subjCat) : base(subjCat) {
		doorLoc = cat.gameManager.mapManager.getDoorLocation ();
	}

	public override void UpdateState ()
	{
		Vector2 currPos = new Vector2 (cat.transform.position.x, cat.transform.position.y);
		cat.transform.position = Vector2.MoveTowards(currPos, doorLoc, cat.getSpeed() * Time.deltaTime);
	}

	public override void OnTriggerEnter2D(Collider2D other) {

		if (other.CompareTag ("DoorLoc")) {
			GameObject.Destroy (cat.gameObject);
		}

	}

}
