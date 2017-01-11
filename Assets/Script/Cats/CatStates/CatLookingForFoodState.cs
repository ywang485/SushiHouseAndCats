using UnityEngine;
using System.Collections;

public class CatLookingForFoodState : CatState {

	private GameManager gameManager;

	private int nextWayPoint;
	private int searchingStartTime;
	private Transform[] wayPoints;

	public float actualSpeed;

	public CatLookingForFoodState(Cat subjCat) : base(subjCat) {
		gameManager = GameManager.getGameManager ();
		wayPoints = gameManager.mapManager.catWanderingWayPoints;
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

		if (other.CompareTag ("CatWaypoint")) {
			nextWayPoint = (nextWayPoint + 1) % wayPoints.Length;
		}

		cat.targetPosition = wayPoints [nextWayPoint].position;
		if (nextWayPoint == 1) {
			//cat.towardsFloor = true;
		}
		else if (nextWayPoint == 2) {
			actualSpeed = cat.speedScale (1);
			cat.towardsFloor = false;
		} else if (nextWayPoint == 3) {
			cat.towardsFloor = true;
			actualSpeed = cat.speedScale (2.0f);
		} else if (nextWayPoint == 4) {
			cat.towardsFloor = false;
			actualSpeed = cat.speedScale (1.0f);
		} else if (nextWayPoint == 0) {
		} 
		if (other.CompareTag ("Sushi")) {
			Sushi sushi = other.GetComponent<Sushi> ();
			if (cat.isWantedSushi (sushi.getSushiType ())) {
				cat.setTargetSushiPlate (sushi.gameObject);
				ToGoTowardsFood ();
			}
		} else if (other.CompareTag ("Food")) {
			if (!((Food)other.gameObject.GetComponent<Food> ()).finished && ((Food)other.gameObject.GetComponent<Food> ()).active) {
				cat.setTargetSushiPlate (other.gameObject);
				ToGoTowardsFood ();
			}
			
		} else if(other.CompareTag ("Treats"))  {
			GameObject.Destroy (other.gameObject);
			GameManager.getGameManager ().catManager.increaseCatPopularity (1);
			//if (PlayerDataManager.playerData.catMoodIconEnabled) {
				cat.showMoodIcon (2);
			//}
		}

	}

}
