using UnityEngine;
using System.Collections;

public class Sushi : MonoBehaviour {

	private HPSubject hp;
	private SushiManager.Sushi sushiType;

	private GameManager gameManager;

	public GameObject discardBtn;

	public SushiManager.Sushi getSushiType() {
		return sushiType;
	}

	public void setSushiType(SushiManager.Sushi type) {
		sushiType = type;
		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = gameManager.sushiManager.sushiSprite [SushiManager.sushi2number(sushiType)];
	}

	// Use this for initialization
	void Awake () {

		hp = GetComponent<HPSubject> ();
		gameManager = GameManager.getGameManager ();

	}
	
	// Update is called once per frame
	void Update () {

		if (hp.HP <= 0) {
			gameObject.SetActive(false);
		}

	}

	void OnEnable() {
		hp.resetHP ();
		GameObject hpBar = transform.FindChild ("HPBar").gameObject;
		hpBar.SetActive (false);
		if (discardBtn != null) {
			discardBtn.SetActive (false);
		}
	}
}
