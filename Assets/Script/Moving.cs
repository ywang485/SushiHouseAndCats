using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

	public float speed = 1f;

	private Animator animator;

	private Rigidbody2D rb;
	private bool towardsRight = true;

	private GameManager gameManager;

	private HPSubject hpSub;

	private Vector2 externalVelocity;

	// Use this for initialization
	void Start () {
	
		gameManager = GameManager.getGameManager ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		hpSub = GetComponent<HPSubject> ();
	}

	public void setExternalVelocity(Vector2 v) {
		externalVelocity = v;
	}

	public bool getFacingRight() {
		return towardsRight;
	}

	// Update is called once per frame
	void Update () {

		if (hpSub.HP <= 0) {
			gameManager.gameover ();
		}

		rb.velocity = Vector2.zero;
		rb.angularVelocity = 0f;

		float movex = Input.GetAxis ("Horizontal") + externalVelocity.x;
		float movey = Input.GetAxis ("Vertical") + externalVelocity.y;
		//rb.velocity = Utilities.coordinateReverseTransform(new Vector2 (movex * speed * (gameManager.movingSpeed + 1), movey * speed * (gameManager.movingSpeed + 1) ));
		//rb.velocity = new Vector2 ((movex * speed * 1f + movey * speed * (-1f)) * (PlayerDataManager.getPlayerData().movingSpeed + 1), (movex * speed * 0.5f + movey * speed * (0.5f)) * (PlayerDataManager.getPlayerData().movingSpeed + 1));
		rb.velocity = new Vector2(movex * speed, movey * speed);
		transform.localRotation = Quaternion.identity;

		if (movex == 0 && movey == 0) {
			animator.SetBool ("still", true);
		} else {
			animator.SetBool ("still", false);
			// Moving right: right, back
			if (movex > 0) {
				if (!towardsRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					towardsRight = true;
				}
				animator.SetInteger ("direction", 1);
			// Moving up: left, back
			} else if (movey > 0) {
				if (towardsRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					towardsRight = false;
				}
				animator.SetInteger ("direction", 1);
			// Moving left: left, front
			} else if (movex < 0) {
				if (towardsRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					towardsRight = false;
				}
				animator.SetInteger ("direction", 0);
			// Moving down: right, front
			} else if (movey < 0) {
				if (!towardsRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					towardsRight = true;
				}
				animator.SetInteger ("direction", 0);
			}
		}

	}
}
