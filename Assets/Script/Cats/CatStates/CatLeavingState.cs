using UnityEngine;
using System.Collections;

public class CatLeavingState : CatState {

	public CatLeavingState(Cat subjCat) : base(subjCat) {
	}

	public override void UpdateState ()
	{
		Vector2 currPos = new Vector2 (cat.transform.position.x, cat.transform.position.y);
		cat.transform.position = Vector2.MoveTowards(currPos, cat.targetPosition, cat.getSpeed() * Time.deltaTime);
	}

	public override void OnTriggerEnter2D(Collider2D other) {

		if (other.CompareTag ("DoorLoc")) {
			GameObject.Destroy (cat.gameObject);
		}

	}

}
