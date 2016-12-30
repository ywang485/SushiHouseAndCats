using UnityEngine;
using System.Collections;

public class CatLookingForFoodState : CatState {

	private GameManager gameManager;

	private int nextWayPoint;
	private int searchingStartTime;
	private Transform[] wayPoints;

	public float actualSpeed;

	private bool facingRight = true;

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

		Vector2 targetLocation = wayPoints [nextWayPoint].position;
		Vector2 currPos = new Vector2 (cat.transform.position.x, cat.transform.position.y);
		cat.transform.position = Vector2.MoveTowards(currPos, targetLocation, actualSpeed * Time.deltaTime);

	}

	public override void OnTriggerEnter2D(Collider2D other) {

		if (other.CompareTag ("CatWaypoint")) {
			nextWayPoint = (nextWayPoint + 1) % wayPoints.Length;
		}

		if (nextWayPoint == 2) {
			if (cat.animator.GetInteger ("animNo") != 1) {
				cat.animator.SetInteger ("animNo", 1);
				actualSpeed = cat.speedScale (1);
			}
		} else if (nextWayPoint == 3) {
			if (cat.animator.GetInteger ("animNo") != 5) {
				cat.animator.SetInteger ("animNo", 5);
				actualSpeed = cat.speedScale (2.0f);
				cat.transform.localScale = new Vector3 (cat.transform.localScale.x * (-1), cat.transform.localScale.y, cat.transform.localScale.z);
			}
		} else if (nextWayPoint == 4) {
			if (cat.animator.GetInteger ("animNo") != 2) {
				cat.animator.SetInteger ("animNo", 2);
				actualSpeed = cat.speedScale (1.0f);
				//cat.transform.localScale = new Vector3 (cat.transform.localScale.x * (-1), cat.transform.localScale.y, cat.transform.localScale.z);
				facingRight = false;
			}
		} else if (nextWayPoint == 0) {
			if (!facingRight) {
				cat.transform.localScale = new Vector3 (cat.transform.localScale.x * (-1), cat.transform.localScale.y, cat.transform.localScale.z);
				facingRight = true;
			}
		} 
		if (other.CompareTag ("Sushi")) {
			Sushi sushi = other.GetComponent<Sushi> ();
			if (cat.isWantedSushi (sushi.getSushiType ())) {
				cat.setTargetSushiPlate(sushi.gameObject);
				ToGoTowardsFood ();
			}
		}
	}

}
