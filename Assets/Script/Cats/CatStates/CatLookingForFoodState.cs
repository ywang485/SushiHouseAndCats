using UnityEngine;
using System.Collections;

public class CatLookingForFoodState : CatState {

	private GameManager gameManager;

	public Location nextWayPoint;
	private int searchingStartTime;
	public Transform[] wayPoints;

	public float actualSpeed;

	public CatLookingForFoodState(Cat subjCat) : base(subjCat) {
		gameManager = GameManager.getGameManager ();
		actualSpeed = cat.getSpeed ();
	}

	public void resetSearchingStartTime() {
		searchingStartTime = gameManager.getCurrTimeInMinute();
	}

	public override void UpdateState ()
	{

		if (gameManager.getCurrTimeInMinute () - searchingStartTime > cat.getSearchingTime()) {
			ToLeaving ();
		}
			
		Vector2 currPos = new Vector2 (cat.transform.position.x, cat.transform.position.y);
		cat.transform.position = Vector2.MoveTowards(currPos, cat.targetPosition, actualSpeed * Time.deltaTime);
	}

	public override void OnTriggerEnter2D(Collider2D other) {

		MapManager.LocType currLocType = nextWayPoint.locType;

		if (other.CompareTag ("Location")) {
			int max = nextWayPoint.reachableLocs.Length;
			nextWayPoint = nextWayPoint.reachableLocs [Random.Range (0, max-1)];
		}
			
		cat.targetPosition = nextWayPoint.transform.position;

		if (nextWayPoint.locType == currLocType) {
			cat.jumping = false;
			actualSpeed = cat.speedScale (1.0f);
		} else {
			cat.jumping = true;
			actualSpeed = cat.speedScale (2.0f);
		}
		//if (cat.attacking && other.CompareTag("Player_Range")) {
		//	Debug.Log ("Player Detected!");
		//	cat.attackingState.target = other.transform.parent.gameObject;
		//	ToAttacking ();
		//}
		if (other.CompareTag ("Sushi")) {
			Sushi sushi = other.GetComponent<Sushi> ();
			if (cat.isWantedSushi (sushi.getSushiType ())) {
				cat.setTargetSushiPlate (sushi.gameObject);
				ToGoTowardsFood ();
			}
		} else if (other.CompareTag ("Food")) {
			if (!((Food)other.gameObject.GetComponent<Food> ()).finished) {
				cat.setTargetSushiPlate (other.gameObject);
				ToGoTowardsFood ();
			}
		} else if (other.CompareTag ("Toy")) {
			if (!cat.played && !((Toy)other.gameObject.GetComponent<Toy> ()).finished) {
				cat.showMoodIcon (2);
				cat.setTargetSushiPlate (other.gameObject);
				ToGoTowardsFood ();
			}
		} 

	}

}
