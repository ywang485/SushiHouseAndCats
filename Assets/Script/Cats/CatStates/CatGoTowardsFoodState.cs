using UnityEngine;
using System.Collections;

public class CatGoTowardsFoodState : CatState {

	public CatGoTowardsFoodState(Cat subjCat) : base(subjCat) {
	}

	public void updateAnim() {
		Animator animator = cat.animator;
		Vector2 drct = Utilities.getDirection (new Vector2 (cat.transform.position.x, cat.transform.position.y), cat.getTargetSushiPlate ().gameObject.transform.position);
		float movex = drct.x;
		float movey = drct.y;
		if (movex == 0 && movey == 0) {
			animator.SetInteger ("animNo", 3);
		} else {
			// Moving right: right, back
			if (movex > 0) {
				if (!cat.facingRight) {
					cat.transform.localScale = new Vector3 (cat.transform.localScale.x * (-1), cat.transform.localScale.y, cat.transform.localScale.z);
					cat.facingRight = true;
				}
				animator.SetInteger ("animNo", 4);
				// Moving up: left, back
			} else if (movey > 0) {
				if (cat.facingRight) {
					cat.transform.localScale = new Vector3 (cat.transform.localScale.x * (-1), cat.transform.localScale.y, cat.transform.localScale.z);
					cat.facingRight = false;
				}
				animator.SetInteger ("animNo", 4);
				// Moving left: left, front
			} else if (movex < 0) {
				if (cat.facingRight) {
					cat.transform.localScale = new Vector3 (cat.transform.localScale.x * (-1), cat.transform.localScale.y, cat.transform.localScale.z);
					cat.facingRight = false;
				}
				animator.SetInteger ("animNo", 5);
				// Moving down: right, front
			} else if (movey < 0) {
				if (!cat.facingRight) {
					cat.transform.localScale = new Vector3 (cat.transform.localScale.x * (-1), cat.transform.localScale.y, cat.transform.localScale.z);
					cat.facingRight = true;
				}
				animator.SetInteger ("animNo", 5);
			}
		}
	}

	public override void UpdateState ()
	{
		Vector2 targetLocation = cat.getTargetSushiPlate ().gameObject.transform.position;
		Vector2 currPos = new Vector2 (cat.transform.position.x, cat.transform.position.y);
		cat.transform.position = Vector2.MoveTowards(currPos, targetLocation, 1.5f * cat.getSpeed() * Time.deltaTime);

	}

	public override void OnTriggerEnter2D(Collider2D other) {

		if (other.CompareTag ("ReachDetection")) {
			ToEating ();
		}
	}

}
