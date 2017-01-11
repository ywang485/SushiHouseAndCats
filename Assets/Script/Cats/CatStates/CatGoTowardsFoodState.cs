using UnityEngine;
using System.Collections;

public class CatGoTowardsFoodState : CatState {

	public CatGoTowardsFoodState(Cat subjCat) : base(subjCat) {
	}

	public override void UpdateState ()
	{
		Vector2 currPos = new Vector2 (cat.transform.position.x, cat.transform.position.y);
		cat.transform.position = Vector2.MoveTowards(currPos, cat.targetPosition, 1.5f * cat.getSpeed() * Time.deltaTime);

	}

	public override void OnTriggerEnter2D(Collider2D other) {

		if (other.CompareTag ("ReachDetection")) {
			ToEating ();
		}
	}

}