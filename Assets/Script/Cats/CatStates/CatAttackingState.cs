using UnityEngine;
using System.Collections;

public class CatAttackingState : CatState {

	public GameObject target;
	public bool attacking = false;
	public float attackingBufferTime = 1f;
	public float minDist = 0.5f;

	public CatAttackingState(Cat subjCat) : base(subjCat) {
	}

	public override void UpdateState ()
	{
		if (attacking) {
			return;
		}

		Vector2 currPos = new Vector2 (cat.transform.position.x, cat.transform.position.y);
		if (Vector3.Distance (target.transform.position, currPos) >= minDist) {
			// Go towards target
			cat.transform.position = Vector2.MoveTowards (currPos, target.transform.position, cat.getSpeed() * Time.deltaTime);
		} else {
			// Attack
			cat.StartCoroutine (attack ());
			attacking = true;
		}
			
	}

	IEnumerator attack() {
		target.GetComponent<HPSubject> ().beAttacked (10);
		// Knockback
		target.transform.Translate(target.transform.position - cat.transform.position);
		yield return new WaitForSeconds (attackingBufferTime);
		attacking = false;
	}
}
