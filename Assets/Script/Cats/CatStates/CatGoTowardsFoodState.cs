using UnityEngine;
using System.Collections;

public class CatGoTowardsFoodState : CatState {

	public CatGoTowardsFoodState(Cat subjCat) : base(subjCat) {
	}

	public override void UpdateState ()
	{
		if (cat.getTargetSushiPlate () == null) {
			cat.lookingForFoodState.resetSearchingStartTime ();
			cat.towardsFood = false;
			ToLookingForFood ();
			return;
		}

		Vector2 currPos = new Vector2 (cat.transform.position.x, cat.transform.position.y);
		cat.transform.position = Vector2.MoveTowards(currPos, cat.targetPosition, 1.5f * cat.getSpeed() * Time.deltaTime);
		cat.targetPosition = cat.getTargetSushiPlate ().transform.position;
	}

	public override void OnTriggerEnter2D(Collider2D other) {

		if (other.CompareTag ("ReachDetection")) {
			if(other.transform.parent.gameObject == cat.getTargetSushiPlate()) {
				
				if (other.transform.parent.CompareTag ("Toy")) {
					Toy toy = other.transform.parent.gameObject.GetComponent<Toy> ();
					cat.playState.totalPlayTime = toy.playTime;
					Debug.Log("Play State Entered");
					ToPlay ();
					return;
				} else 	if (other.transform.parent.CompareTag ("Treat")) {
					Toy toy = other.transform.parent.gameObject.GetComponent<Toy> ();
					cat.playState.totalPlayTime = toy.playTime;
					Debug.Log("Play State Entered");
					ToPlay ();
					return;
				}
				ToEating ();
			}
		}
	}

}