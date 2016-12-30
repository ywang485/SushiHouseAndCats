using UnityEngine;
using System.Collections;

public class GenericButton : MonoBehaviour {

	public GameObject cursor;
	public TitleScreen titleScreen;
	public Sprite[] imgs;

	protected delegate void clickAction ();
	protected clickAction clickAct;

	// HoverBehaviour:
	// 1 - Move Cursor
	// 2 - Image Change
	protected int hoverBehaviour = 1;

	/*
	void moveCursor() {
		BoxCollider2D box = gameObject.GetComponent<BoxCollider2D> ();
		Vector2 moveCursorTo = new Vector2 (box.transform.position.x - titleScreen.cursorXOffset, box.transform.position.y);

		SpriteRenderer cursorSprite = cursor.GetComponent<SpriteRenderer> ();
		Color changeTo = cursorSprite.color;
		changeTo.a = 1f;
		cursorSprite.color = changeTo;

		cursor.transform.position = moveCursorTo;
	}*/

	void changeImage(int index) {
		SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer> ();
		sprite.sprite = imgs[index];
	}

	void OnMouseOver() {

		if (hoverBehaviour == 1) {
			//moveCursor ();
		} else if (hoverBehaviour == 2) {
			changeImage (1);
		}
	}

	void OnMouseExit() {
		if (hoverBehaviour == 2) {
			changeImage (0);
		}
	}

	void OnMouseDown() {
		clickAct ();
	}
}