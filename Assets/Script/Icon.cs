using UnityEngine;
using System.Collections;

public class Icon : MonoBehaviour {
	
	private SpriteRenderer spriteRenderer;

	void OnEnable () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public void setIcon (Sprite sprite) {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = sprite;
	}
}
