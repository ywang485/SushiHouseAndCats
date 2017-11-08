using UnityEngine;
using System.Collections;

public class CatAvoidingState : CatState {

	public int avoidingEndTime;
	public Vector2 avoidingDirection;

	public CatAvoidingState(Cat subjCat) : base(subjCat) {
	}

	public override void UpdateState ()
	{
		if (cat.gameManager.getCurrTimeInMinute () == avoidingEndTime) {
			ToLookingForFood ();
		}

		Vector2 currPos = new Vector2 (cat.transform.position.x, cat.transform.position.y);
		cat.transform.position = Vector2.MoveTowards(currPos, currPos + avoidingDirection, cat.getSpeed() * Time.deltaTime * 4f);
	}

}
