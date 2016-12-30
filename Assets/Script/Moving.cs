using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

	public float speed = 0.1f;

	private Animator animator;

	private Rigidbody2D rb;
	private bool towardsRight = true;

	// Use this for initialization
	void Start () {
	
		rb = gameObject.GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		rb.velocity = Vector2.zero;
		rb.angularVelocity = 0f;

		float movex = Input.GetAxis ("Horizontal");
		float movey = Input.GetAxis ("Vertical");
		rb.velocity = new Vector2 (movex * speed, movey * speed);

		transform.localRotation = Quaternion.identity;

		if (movex == 0 && movey == 0) {
			animator.SetBool ("still", true);
		} else {
			animator.SetBool ("still", false);
			if (movex > 0) {
				if (!towardsRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					towardsRight = true;
				}
			} else if (movey > 0) {
				animator.SetInteger ("direction", 1);
			} else if (movex < 0) {
				if (towardsRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					towardsRight = false;
				}
			} else if (movey < 0) {
				animator.SetInteger ("direction", 0);
			}
		}

	}
}
