using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon {

	public static bool hasInstance;
	private GameObject player;
	private Moving playerMoving;
	private int swingCount;
	private int swingSpeed = 10;
	private bool clockwise;

	// Use this for initialization
	void Start () {
		//base.Start ();
		player = GameObject.Find("player");
		playerMoving = player.GetComponent<Moving> ();
		swingCount = 0;

		if (playerMoving.getFacingRight ()) {
			clockwise = true;
		} else {
			clockwise = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position;
		//Vector2 moveVec = new Vector2 (targetLoc.x - transform.position.x, targetLoc.y - transform.position.y);
		/*Vector2 moveVec = player.GetComponent<Rigidbody2D>().velocity;
		moveVec.Normalize ();
		float rotateAngle = 0;
		if (moveVec.x == 0 && moveVec.y == 0) {
			if (playerMoving.getFacingRight ()) {
				rotateAngle = 0f;
			} else {
				rotateAngle = 0f;
			}
		}
		else if (moveVec.x >= 0 && moveVec.y >= 0) {
			rotateAngle = 0f;
		} else if (moveVec.x <= 0 && moveVec.y >= 0) {
			rotateAngle = 165f;
		} else if (moveVec.x >= 0 && moveVec.y <= 0) {
			rotateAngle = 65f;
		} else {
			rotateAngle = 0f;
		}
		//transform.Translate (moveVec);
		*/
		transform.rotation = Quaternion.AngleAxis(swingCount, new Vector3(0f, 0f, 1f));
		if (!clockwise) {
			swingCount += swingSpeed;
			if (swingCount >= 180) {
				GameObject.Destroy (gameObject);
			}
		} else {
			swingCount -= swingSpeed;
			if (swingCount <= -180) {
				GameObject.Destroy (gameObject);
			}
		}
	}

	void OnDestroy() {
		hasInstance = false;
	}
}
