using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	void OnEnable() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public void hide() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		Color changeTo = spriteRenderer.color;
		changeTo.a = 0f;
		spriteRenderer.color = changeTo;
	}

	public void show() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		Color changeTo = spriteRenderer.color;
		changeTo.a = 1f;
		spriteRenderer.color = changeTo;
	}
}
