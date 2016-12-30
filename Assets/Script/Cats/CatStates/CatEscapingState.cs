using UnityEngine;
using System.Collections;

public class CatEscapingState : CatState {

	private Vector2 doorLoc;

	public CatEscapingState(Cat subjCat) : base(subjCat) {
		doorLoc = GameManager.getGameManager ().mapManager.getDoorLocation ();
	}

	public override void UpdateState ()
	{
		Vector2 currPos = new Vector2 (cat.transform.position.x, cat.transform.position.y);
		cat.transform.position = Vector2.MoveTowards(currPos, doorLoc, cat.getSpeed() * Time.deltaTime * 3);
	}

	public override void OnTriggerEnter2D(Collider2D other) {

		if (other.CompareTag ("DoorLoc")) {
			GameObject.Destroy (cat.gameObject);
		}

	}

}
